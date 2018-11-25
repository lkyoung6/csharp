using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example_4
{
    public class example_4
    {
        public int Foo(int x) { return x; }
        public int Foo (ref int x) { return x; }
        //void Foo(out int x) { } //ref랑 out은 오버로딩 적용 안됨
    }
    class Program
    {
        static void Main(string[] args)
        {
            example_4 e = new example_4();

            Console.WriteLine(e.Foo(3));
            Console.WriteLine(e.Foo(4));
        }
    }
}
