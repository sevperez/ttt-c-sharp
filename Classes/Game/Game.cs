using System;

namespace GameClass
{
    public class Game
    {
        private string _greeting;
        
        public string Greeting { get; set; }

        public Game()
        {
            this.Greeting = "Hello!";
        }

        public bool returnsFalse()
        {
            return false;
            throw new NotImplementedException("not implemented!");
        }
    }
}
