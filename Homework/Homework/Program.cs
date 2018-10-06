using System;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = GenerateMatrix(5, 5);
            DrawMatrix(matrix);
            GetSumEvenOdd(matrix);
            Console.WriteLine(new string('-', 40));

            int[] array = GenereteArray(5);
            DrawArray(array);
            GetSumEvenOdd(array);

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

        public static int[] GenereteArray(int size, int maxRand = 10)
        {
            int[] numbers = new int[size];
            Random rand = new Random();

            for (int i = 0; i < size; i++)
            {
                numbers[i] = rand.Next(0, maxRand);
            }
            return numbers;
        }

        public static void GetSumEvenOdd(int[] numbers)
        {
            int sumOdd = 0;
            int sumEven = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    sumEven += numbers[i];
                }
                else
                {
                    sumOdd += numbers[i];
                }
            }

            Console.WriteLine($"even : {sumEven}");
            Console.WriteLine($"odd: {sumOdd}");
        }

        public static void GetSumEvenOdd(int[,] numbers)
        {
            int sumOdd = 0;
            int sumEven = 0;

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (numbers[i, j] % 2 == 0)
                    {
                        sumEven += numbers[i, j];
                    }
                    else
                    {
                        sumOdd += numbers[i, j];
                    }
                }
            }

            Console.WriteLine($"even : {sumEven}");
            Console.WriteLine($"odd: {sumOdd}");
        }
    }
}
