using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Homework
{
    class Program
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);


        static void Main(string[] args)
        {
            #region 1.1 Сортировка массива
            {
                Console.WriteLine($"1.1 Сортировка массива\r\n------------------------");

                int[] numbers = { 1, -5, 22, -11, 7, 8, -34, 11, 9, 0 };
                int temp;

                Console.WriteLine($"Исходный: [{String.Join(", ", numbers)}]");

                for (int i = 0; i < numbers.Length; i++)
                {
                    for (int j = i + 1; j < numbers.Length; j++)
                    {
                        if (numbers[i] > numbers[j])
                        {
                            temp = numbers[i];
                            numbers[i] = numbers[j];
                            numbers[j] = temp;
                        }
                    }
                }

                Console.WriteLine($"Сортировка: [{String.Join(", ", numbers)}]\r\n");
            }
            #endregion

            #region 1.2 Работа со строками
            {
                string content = "18fdas99dsfadf88sfsdg9gffd11dfsgsd11fda6";
                Regex rx = new Regex(@"[0-9]+");
                MatchCollection matches = rx.Matches(content);
                List<int> numbers = new List<int>();

                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        numbers.Add(Int32.Parse(match.Value));
                    }
                }

                Console.WriteLine($"1.2 Работа со строками\r\n------------------------");
                Console.WriteLine($"Исходная строка: {content}");
                Console.WriteLine($"Сумма чисел:  {String.Join(" + ", numbers)} = {numbers.Sum()}\r\n");
            }
            #endregion

            #region 1.3 Двумерные массивы
            {
                Console.WriteLine($"1.3 Двумерные массивы\r\n------------------------");
                Console.WriteLine(" Исходный массив:\r\n");

                const int sizeNumbers = 10;
                const int randMin = 45;
                const int randMax = 210;

                int[,] numbers = new int[sizeNumbers, sizeNumbers];
                Random rand = new Random();

                for (int i = 0; i < sizeNumbers; i++)
                {
                    for (int j = 0; j < sizeNumbers; j++)
                    {
                        numbers[i, j] = rand.Next(randMin, randMax);
                        Console.Write($"{numbers[i, j]}\t");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("\r\n Лампа накаливания:\r\n");

                const int r = 255;
                int g;
                const int b = 0;

                var handle = GetStdHandle(-11);
                int mode;
                GetConsoleMode(handle, out mode);
                SetConsoleMode(handle, mode | 0x4);

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        g = 255 - numbers[i, j];
                        Console.Write("\x1b[48;2;" + r + ";" + g + ";" + b + "m        ", numbers[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            #endregion

            Console.ReadKey();
        }
    }
}
