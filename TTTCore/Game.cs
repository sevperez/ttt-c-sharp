using System;

namespace TTTCore
{
    public class Game
    {
        private int _roundsToWin;
        private bool _gameOver;
        private GameModes _mode;
        private Player player1;
        private Player player2;
        
        public int RoundsToWin 
        {
            get { return _roundsToWin; }
            set { _roundsToWin = value; }
        }
        public bool GameOver { get; set; }
        public GameModes Mode { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        
        public void Play()
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");
        }

        public void SetGameMode(string input)
        {
            GameModes mode = (GameModes)Enum.Parse(typeof(GameModes), input);
            if (Enum.IsDefined(typeof(GameModes), mode))
            {
                this.Mode = mode;
            }
            else
            {
                throw new ArgumentException("Invalid game mode selection.");
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
                throw new ArgumentException("Rounds to win must be in range 1..9");
            }
        }

        public void InstantiatePlayers(GameModes mode)
        {
            this.Player1 = new Human();

            if (mode == GameModes.PlayerVsComputer)
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
