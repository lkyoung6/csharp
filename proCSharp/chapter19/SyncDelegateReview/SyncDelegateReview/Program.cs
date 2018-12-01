using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncDelegateReview
{
    public delegate int BinaryOp(int x, int y);
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Async Delegate Invocation *****");

            Console.WriteLine("Main() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);
            IAsyncResult ar = b.BeginInvoke(10, 10, null, null);

            //while(!ar.IsCompleted)
            //{
            //    Console.WriteLine("Doing more work in Main()");
            //    Thread.Sleep(1000);
            //}
            while (!ar.AsyncWaitHandle.WaitOne(1000,true))
            {
                Console.WriteLine("Doing more work in Main()!");
            }


            int answer = b.EndInvoke(ar);
            Console.WriteLine("10 + 10 is {0}.", answer);
            
            Console.ReadLine();
           


        }

        private static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(5000);
            return x + y;
        }
    }
}
