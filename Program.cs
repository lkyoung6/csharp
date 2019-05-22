using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisterMySolution2
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = "cookie";
            string s2 = "coookie";
            //string s2 = "cokie";
            //string s2 = "cookoie";

            bool result = Solution(s1, s2);

        }

        public static bool Solution(string s1, string s2)
        {
            List<string> s1StringList = GetStringList(s1);
            List<string> s2StringList = GetStringList(s2);

            return CompareTwoLists(s1StringList, s2StringList);
            
        }

        private static bool CompareTwoLists(List<string> s1StringList, List<string> s2StringList)
        {
            if (s1StringList.Count() == s2StringList.Count())
            {
                for (int i = 0; i < s1StringList.Count(); i++)
                {
                    if (!s2StringList[i].Contains(s1StringList[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private static List<string> GetStringList(string s1)
        {
            List<String> s1StringList = new List<String>();
            for (int s1StartCursor = 0; s1StartCursor < s1.Length; s1StartCursor++)
            {
                for (int s1MovingCursor = s1StartCursor + 1; s1MovingCursor < s1.Length + 1; s1MovingCursor++)
                {
                    if (s1MovingCursor < s1.Length && s1[s1StartCursor] != s1[s1MovingCursor])
                    {
                        StringBuilder sb = new StringBuilder();

                        for (int i = s1StartCursor; i < s1MovingCursor; i++)
                        {
                            char s = s1[i];
                            sb.Append(s);
                        }

                        for (int i = s1StartCursor; i < s1MovingCursor - 1; i++)
                        {
                            s1StartCursor++;
                        }
                        s1StringList.Add(sb.ToString());
                        break;
                    }
                    else if (s1MovingCursor == s1.Length)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = s1StartCursor; i < s1MovingCursor; i++)
                        {

                            char s = s1[i];
                            sb.Append(s);
                        }
                        s1StringList.Add(sb.ToString());
                        for (int i = s1StartCursor; i < s1MovingCursor - 1; i++)
                        {
                            s1StartCursor++;
                        }

                    }
                }

            }
            return s1StringList;
        }
    }
}
