using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_14
{
    public class Stock
    {
        public decimal CurrentPrice { get; set; } = 123;
        public int Maximum { get; } = 999;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock();
            Console.WriteLine($"{stock.CurrentPrice} {stock.Maximum}");

        }
    }
}
