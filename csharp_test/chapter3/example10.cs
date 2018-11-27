using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_10
{
    public class Panda
    {
        public Panda Mate;
        public string name;

        public Panda(string name)
        {
            this.name = name;
        }

        public void Marry(Panda partner)
        {
            Mate = partner;
            partner.Mate = this;
        }
    }
    
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Panda panda = new Panda("Kim");
    //        Panda panda2 = new Panda("Lee");

    //        panda.Marry(panda2);
    //        panda2.Marry(panda);

    //        Console.WriteLine($"panda1's mate is {panda.Mate.name}");
    //        Console.WriteLine($"panda2's mate is {panda2.Mate.name}");



    //    }
    //}
}
