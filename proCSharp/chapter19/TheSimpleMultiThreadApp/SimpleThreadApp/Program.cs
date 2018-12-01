using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleMultiThreadApp
{
    public class Printer
    {
        public void PrintNumbers()
        {
            Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name);

            Console.WriteLine("Your numbers:");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0}", i);
                Thread.Sleep(2000);
            }
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** The Amazing Thread App *****\n");
            Console.WriteLine("Do you want [1] or [2] threads? ");
            string threadCount = Console.ReadLine();

            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary";

            Console.WriteLine("->{0} is ececuting Main()", Thread.CurrentThread.Name);
            Printer p = new Printer();

            switch (threadCount)
            {
                case "2":
                    Thread backgourndThread = new Thread(new ThreadStart(p.PrintNumbers));
                    backgourndThread.Name = "Secondary";
                    backgourndThread.Start();
                    break;
                case "1":
                    p.PrintNumbers();
                    break;

                default:
                    Console.WriteLine("I don't know what you want...you get 1 thread.");
                    goto case "1";
            }

            MessageBox.Show("I'm busy!", "Work on main thread...");
            Console.ReadLine();
        }
    }
}
