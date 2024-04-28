using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int matrixSize = 1000; // Rozmiar macierzy

        // Inicjalizacja macierzy
        int[,] matrixA = GenerateRandomMatrix(matrixSize, matrixSize);
        int[,] matrixB = GenerateRandomMatrix(matrixSize, matrixSize);
        int[,] resultMatrix = new int[matrixSize, matrixSize];


        // Pomiar czasu dla obliczeń sekwencyjnych
        WrapperParallel(matrixA, matrixB, resultMatrix, 1);
        //PrintMatrix(resultMatrix);

        // Pomiar czasu dla obliczeń równoległych
        WrapperParallel(matrixA, matrixB, resultMatrix, 2);
        //PrintMatrix(resultMatrix);
    }

    static float WrapperParallel(int[,] matrixA, int[,] matrixB, int[,] resultMatrix, int threadCount)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        MultiplyMatricesParallel(matrixA, matrixB, resultMatrix, threadCount);
        stopwatch.Stop();
        Console.WriteLine($"Czas obliczeń dla {threadCount} wątków: {stopwatch.ElapsedMilliseconds} ms");
        return stopwatch.ElapsedMilliseconds;
    }

    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i, j]} ");
            }
            Console.WriteLine();
        }
    }   

    static int[,] GenerateRandomMatrix(int rows, int cols)
    {
        Random random = new Random();
        int[,] matrix = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(10); // Losowe wartości dla uproszczenia
            }
        }
        return matrix;
    }

    
    static void MultiplyMatricesParallel(int[,] matrixA, int[,] matrixB, int[,] resultMatrix, int threadCount)
    {
        int rowsA = matrixA.GetLength(0);
        int colsA = matrixA.GetLength(1);
        int colsB = matrixB.GetLength(1);

        // Tablica wątków
        Thread[] threads = new Thread[threadCount];

        // Rozdzielenie pracy na wątki
        int rowsPerThread = rowsA / threadCount;
        for (int i = 0; i < threadCount; i++)
        {
            int startRow = i * rowsPerThread;
            int endRow = (i == threadCount - 1) ? rowsA : startRow + rowsPerThread;
            threads[i] = new Thread(() =>
            {
                for (int row = startRow; row < endRow; row++)
                {
                    for (int colB = 0; colB < colsB; colB++)
                    {
                        int value = 0;
                        for (int colA = 0; colA < colsA; colA++)
                        {
                            value += matrixA[row, colA] * matrixB[colA, colB];
                        }
                        resultMatrix[row, colB] = value;
                    }
                }
            });
            threads[i].Start();
        }

        // Oczekiwanie na zakończenie wszystkich wątków
        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
}
