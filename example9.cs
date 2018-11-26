using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example_9
{
    public class Bunny
    {
        public string Name;
        public bool LikesCarrots;
        public bool LikesHumans;

        public Bunny() { }
        public Bunny (string n) { Name = n; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Bunny b1 = new Bunny { Name = "Bo", LikesCarrots = true, LikesHumans = false };
            //Bunny b2 = new Bunny ("Bo") {  LikesCarrots = true, LikesHumans = false };

            Bunny temp1 = new Bunny();
            temp1.Name = "Bo";
            temp1.LikesCarrots = true;
            temp1.LikesHumans = false;

            Bunny b1 = temp1;

            Bunny temp2 = new Bunny("Bo");
            temp2.LikesCarrots = true;
            temp2.LikesHumans = false;
            Bunny b2 = temp2;

            Console.WriteLine($"{temp1.Name} {temp1.LikesCarrots} {temp1.LikesHumans}");
            Console.WriteLine($"{temp2.Name} {temp2.LikesCarrots} {temp2.LikesHumans}");

        }
    }
}
