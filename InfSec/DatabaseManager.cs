using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace InfSec
{
    public static class DatabaseManager
    {
        private static string dbPath = "users.db";
        private static string encryptedDbPath = "users_encrypted.db";
        private static string tempDbPath = "temp_users.db";
        private static string currentDbPath = "";
        private static string currentPasswordPhrase = "";

        public static void InitializeDatabase(string passwordPhrase)
        {
            currentPasswordPhrase = passwordPhrase;

            if (!File.Exists(encryptedDbPath))
            {
                // Первый запуск
                CreateDatabase(tempDbPath);
                currentDbPath = tempDbPath;
                EnsureUsersTable();
                CreateAdminUser(); // создаём ADMIN только в новой базе
            }
            else
            {
                // Повторный запуск: расшифровка
                if (!DecryptDatabase())
                    throw new Exception("Неверная парольная фраза");

                currentDbPath = tempDbPath;

                // Проверка наличия ADMIN
                if (!UserExists("ADMIN"))
                {
                    throw new Exception("Неверная парольная фраза (учетная запись ADMIN не найдена)");
                }
            }
        }


        private static void CreateDatabase(string path)
        {
            if (File.Exists(path)) File.Delete(path);

            SQLiteConnection.CreateFile(path);
            using (var connection = new SQLiteConnection($"Data Source={path};Version=3;"))
            {
                connection.Open();
                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS users (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        username TEXT UNIQUE NOT NULL,
                        password_hash TEXT,
                        blocked BOOLEAN DEFAULT 0,
                        restrictions_enabled BOOLEAN DEFAULT 0,
                        min_length INTEGER DEFAULT 0,
                        expiry_months INTEGER DEFAULT 0
                    )";
                using (var command = new SQLiteCommand(createTableSql, connection)) command.ExecuteNonQuery();
            }
        }

        private static void EnsureUsersTable()
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS users (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        username TEXT UNIQUE NOT NULL,
                        password_hash TEXT,
                        blocked BOOLEAN DEFAULT 0,
                        restrictions_enabled BOOLEAN DEFAULT 0,
                        min_length INTEGER DEFAULT 0,
                        expiry_months INTEGER DEFAULT 0
                    )";
                using (var command = new SQLiteCommand(sql, connection)) command.ExecuteNonQuery();
            }
        }

        public static void CreateAdminUser()
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                // Создаём ADMIN только если его ещё нет
                string sql = "INSERT OR IGNORE INTO users (username, password_hash) VALUES ('ADMIN', '')";
                using (var command = new SQLiteCommand(sql, connection))
                    command.ExecuteNonQuery();
            }
        }


        public static bool ValidateUser(string username, string password)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "SELECT password_hash FROM users WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    var result = command.ExecuteScalar();

                    // Если в базе null, трактуем это как пустую строку
                    string storedHash = result == null || result == DBNull.Value ? "" : result.ToString();

                    // Если пароль пустой → вход разрешён только при пустом хэше
                    if (string.IsNullOrEmpty(password))
                    {
                        return string.IsNullOrEmpty(storedHash);
                    }

                    // Проверяем хэш
                    string inputHash = ComputeMD5Hash(password);
                    return storedHash == inputHash;
                }
            }
        }


        public static void SetUserPassword(string username, string password)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "UPDATE users SET password_hash = @password_hash WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@password_hash", ComputeMD5Hash(password));
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "SELECT username, blocked, restrictions_enabled, min_length, expiry_months FROM users";
                using (var adapter = new SQLiteDataAdapter(sql, connection))
                    adapter.Fill(dt);
            }
            return dt;
        }

        public static bool UserExists(string username)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result) > 0;
                }
            }
        }

        public static void AddUser(string username)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "INSERT INTO users (username, password_hash) VALUES (@username, '')";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static bool IsUserBlocked(string username)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "SELECT blocked FROM users WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    var result = command.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return false;
                    return Convert.ToBoolean(result);
                }
            }
        }

        public static void SetUserBlocked(string username, bool blocked)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "UPDATE users SET blocked = @blocked WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@blocked", blocked);
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void SetUserRestrictions(string username, bool enabled)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "UPDATE users SET restrictions_enabled = @enabled WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@enabled", enabled);
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static bool GetUserRestrictions(string username)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "SELECT restrictions_enabled FROM users WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    var result = command.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return false;
                    return Convert.ToBoolean(result);
                }
            }
        }

        public static void SetUserMinLength(string username, int minLength)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "UPDATE users SET min_length = @min_length WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@min_length", minLength);
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void SetUserExpiry(string username, int expiryMonths)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "UPDATE users SET expiry_months = @expiry_months WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@expiry_months", expiryMonths);
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void EncryptDatabase()
        {
            try
            {
                if (!File.Exists(dbPath))
                {
                    throw new Exception("Файл базы данных не существует");
                }

                byte[] fileBytes = File.ReadAllBytes(dbPath);
                byte[] encryptedBytes = EncryptData(fileBytes);
                File.WriteAllBytes(encryptedDbPath, encryptedBytes);
                File.Delete(dbPath);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка шифрования базы данных: " + ex.Message);
            }
        }

        private static bool DecryptDatabase()
        {
            try
            {
                if (File.Exists(tempDbPath))
                {
                    File.Delete(tempDbPath);
                }

                if (!File.Exists(encryptedDbPath))
                {
                    return false;
                }

                byte[] encryptedBytes = File.ReadAllBytes(encryptedDbPath);
                byte[] decryptedBytes = DecryptData(encryptedBytes);
                File.WriteAllBytes(tempDbPath, decryptedBytes);

                currentDbPath = tempDbPath;

                return true;
            }
            catch
            {
                return false;
            }
        }


        public static void SaveChangesAndEncrypt()
        {
            try
            {
                if (File.Exists(tempDbPath))
                {
                    // Шифруем temp_users.db в users_encrypted.db
                    byte[] fileBytes = File.ReadAllBytes(tempDbPath);
                    byte[] encryptedBytes = EncryptData(fileBytes);
                    File.WriteAllBytes(encryptedDbPath, encryptedBytes);

                    // Удаляем temp только В МОМЕНТ ВЫХОДА (мы вызываем этот метод в FormClosing).
                    File.Delete(tempDbPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении: " + ex.Message);
            }
        }



        private static byte[] EncryptData(byte[] data)
        {
            byte[] key = GenerateKeyFromPhrase(currentPasswordPhrase);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform encryptor = des.CreateEncryptor(key, new byte[8]))
                {
                    return encryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }
        }

        private static byte[] DecryptData(byte[] encryptedData)
        {
            byte[] key = GenerateKeyFromPhrase(currentPasswordPhrase);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform decryptor = des.CreateDecryptor(key, new byte[8]))
                {
                    return decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                }
            }
        }

        private static byte[] GenerateKeyFromPhrase(string phrase)
        {
            byte[] phraseBytes = Encoding.UTF8.GetBytes(phrase);
            byte[] key = new byte[8];
            for (int i = 0; i < 8; i++)
                key[i] = i < phraseBytes.Length ? phraseBytes[i] : (byte)0;
            return key;
        }

        private static string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        
    }
}