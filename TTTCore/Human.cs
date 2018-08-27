using System;

namespace TTTCore
{
    public class Human : Player
    {
        public void SetPlayerName(string str)
        {
            string trimmed = str.Trim();

            if (trimmed.Length > 0)
            {
                this.Name = trimmed;
            }
            else
            {
                throw new ArgumentException("name must be greater than 0 length, trimmed");
            }
        }

        public void SetPlayerToken(string str)
        {
            string trimmed = str.Trim();

            if (trimmed.Length == 1)
            {
                this.Token = trimmed;
            }
            else
            {
                throw new ArgumentException("token must be greater 1 length");
            }
        }
    }
}
