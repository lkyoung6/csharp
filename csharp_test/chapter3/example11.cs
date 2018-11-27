using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_11
{
    public class Stock
    {
        decimal currentPrice;
        public decimal CurrentPrice
        {
            get { return currentPrice; }
            set { currentPrice = value; }
        }

    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Stock msft = new Stock();
    //        msft.CurrentPrice = 30;
    //        msft.CurrentPrice -= 3;
    //        Console.WriteLine(msft.CurrentPrice);
    //    }

    //}
}
