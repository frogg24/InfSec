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
        private static string encryptedDbPath = "users_encrypted.db";
        private static string tempDbPath = "temp_users.db";
        private static string currentDbPath = "";
        private static string currentPasswordPhrase = "";

        /// <summary>
        /// Метод, который при первом запуске инициирует создание БД и записи администратора. 
        /// При последующих запусках метод расшифрует существующую БД и проверит
        /// начилие записи администратора
        /// </summary>
        /// <param name="passwordPhrase">Введенная пользователем парольная фраза</param>
        public static void InitializeDatabase(string passwordPhrase)
        {
            currentPasswordPhrase = passwordPhrase;

            if (!File.Exists(encryptedDbPath))
            {
                // Первый запуск
                CreateDatabase(tempDbPath);
                currentDbPath = tempDbPath;
                // создаём ADMIN только в новой базе
                CreateAdminUser();
            }
            else
            {
                // Повторный запуск: расшифровка
                if (!DecryptDatabase()) throw new Exception("Неверная парольная фраза");

                currentDbPath = tempDbPath;

                // Проверка наличия ADMIN
                if (!UserExists("ADMIN"))
                {
                    throw new Exception("Неверная парольная фраза (учетная запись ADMIN не найдена)");
                }
            }
        }

        /// <summary>
        /// Метод создания БД с единственной сущностью User
        /// </summary>
        /// <param name="path">Путь для сохранения БД</param>
        private static void CreateDatabase(string path)
        {
            if (File.Exists(path)) File.Delete(path);

            SQLiteConnection.CreateFile(path);
            using (var connection = new SQLiteConnection($"Data Source={path};Version=3;"))
            {
                connection.Open();
                string createTableSql = @"CREATE TABLE IF NOT EXISTS users (
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

        /// <summary>
        /// Метод для создания пользователя ADMIN, если он не существует
        /// </summary>
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

        /// <summary>
        /// Метод проверки правильности пароля пользователя для входа
        /// Введенный пароля, хэширутеся и сравнивается с хэшем в БД
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Введенный пароль</param>
        /// <returns>True, если пароль верен, иначе - False</returns>
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

                    // Если пароль пустой вход разрешён только при пустом хэше
                    if (string.IsNullOrEmpty(password))
                    {
                        return string.IsNullOrEmpty(storedHash);
                    }

                    // Проверка хэша
                    string inputHash = ComputeMD5Hash(password);
                    return storedHash == inputHash;
                }
            }
        }


        /// <summary>
        /// Метод проверки изменения пароля пользователя для входа
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Новый пароль</param>
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

        /// <summary>
        /// Метод для получения всех пользователей
        /// </summary>
        /// <returns>DataTable -  список пользователей</returns>
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

        /// <summary>
        /// Метод проверки на существование пользователя с таким именем пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>True, если пользователя существует, иначе - False</returns>
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

        /// <summary>
        /// Метод добавления пользователя в БД с пустым паролем
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>True, если пароль верен, иначе - False</returns>
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


        /// <summary>
        /// Метод проверки заюлокирован пользователь или нет по имени пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>True, если пользователь заблокирован, иначе - False</returns>
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

        /// <summary>
        /// Метод блокировки/разблокировки пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="blocked">Поле, указывающее заблокировать или разблокировать</param>
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


        /// <summary>
        /// Метод включения/выключения проверки на ограничения для задания пароля
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="enabled">Поле,отображающее включить ограничения или снять</param>
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

        /// <summary>
        /// Метод проверки включены ли ограничения на пароля для пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>True, если ограничения для пользователя включены, иначе - False</returns>
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

        /// <summary>
        /// Метод пзадания минимальной длины пароля для пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="minLength">Новая минимальная длина пароля</param>
        /// <returns>True, если пароль верен, иначе - False</returns>
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

        /// <summary>
        /// Метод проверки правильности пароля полязователя для входа
        /// Введенный пароля, хэширутеся и сравнивается с хэшем в БД
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Введенный пароль</param>
        /// <returns>True, если пароль верен, иначе - False</returns>
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

        /// <summary>
        /// Метод расшифрования базы данных с использованием алгоритма DES в режиме ECB.
        /// </summary>
        /// <returns>True, если расшифровка успешна, иначе False</returns>
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

        /// <summary>
        /// Метод сохранения изменений и шифрования базы данных при завершении работы программы.
        /// </summary>
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

                    File.Delete(tempDbPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении: " + ex.Message);
            }
        }


        /// <summary>
        /// Метод шифрования данных с использованием алгоритма DES в режиме ECB.
        /// </summary>
        /// <param name="data">Данные для шифрования</param>
        /// <returns>Зашифрованные данные</returns>
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

        /// <summary>
        /// Метод расшифрования данных с использованием алгоритма DES в режиме ECB.
        /// </summary>
        /// <param name="encryptedData">Зашифрованные данные</param>
        /// <returns>Расшифрованные данные</returns>
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

        /// <summary>
        /// Метод генерации ключа шифрования из парольной фразы.
        /// </summary>
        /// <param name="phrase">Парольная фраза</param>
        /// <returns>Ключ шифрования</returns>
        private static byte[] GenerateKeyFromPhrase(string phrase)
        {
            byte[] phraseBytes = Encoding.UTF8.GetBytes(phrase);
            byte[] key = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                key[i] = i < phraseBytes.Length ? phraseBytes[i] : (byte)0;
            }
            return key;
        }

        /// <summary>
        /// Метод вычисления хэша MD5 для пароля.
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Хэш в виде строки</returns>
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

        /// <summary>
        /// Метод получения минимальной длины пароля для пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Минимальная длина пароля</returns>
        public static int GetUserMinLength(string username)
        {
            using (var connection = new SQLiteConnection($"Data Source={currentDbPath};Version=3;"))
            {
                connection.Open();
                string sql = "SELECT min_length FROM users WHERE username = @username";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    var result = command.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return 0;
                    return Convert.ToInt32(result);
                }
            }
        }
    }
}