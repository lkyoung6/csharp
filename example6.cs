using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example_6
{
    public class Wine
    {
        public decimal Price;
        public int Year;
        public Wine (decimal price)
        {
            Price = price;
        }
        public Wine ( decimal price, int year) : this(price)
        {
            Year = year;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Wine wine = new Wine(20000, 1998);
            Console.WriteLine($"{wine.Year} { wine.Price}");

        }
    }
}
