using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example_1
{
    class Octoppus
    {
        public string name;
        public int Age = 10;
        public static readonly int legs = 8,
                                    eyes=2;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Octoppus octoppus1 = new Octoppus();
            octoppus1.name = "keunha";
            Octoppus.legs = 10; //readonly이기 변수 값 변경할 수 없음
            Console.WriteLine(octoppus1.name);
            Console.WriteLine(octoppus1.Age);
        }
    }
}
