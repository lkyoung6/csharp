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
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Foo foo = new Foo();
    //        foo.Round(4.535m);
    //        //foo.X = 5.35m;// private이 set 앞에 있어 접근 불가
    //        Console.WriteLine($"{foo.X}");
    //    }
    //}
}
