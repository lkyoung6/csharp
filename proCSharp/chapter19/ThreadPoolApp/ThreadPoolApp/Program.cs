using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolApp
{
    public class Printer
    {
        private object lockToken = new object();

        public void PrintNumbers()
        {
            lock(lockToken)
            {
                Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.ManagedThreadId);

                Console.Write("Your numbers: ");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write("{0}, ", i);
                    //Thread.Sleep(1000);
                }
                Console.WriteLine();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with the CLR Thread Pool *****\n");

            Console.WriteLine("Main thread started. ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);

            Printer p = new Printer();

            WaitCallback workItem = new WaitCallback(PrintTheNumbers);

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(workItem, p);
            }

            Console.WriteLine("All tasks queued");
            Console.ReadLine();
        }

        static void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state;
            task.PrintNumbers();
        }
    }
}
