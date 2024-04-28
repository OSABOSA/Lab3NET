using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Przykładowe macierze
        double[,] A = GenerateRandomMatrix(1000, 1000);
        double[,] B = GenerateRandomMatrix(1000, 1000);

        // Wynik mnożenia macierzy
        double[,] result = MultiplyMatrices(A, B, 8);

        // Wyświetlenie wyniku
        //PrintMatrix(result);
    }

    static double[,] MultiplyMatrices(double[,] A, double[,] B, int processCount)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int rowsA = A.GetLength(0);
        int colsA = A.GetLength(1);
        int colsB = B.GetLength(1);
        double[,] result = new double[rowsA, colsB];

        ParallelOptions options = new ParallelOptions();
        options.MaxDegreeOfParallelism = processCount;

        Parallel.For(0, rowsA, options, i =>
        {
            for (int j = 0; j < colsB; j++)
            {
                double sum = 0;
                for (int k = 0; k < colsA; k++)
                {
                    sum += A[i, k] * B[k, j];
                }
                result[i, j] = sum;
            }
        });

        Console.WriteLine($"Czas obliczeń dla {processCount} procesów: {stopwatch.ElapsedMilliseconds} ms");

        return result;
    }

    static double[,] GenerateRandomMatrix(int rows, int cols)
    {
        Random rand = new Random();
        double[,] matrix = new double[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = rand.NextDouble();
            }
        }
        return matrix;
    }

    static void PrintMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
