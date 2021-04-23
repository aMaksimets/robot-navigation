using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class stringInt
    {
        private string stringToDivide;
        public stringInt(string str)
        {
            stringToDivide = str;
        }

        public List<int> getIntFromString()
        {
            string[] _ = Regex.Split(stringToDivide, @"\D+");

            List<int> listInt = new List<int>();

            foreach (string i in _)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    int j = int.Parse(i);
                    listInt.Add(j);
                }
            }

            return listInt;
        }
    }
}
