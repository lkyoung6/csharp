using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example_3
{
    class Example3
    {
        int Foo(int x)
        {
            return x;
        }
        double Foo(double x)
        {
            return x;
        }
        double Foo(int x, float y)
        {
            return x + y;
        }
        void Foo2(int x) { }
        //float Foo2 (int x) { } //리턴타입만 다르면 컴파일 에러
        void Goo(int[] x) { }
        //void Goo (params int[] x) { } //Param는 오버로딩 안됨

        //class Program
        //{
        //    static void Main(string[] args)
        //    {
        //        Example3 example = new Example3();
        //        Console.WriteLine(example.Foo(3));
        //        Console.WriteLine(example.Foo(3.5));
        //        Console.WriteLine(example.Foo(3,1));
        //    }
        //}
    }
}
