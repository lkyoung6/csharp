using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_15
{
    public class Foo
    {
        private decimal x;
        public decimal X
        {
            get { return x; }
            private set { x = Math.Round(value, 2); }
        }
        public decimal Round(decimal x)
        {
            X = x;
            return X;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Foo foo = new Foo();
            foo.Round(4.535m);
            Console.WriteLine($"{foo.X}");
        }
    }
}
