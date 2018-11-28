using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_12
{
    public class Stock
    {
        public decimal currentPrice, sharedOwned;

        public decimal Worth
        {
            get { return currentPrice * sharedOwned; }
        }

    }
    
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Stock stock = new Stock();
    //        stock.currentPrice = 10;
    //        stock.sharedOwned = 3;
    //        Console.WriteLine($"{stock.Worth}");
    //    }
    //}
}
