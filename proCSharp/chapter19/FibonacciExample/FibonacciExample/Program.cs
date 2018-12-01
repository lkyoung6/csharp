using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FibonacciExample
{
    public class Fibonacci
    {
        private ManualResetEvent _doneEvent;

        public Fibonacci(int n, ManualResetEvent doneEvent)
        {
            N = n;
            _doneEvent = doneEvent;
        }

        public int N { get; }
        public int FibOfN { get; private set; }
        public void ThreadPoolCallback(object threadContext)
        {
            int threadIndex = (int)threadContext;
            Console.WriteLine($"Thread {threadIndex} started...");
            FibOfN = Calculate(N);
            Console.WriteLine($"Thread {threadIndex} reuslt calculated...");
            _doneEvent.Set();
        }

        private int Calculate(int n)
        {
            if ( n <= 1)
            {
                return n;
            }
            return Calculate(n - 1) + Calculate(n - 2);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            const int FibonacciCalculations = 5;

            var doneEvents = new ManualResetEvent[FibonacciCalculations];
            var fibArray = new Fibonacci[FibonacciCalculations];
            var rand = new Random();

            Console.WriteLine($"Launching {FibonacciCalculations} tasks...");
            for (int i = 0; i < FibonacciCalculations; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                var f = new Fibonacci(rand.Next(20, 40), doneEvents[i]);
                fibArray[i] = f;
                ThreadPool.QueueUserWorkItem(f.ThreadPoolCallback, i);
            }

            WaitHandle.WaitAll(doneEvents);
            Console.WriteLine("All calculations are complete.");

            for (int i = 0; i < FibonacciCalculations; i++)
            {
                Fibonacci f = fibArray[i];
                Console.WriteLine($"Fibonacci({f.N}) = {f.FibOfN}");
            }


        }
    }
}
