using System;
using PlayerClass;

namespace HumanClass
{
    public class Human : Player
    {
        public void SetPlayerName(string str)
        {
            string trimmedStr = str.Trim();

            if (trimmedStr.Length > 0)
            {
                Console.WriteLine(trimmedStr);
                this.Name = str;
            }
            else
            {
                throw new ArgumentException("name must be greater than 0 length, trimmed");
            }
        }
    }
}
