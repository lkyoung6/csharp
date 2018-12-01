using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCallbackDelegate
{
    public delegate int BinaryOp(int x, int y);
    class Program
    {
        private static bool isDone = false;

        static void Main(string[] args)
        {
            Console.WriteLine("***** AsyncCallbackDelegate Example *****");
            Console.WriteLine("Main() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);
            IAsyncResult ar = b.BeginInvoke(10, 10, new AsyncCallback(AddComplete), null);

            while (!isDone)
            {
                Console.WriteLine("Working.....");
            }
            
        }

        private static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            return x + y;
        }
        static void AddComplete(IAsyncResult iar)
        {
            Console.WriteLine("AddComplete() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Your addition is complete");
            isDone = true;
        }
    }
}
