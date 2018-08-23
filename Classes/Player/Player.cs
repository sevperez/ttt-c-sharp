using System;

namespace PlayerClass
{
    public class Player
    {
        private string _name;
        private string _token;
        private int _numWins;

        // public string Name { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Token { get; set; }
        public int NumWins { get; set; }
    }
}
