using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example_3
{
    class FooExapmle
    {
        public int Foo (int x)
        {
            return x * 2;
        }
        public int Foo2(int x) => x * 2;
        public void Foo3(int x) => Console.WriteLine (x);
    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        FooExapmle  fooExample= new FooExapmle();
    //        int a = fooExample.Foo(2);
    //        int b = fooExample.Foo2(3);
            

    //        Console.WriteLine(a);
    //        Console.WriteLine(b);
    //         fooExample.Foo3(4);


    //    }
    //}
}
