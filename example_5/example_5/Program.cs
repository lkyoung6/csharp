using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example_5
{
    public class Panda
    {
        public string name;
        public Panda (string n)
        {
            name = n;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Panda p = new Panda("keunha");
            Console.WriteLine(p.name);
        }
    }
}
