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

            Thread[] threadArr = new Thread[n];
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i=0; i < threadArr.Length; i++)
            {
                int row = i;
                threadArr[i] = new Thread(new ParameterizedThreadStart(s =>Mult(a, b, r, row)));
                threadArr[i].Name = string.Format("Thread {0} :", i + 1);
                threadArr[i].Start();
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Время многопоточного метода " + elapsedMs);


            for (int i = 0; i < threadArr.Length; i++)
            {
                threadArr[i].Join();
            }

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < a.GetLength(0); i++)
            {
                Mult(a, b, r, i);
            }
            Console.WriteLine("Время однопоточного метода " + watch2.ElapsedMilliseconds);

            /*Print(a);
            Print(b);
            Print(r);*/

            Console.ReadLine();
        }
        static void Mult(int[,] first, int[,] second, int[,] result, int i)
        {
            for (int j = 0; j < first.GetLength(0); j++)
            {
                for (int k = 0; k < second.GetLength(0); k++)
                {
                    result[i, j] += first[i, k] * second[k, j];
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

