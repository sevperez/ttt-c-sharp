using System;
using PlayerClass;
using HumanClass;
using ComputerClass;

namespace GameClass
{
    public class Game
    {
        private int _roundsToWin;
        private bool _gameOver;
        private bool _vsComputer;
        private Player player1;
        private Player player2;
        
        public int RoundsToWin 
        {
            get { return _roundsToWin; }
            set { _roundsToWin = value; }
        }
        public bool GameOver { get; set; }
        public bool VsComputer { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

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

        public void InstantiatePlayers(bool vsComp)
        {
            this.Player1 = new Human();

            if (vsComp)
            {
                this.Player2 = new Computer();
            }
            else
            {
                this.Player2 = new Human();
            }
        }
    }
}
