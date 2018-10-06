using System;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = GenerateMatrix(5, 5);
            DrawMatrix(matrix);
            Console.WriteLine(new string('-', 40));

            int[] sumColumns = GetSumColum(matrix);
            DrawArray(sumColumns);
            GetMaxSum(sumColumns);

            Console.ReadKey();
        }

        public static void DrawMatrix(int[,] numbers)
        {
            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.Write($"{numbers[i, j]}\t");
                }
                Console.WriteLine();
            }

        }

        public static void DrawArray(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write($"{numbers[i]}\t");
            }
            Console.WriteLine();
        }

        public static int[,] GenerateMatrix(int x, int y, int maxRand = 10)
        {

            int[,] numbers = new int[x, y];
            Random rand = new Random();

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    numbers[i, j] = rand.Next(0, maxRand);
                }
            }

            return numbers;
        }

        public static int[] GetSumColum(int[,] numbers)
        {
            int[] sumColumns = new int[numbers.GetLength(1)];

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    sumColumns[j] += numbers[i, j];
                }
            }

            return sumColumns;
        }

        public static void GetMaxSum(int[] numbers)
        {
            int sumKey = 0;


            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > numbers[sumKey])
                {
                    sumKey = i;
                }
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == numbers[sumKey])
                {
                    Console.WriteLine($"Max sum in column # {i + 1}");
                }
            }
        }
    }
}
