using System;

namespace GameClass
{
    public class Game
    {
        private int _roundsToWin;
        private bool _gameOver;
        private bool _vsComputer;
        
        public int RoundsToWin 
        {
            get { return _roundsToWin; }
            set { _roundsToWin = value; }
        }
        public bool GameOver { get; set; }

        public bool VsComputer { get; set; }

        public void Play()
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");
        }

        public void SetGameType(int value)
        {
            if (value == 1)
            {
                this.VsComputer = false;
            }
            else if (value == 2)
            {
                this.VsComputer = true;
            }
            else
            {
                throw new ArgumentException("game type selection must be in range 1..2");
            }
        }

        public void SetRoundsToWin(int value)
        {
            if (value >= 1 && value <= 9)
            {
                this.RoundsToWin = value;
            }
            else
            {
                throw new ArgumentException("rounds to win must be in range 1..9");
            }
        }
    }
}
