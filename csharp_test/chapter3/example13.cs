using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_13
{
    public class Stock
    {
        private int currentPrice;
        public int CurrentPrice
        {
            get { return this.currentPrice; }
            set { currentPrice = value; }
            }


        private int sharesOwned;
        public int SharesOwned
        {
            get { return this.sharesOwned; }
            set { sharesOwned = value; }
        }
        
        public decimal Worth => currentPrice * sharesOwned ;
    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Stock stock = new Stock();
    //        stock.CurrentPrice = 1000;

    //        stock.SharesOwned = 3;
    //        decimal totalPrice = stock.Worth;
    //        Console.WriteLine($"{totalPrice}");
    //    }
    //}
}
