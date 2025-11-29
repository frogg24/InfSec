using System;
using System.IO;
using System.Security.Cryptography;

namespace GostMagma
{
    public static class GostMagmaCipher
    {
        // Размер блока в байтах 8 байт
        private const int BlockSize = 8;
        // Размер ключа в байтах 32 байта
        private const int KeySize = 32;
        // Размер синхропосылки в байтах 8 байт
        private const int IvSize = 8;

        // S-блоки для алгоритма Магма (ГОСТ 28147-89)
        private static readonly byte[,] SBox = new byte[8, 16]
        {
            { 0xC, 0x4, 0x6, 0x2, 0xA, 0x5, 0xB, 0x9, 0xE, 0x8, 0xD, 0x7, 0x0, 0x3, 0xF, 0x1 },
            { 0x6, 0x8, 0x2, 0x3, 0x9, 0xA, 0x5, 0xC, 0x1, 0xE, 0x4, 0x7, 0xB, 0xD, 0x0, 0xF },
            { 0xB, 0x3, 0x5, 0x8, 0x2, 0xF, 0xA, 0xD, 0xE, 0x1, 0x7, 0x4, 0xC, 0x9, 0x6, 0x0 },
            { 0xC, 0x8, 0x2, 0x1, 0xD, 0x4, 0xF, 0x6, 0x7, 0x0, 0xA, 0x5, 0x3, 0xE, 0x9, 0xB },
            { 0x7, 0xF, 0x5, 0xA, 0x8, 0x1, 0x6, 0xD, 0x0, 0x9, 0x3, 0xE, 0xB, 0x4, 0x2, 0xC },
            { 0x5, 0xD, 0xF, 0x6, 0x9, 0x2, 0xC, 0xA, 0xB, 0x7, 0x8, 0x1, 0x4, 0x3, 0xE, 0x0 },
            { 0x8, 0xE, 0x2, 0x5, 0x6, 0x9, 0x1, 0xC, 0xF, 0x4, 0xB, 0x0, 0xD, 0xA, 0x3, 0x7 },
            { 0x1, 0x7, 0xE, 0xD, 0x0, 0x5, 0x8, 0x3, 0x4, 0xF, 0xA, 0x6, 0x9, 0xC, 0xB, 0x2 }
        };

        /// <summary>
        /// Шифрование файла алгоритмом ГОСТ Магма в режиме гаммирования
        /// </summary>
        public static void EncryptFile(string inputFile, string outputFile, string password)
        {
            byte[] key = GenerateKeyFromPassword(password);
            byte[] iv = GenerateRandomIV();

            using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                // Записываем синхропосылку в начало файла
                outputStream.Write(iv, 0, iv.Length);

                ProcessStream(inputStream, outputStream, key, iv);
            }
        }

