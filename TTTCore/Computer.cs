using System;
using MM.AI;

namespace TTTCore
{
    public class Computer : Player
    {
        private readonly string[] validNames = { 
            "GLaDOS", "Hal 9000", "Ava", "Cortana", "Alexa"
        };

        public override AI ai { get; set; }
        
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

        public override void SetPlayerToken(string invalidToken)
        {
            this.Token = invalidToken == "O" ? "X" : "O";
        }
    }
}
