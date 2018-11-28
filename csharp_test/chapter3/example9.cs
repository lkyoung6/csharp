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
        public Bunny(string n) { Name = n; }


        static void Main(string[] args)
        {
            //Bunny b1 = new Bunny { Name = "Bo", LikesCarrots = true, LikesHumans = false };
            //Bunny b2 = new Bunny ("Bo") {  LikesCarrots = true, LikesHumans = false };

            Bunny temp1 = new Bunny();
            temp1.Name = "Bo";          //temp1의 name 값을 "Bo" 로 설정
            temp1.LikesCarrots = true;  //temp1의 LikesCarrot 값을  true 로 설정
            temp1.LikesHumans = false;  //temp1의  LikesHumans 값을 false 로 설정

            Bunny b1 = temp1;

            Bunny temp2 = new Bunny("Bo"); // temp2의 name 값을 "Bo" 로 설정
            temp2.LikesCarrots = true;     //temp2의 LikesCarrot 값을  true 로 설정
            temp2.LikesHumans = false;     //temp2의  LikesHumans 값을 false 로 설정
            Bunny b2 = temp2;              //temp2의  name,LikesCarrot,LikesHumans 값이 b2로 옮겨짐

            Console.WriteLine($"{temp1.Name} {temp1.LikesCarrots} {temp1.LikesHumans}");  //Bo true false
            Console.WriteLine($"{temp2.Name} {temp2.LikesCarrots} {temp2.LikesHumans}");  //Bo true false
            Console.WriteLine($"{b2.Name} {b2.LikesCarrots} {b2.LikesHumans}");           //Bo true false   
        }

    }
}