        /// <summary>
        /// Расшифрование файла алгоритмом ГОСТ Магма в режиме гаммирования
        /// </summary>
        public static void DecryptFile(string inputFile, string outputFile, string password)
        {
            byte[] key = GenerateKeyFromPassword(password);

            using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                // Читаем синхропосылку из начала файла
                byte[] iv = new byte[IvSize];
                inputStream.Read(iv, 0, iv.Length);

                ProcessStream(inputStream, outputStream, key, iv);
            }
        }

        /// <summary>
        /// Обработка потока данных (шифрование/расшифрование)
        /// </summary>
        private static void ProcessStream(Stream input, Stream output, byte[] key, byte[] iv)
        {
            byte[] block = new byte[BlockSize];
            byte[] gamma = new byte[BlockSize];
            byte[] counter = new byte[BlockSize];

            Array.Copy(iv, counter, IvSize);
            int bytesRead;

            while ((bytesRead = input.Read(block, 0, BlockSize)) > 0)
            {
                // Генерируем гамму для текущего блока
                GenerateGammaBlock(key, counter, gamma);

                // Накладываем гамму на данные
                for (int i = 0; i < bytesRead; i++)
                {
                    block[i] ^= gamma[i];
                }

                output.Write(block, 0, bytesRead);

                // Увеличиваем счетчик для следующего блока
                IncrementCounter(counter);
            }
        }

        /// <summary>
        /// Генерация блока гаммы для текущего счетчика
        /// </summary>
        private static void GenerateGammaBlock(byte[] key, byte[] counter, byte[] gamma)
        {
            // Шифруем счетчик алгоритмом Магма (простая замена)
            byte[] encryptedCounter = EncryptBlock(counter, key);
            Array.Copy(encryptedCounter, gamma, BlockSize);
        }

        /// <summary>
        /// Шифрование одного блока алгоритмом Магма (32 раунда)
        /// </summary>
        private static byte[] EncryptBlock(byte[] block, byte[] key)
        {
            if (block.Length != BlockSize)
                throw new ArgumentException($"Размер блока должен быть {BlockSize} байт");
            if (key.Length != KeySize)
                throw new ArgumentException($"Размер ключа должен быть {KeySize} байт");

            uint[] subKeys = GenerateSubKeys(key);
            uint left = BytesToUInt32(block, 0);
            uint right = BytesToUInt32(block, 4);

            // 32 раунда шифрования
            for (int round = 0; round < 32; round++)
            {
                uint roundKey = subKeys[round < 24 ? round % 8 : 7 - (round % 8)];
                uint temp = left;
                left = right ^ F(left, roundKey);
                right = temp;
            }

            // Формируем результат
            byte[] result = new byte[BlockSize];
            UInt32ToBytes(right, result, 0);
            UInt32ToBytes(left, result, 4);
            return result;
        }

        /// <summary>
        /// Функция F для одного раунда
        /// </summary>
        private static uint F(uint value, uint key)
        {
            uint temp = (value + key) % uint.MaxValue;
            uint result = 0;

            for (int i = 0; i < 8; i++)
            {
                byte sBoxIndex = (byte)((temp >> (4 * i)) & 0x0F);
                byte sBoxValue = SBox[7 - i, sBoxIndex];
                result |= (uint)(sBoxValue << (4 * i));
            }

            result = (result << 11) | (result >> 21);
            return result;
        }

        /// <summary>
        /// Генерация 32 подключей из основного ключа
        /// </summary>
        private static uint[] GenerateSubKeys(byte[] key)
        {
            uint[] subKeys = new uint[8];
            for (int i = 0; i < 8; i++)
            {
                subKeys[i] = BytesToUInt32(key, i * 4);
            }
            return subKeys;
        }

        /// <summary>
        /// Генерация ключа из пароля с помощью хеширования
        /// </summary>
        private static byte[] GenerateKeyFromPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Генерация случайной синхропосылки
        /// </summary>
        private static byte[] GenerateRandomIV()
        {
            byte[] iv = new byte[IvSize];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }
            return iv;
        }

        /// <summary>
        /// Увеличение счетчика на 1
        /// </summary>
        private static void IncrementCounter(byte[] counter)
        {
            for (int i = counter.Length - 1; i >= 0; i--)
            {
                if (++counter[i] != 0)
                    break;
            }
        }

        // Вспомогательные методы
        private static uint BytesToUInt32(byte[] bytes, int startIndex)
        {
            return (uint)(bytes[startIndex] | (bytes[startIndex + 1] << 8) |
                         (bytes[startIndex + 2] << 16) | (bytes[startIndex + 3] << 24));
        }

        private static void UInt32ToBytes(uint value, byte[] bytes, int startIndex)
        {
            bytes[startIndex] = (byte)value;
            bytes[startIndex + 1] = (byte)(value >> 8);
            bytes[startIndex + 2] = (byte)(value >> 16);
            bytes[startIndex + 3] = (byte)(value >> 24);
        }
    }
}