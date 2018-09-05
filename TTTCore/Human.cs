using System;

namespace TTTCore
{
    public class Human : Player
    {
        public override void SetPlayerName(string inputString)
        {
            var trimmed = inputString.Trim();

            if (trimmed.Length > 0)
            {
                this.Name = trimmed;
            }
            else
            {
                throw new ArgumentException("Name must be greater than 0 length.");
            }
        }

        public override void SetPlayerToken(string inputString)
        {
            var trimmed = inputString.Trim();

            if (trimmed.Length == 1)
            {
                this.Token = trimmed;
            }
            else
            {
                throw new ArgumentException("Token must be greater 1 length.");
            }
        }
    }
}
