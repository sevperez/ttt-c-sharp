using System;

namespace TTTCore
{
    public class Computer : Player
    {
        private readonly string[] _validNames = { "GLaDOS", "Hal 9000", "Ava", "Cortana", "Alexa" };
        
        public string[] ValidNames
        { 
            get { return _validNames; }
        }

        public void SetPlayerName()
        {
            Random rnd = new Random();
            int r = rnd.Next(this.ValidNames.Length);
            this.Name = this.ValidNames[r];
        }

        public void SetPlayerToken(string humToken)
        {
            if (humToken == "O")
            {
                this.Token = "X";
            }
            else
            {
                this.Token = "O";
            }
        }
    }
}
