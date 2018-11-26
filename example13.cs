using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_13
{
    public class Stock
    {
        public int currentPrice;
        public int sharesOwned;

        public decimal Worth => currentPrice * sharesOwned ;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock();
            stock.currentPrice = 1000;

            stock.sharesOwned = 3;
            decimal totalPrice = stock.Worth;
            Console.WriteLine($"{totalPrice}");
        }
    }
}
