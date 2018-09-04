using System;

namespace TTTCore
{
    public class Computer : Player
    {
        private readonly string[] validNames = { 
            "GLaDOS", "Hal 9000", "Ava", "Cortana", "Alexa"
        };

        public AI ai { get; set; }
        
        public string[] ValidNames
        { 
            get { return validNames; }
        }

        public override void SetPlayerName()
        {
            var random = new Random();

            int index = random.Next(this.ValidNames.Length);

            this.Name = this.ValidNames[index];
        }

        public override void SetPlayerToken(string humanToken)
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
