using System;

namespace TTTCore
{
    public class Game
    {
        public int RoundsToWin { get; set; }
        public int NextPlayerNumber { get; set; }
        public bool GameOver { get; set; }
        public GameModes Mode { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        
        public void Play()
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");
        }

        public void SetGameMode(string gameModeNumber)
        {
            var mode = (GameModes)Enum.Parse(typeof(GameModes), gameModeNumber);
            
            if (Enum.IsDefined(typeof(GameModes), mode))
            {
                this.Mode = mode;
            }
            else
            {
                throw new ArgumentException("Invalid game mode selection.");
            }
        }

        public void SetRoundsToWin(string roundsToWinString)
        {  
            var value = System.Int32.Parse(roundsToWinString);
            
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

        public void SetFirstPlayer(string playerNumberString)
        {
            var value = System.Int32.Parse(playerNumberString);
            
            if (value == 1 || value == 2)
            {
                this.NextPlayerNumber = value;
            }
            else
            {
                throw new ArgumentException("Invalid first player selection.");
            }
        }
    }
}
