using System;
using System.Threading;
using System.Threading.Tasks;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Random random = new System.Random();

            int n = 100;
            int[,] a = new int[n, n];
            int[,] b = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = random.Next();
                    b[i, j] = random.Next();
                }
            }
            
            int[,] r = new int[n, n];

            int myThreads = 4;
            int calculatedStrings = n / myThreads;

            Thread[] threadArr = new Thread[myThreads];
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i=0; i < myThreads; i++)
            {
                int start = i * calculatedStrings;
                int end = start + calculatedStrings - 1;
                threadArr[i] = new Thread(new ParameterizedThreadStart(s =>Mult(a, b, r, start, end, n)));
                threadArr[i].Start();
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Время многопоточного метода " + elapsedMs);


            for (int i = 0; i < threadArr.Length; i++)
            {
                threadArr[i].Join();
            }

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            
            Mult(a, b, r, 0, n-1, n);
            
            Console.WriteLine("Время однопоточного метода " + watch2.ElapsedMilliseconds);

            //Print(a);
            //Print(b);
            //Print(r);

            Console.ReadLine();
        }
        static void Mult(int[,] first, int[,] second, int[,] result, int start, int end, int size)
        {
            for (int i = start; i <= end; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        result[i, j] += first[i, k] * second[k, j];
                    }
                }
            }

        }

        static void Print(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    Console.Write("{0} ", arr[i, j]);
                }
                Console.WriteLine();

            }
        }
        
    }
}

