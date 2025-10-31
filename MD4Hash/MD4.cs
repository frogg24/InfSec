using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD4Hash
{
    public static class MD4
    {
        /// <summary>
        /// размер блока 
        /// </summary>
        private const int BlockSize = 64;

        public static byte[] ComputeHash(byte[] input)
        {
            // Дополнение
            byte[] paddedInput = PadMessage(input);

            // Инициализация MD-буфера
            uint A = 0x67452301;
            uint B = 0xefcdab89;
            uint C = 0x98badcfe;
            uint D = 0x10325476;

            // Обработка по 512-битным блокам - 16 слов по 32 бита
            for (int i = 0; i < paddedInput.Length; i += BlockSize)
            {
                uint[] chunk = new uint[16];

                for (int j = 0; j < 16; j++)
                {
                    chunk[j] = BitConverter.ToUInt32(paddedInput, i + j * 4);
                }

                uint AA = A;
                uint BB = B;
                uint CC = C;
                uint DD = D;

                // Раунд 1: Использует F, 16 шагов
                FF(ref A, B, C, D, chunk[0], 3); FF(ref D, A, B, C, chunk[1], 7); FF(ref C, D, A, B, chunk[2], 11); FF(ref B, C, D, A, chunk[3], 19);
                FF(ref A, B, C, D, chunk[4], 3); FF(ref D, A, B, C, chunk[5], 7); FF(ref C, D, A, B, chunk[6], 11); FF(ref B, C, D, A, chunk[7], 19);
                FF(ref A, B, C, D, chunk[8], 3); FF(ref D, A, B, C, chunk[9], 7); FF(ref C, D, A, B, chunk[10], 11); FF(ref B, C, D, A, chunk[11], 19);
                FF(ref A, B, C, D, chunk[12], 3); FF(ref D, A, B, C, chunk[13], 7); FF(ref C, D, A, B, chunk[14], 11); FF(ref B, C, D, A, chunk[15], 19);

                // Раунд 2: Использует G, 16 шагов
                GG(ref A, B, C, D, chunk[0], 3); GG(ref D, A, B, C, chunk[4], 5); GG(ref C, D, A, B, chunk[8], 9); GG(ref B, C, D, A, chunk[12], 13);
                GG(ref A, B, C, D, chunk[1], 3); GG(ref D, A, B, C, chunk[5], 5); GG(ref C, D, A, B, chunk[9], 9); GG(ref B, C, D, A, chunk[13], 13);
                GG(ref A, B, C, D, chunk[2], 3); GG(ref D, A, B, C, chunk[6], 5); GG(ref C, D, A, B, chunk[10], 9); GG(ref B, C, D, A, chunk[14], 13);
                GG(ref A, B, C, D, chunk[3], 3); GG(ref D, A, B, C, chunk[7], 5); GG(ref C, D, A, B, chunk[11], 9); GG(ref B, C, D, A, chunk[15], 13);

                // Раунд 3: Использует H, 16 шагов
                HH(ref A, B, C, D, chunk[0], 3); HH(ref D, A, B, C, chunk[8], 9); HH(ref C, D, A, B, chunk[4], 11); HH(ref B, C, D, A, chunk[12], 15);
                HH(ref A, B, C, D, chunk[2], 3); HH(ref D, A, B, C, chunk[10], 9); HH(ref C, D, A, B, chunk[6], 11); HH(ref B, C, D, A, chunk[14], 15);
                HH(ref A, B, C, D, chunk[1], 3); HH(ref D, A, B, C, chunk[9], 9); HH(ref C, D, A, B, chunk[5], 11); HH(ref B, C, D, A, chunk[13], 15);
                HH(ref A, B, C, D, chunk[3], 3); HH(ref D, A, B, C, chunk[11], 9); HH(ref C, D, A, B, chunk[7], 11); HH(ref B, C, D, A, chunk[15], 15);

                A += AA;
                B += BB;
                C += CC;
                D += DD;
            }

            // Формирование результата
            byte[] output = new byte[16];
            Array.Copy(BitConverter.GetBytes(A), 0, output, 0, 4);
            Array.Copy(BitConverter.GetBytes(B), 0, output, 4, 4);
            Array.Copy(BitConverter.GetBytes(C), 0, output, 8, 4);
            Array.Copy(BitConverter.GetBytes(D), 0, output, 12, 4);

            return output;
        }

        private static byte[] PadMessage(byte[] message)
        {
            int originalLength = message.Length;
            int originalBitLength = originalLength * 8;

            // Рассчет длины дополнения (padding)
            // Нужно, чтобы (originalLength + 1 + paddingLength + 8) % 64 == 0
            int paddingLength = (56 - (originalLength + 1) % 64 + 64) % 64;

            // Создание нового массива с дополнением и длиной
            byte[] padded = new byte[originalLength + 1 + paddingLength + 8];

            // Копирование исходного сообщения
            Array.Copy(message, 0, padded, 0, originalLength);

            // Добавить байт 0x80 (представляет бит '1' и 7 нулей). Остальные байты после 0x80 и до длины уже равны 0 по умолчанию
            padded[originalLength] = 0x80;

            // 6. Добавление длины исходного сообщения в битах
            Array.Copy(BitConverter.GetBytes((uint)originalBitLength), 0, padded, padded.Length - 8, 4);
            Array.Copy(BitConverter.GetBytes((uint)0), 0, padded, padded.Length - 4, 4);

            return padded;
        }

        // Лямбда выражения для логических операций
        private static uint F(uint x, uint y, uint z) => (x & y) | (~x & z);
        private static uint G(uint x, uint y, uint z) => (x & y) | (x & z) | (y & z);
        private static uint H(uint x, uint y, uint z) => x ^ y ^ z;

        /// <summary>
        /// Выполняет один шаг первого раунда алгоритма MD4.
        /// Применяет вспомогательную функцию F к трем регистрам (b, c, d), складывает результат
        /// с текущим значением регистра a, добавляет слово из текущего 512-битного блока (x),
        /// выполняет циклический сдвиг влево на заданное количество битов (s) и записывает результат обратно в регистр a.
        /// </summary>
        /// <param name="a">Ссылка на первый регистр (A, D, C или B в зависимости от шага).</param>
        /// <param name="b">Второй регистр (входное значение).</param>
        /// <param name="c">Третий регистр (входное значение).</param>
        /// <param name="d">Четвёртый регистр (входное значение).</param>
        /// <param name="x">Слово из 32-битного массива текущего 512-битного блока данных.</param>
        /// <param name="s">Количество битов для циклического сдвига влево.</param>
        private static void FF(ref uint a, uint b, uint c, uint d, uint x, byte s)
        {
            a += F(b, c, d) + x;
            a = RotateLeft(a, s);
        }

        /// <summary>
        /// Выполняет один шаг второго раунда алгоритма MD4.
        /// Применяет вспомогательную функцию G к трем регистрам (b, c, d), складывает результат
        /// с текущим значением регистра a, добавляет слово из текущего 512-битного блока (x),
        /// прибавляет константу 0x5a827999, выполняет циклический сдвиг влево на заданное количество битов (s)
        /// и записывает результат обратно в регистр a.
        /// </summary>
        /// <param name="a">Ссылка на первый регистр (A, D, C или B в зависимости от шага).</param>
        /// <param name="b">Второй регистр (входное значение).</param>
        /// <param name="c">Третий регистр (входное значение).</param>
        /// <param name="d">Четвёртый регистр (входное значение).</param>
        /// <param name="x">Слово из 32-битного массива текущего 512-битного блока данных.</param>
        /// <param name="s">Количество битов для циклического сдвига влево.</param>
        private static void GG(ref uint a, uint b, uint c, uint d, uint x, byte s)
        {
            a += G(b, c, d) + x + 0x5a827999;
            a = RotateLeft(a, s);
        }

        /// <summary>
        /// Выполняет один шаг третьего раунда алгоритма MD4.
        /// Применяет вспомогательную функцию H к трем регистрам (b, c, d), складывает результат
        /// с текущим значением регистра a, добавляет слово из текущего 512-битного блока (x),
        /// прибавляет константу 0x6ed9eba1, выполняет циклический сдвиг влево на заданное количество битов (s)
        /// и записывает результат обратно в регистр a.
        /// </summary>
        /// <param name="a">Ссылка на первый регистр (A, D, C или B в зависимости от шага).</param>
        /// <param name="b">Второй регистр (входное значение).</param>
        /// <param name="c">Третий регистр (входное значение).</param>
        /// <param name="d">Четвёртый регистр (входное значение).</param>
        /// <param name="x">Слово из 32-битного массива текущего 512-битного блока данных.</param>
        /// <param name="s">Количество битов для циклического сдвига влево.</param>
        private static void HH(ref uint a, uint b, uint c, uint d, uint x, byte s)
        {
            a += H(b, c, d) + x + 0x6ed9eba1;
            a = RotateLeft(a, s);
        }

        /// <summary>
        /// Выполняет циклический сдвиг 32-битного значения влево на заданное количество битов.
        /// Биты, которые "выходят" слева, возвращаются в младшие разряды справа.
        /// </summary>
        /// <param name="value">32-битное значение для сдвига.</param>
        /// <param name="count">Количество позиций для сдвига влево. Значение по модулю 32.</param>
        /// <returns>Результат циклического сдвига влево.</returns>
        private static uint RotateLeft(uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }
    }
}
