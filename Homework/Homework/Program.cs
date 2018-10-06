using System;

namespace Homework
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[,] matrix = GenereteMatrix(10, 10);
            DrawMatrix(matrix);

            Console.WriteLine(new string('-', 40));
            int[] matrixSortValues = SortArray(GetMatrixToArray(matrix));

            //DrawArray(matrixSortValues);
            //Console.WriteLine(new string('-', 40));

            GetValuesByIndex(matrixSortValues, matrix, 10);

            Console.ReadKey();
        }

        public static int[,] GenereteMatrix(int x, int y, int maxRand = 100)
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

        public static void DrawMatrix(int[,] numbers)
        {
            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.Write("{0}\t", numbers[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static int[] GetMatrixToArray(int[,] numbers)
        {
            int[] values = new int[numbers.GetLength(1) * numbers.GetLength(0)];

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    int _index = i * numbers.GetLength(0) + j;
                    values[_index] = numbers[i, j];
                }
            }

            return values;
        }

        public static int[] SortArray(int[] numbers)
        {

            int tmp = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[i] > numbers[j])
                    {
                        tmp = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = tmp;
                    }
                }
            }

            return numbers;
        }

        public static void DrawArray(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write("{0}\t", numbers[i]);
            }
            Console.WriteLine();
        }

        public static void GetValuesByIndex(int[] values, int[,] matrix, int maxCount = 0)
        {

            maxCount = maxCount != 0 && maxCount < values.Length ? maxCount : values.Length;
            string i_j = null;
            int counter = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    for (int k = 0; k < maxCount; k++)
                    {
                        if (values[k] == matrix[i, j])
                        {
                            if (counter < maxCount && (i_j == null || i_j != i.ToString() + "_" + j.ToString()))
                            {
                                ++counter;
                                i_j = i.ToString() + "_" + j.ToString();
                                Console.WriteLine($"minValue #{counter} : index[{i},{j}], value: {matrix[i, j]}");
                            }
                        }
                    }
                }
            }
        }
    }
}
