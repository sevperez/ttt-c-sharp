using System;

namespace TTTCore
{
    public class Computer : Player
    {
        private readonly string[] validNames = { 
            "GLaDOS", "Hal 9000", "Ava", "Cortana", "Alexa"
        };
        
        public string[] ValidNames
        { 
            get { return validNames; }
        }

        public void SetPlayerName()
        {
            var random = new Random();

            int index = random.Next(this.ValidNames.Length);

            this.Name = this.ValidNames[index];
        }

        public void SetPlayerToken(string humanToken)
        {
            if (humanToken == "O")
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
