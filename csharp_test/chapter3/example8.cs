using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example_8
{
    public class Class1
    {
        public string name;
        Class1() { }
        public static Class1 Create(string name)
        {
            

            Class1  class1= new Class1();
            class1.name = name;
            return class1;
        }
    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Class1 c= Class1.Create("keunha");
    //        Console.WriteLine(c.name);
    //    }
    //}
}
