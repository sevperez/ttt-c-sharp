using System;
using System.Threading;

namespace TTTCore
{
    public class Game
    {
        private CLI ConsoleInterface = new CLI();
        public int RoundsToWin { get; set; }
        public int NextPlayerNumber { get; set; }
        public bool GameOver { get; set; }
        public GameModes Mode { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        
        public void Play()
        {
            this.WelcomeScreen();
            this.GameSetup();
            this.PlayRound();
        }

        public void PlayRound()
        {
            string[] tokens = new string[] {
                "X", "", "O", "O", "", "X", "X", "", "O"
            };

            this.ConsoleInterface.DrawMainScreen
            (
                this.Player1, this.Player2, this.RoundsToWin, tokens
            );
        }

        public void WelcomeScreen()
        {
            Console.Clear();
            ConsoleInterface.WelcomeMessage();
            Thread.Sleep(1000);
        }

        public void GameSetup()
        {
            this.HandleGameModeSetup();
            this.InstantiatePlayers();
            this.HandlePlayerNameSetup();
            this.HandlePlayerTokenSetup();
            this.HandleNumRoundsSetup();
            this.HandleFirstPlayerChoice();
        }

        public void HandleGameModeSetup()
        {
            Console.Clear();
            this.SetGameMode(ConsoleInterface.GetGameModeSelection());
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

        public void InstantiatePlayers()
        {
            this.Player1 = new Human();

            if (this.Mode == GameModes.PlayerVsComputer)
            {
                this.Player2 = new Computer();
            }
            else
            {
                this.Player2 = new Human();
            }
        }

        public void HandlePlayerNameSetup()
        {
            Console.Clear();
            var name1 = ConsoleInterface.GetPlayerNameSelection(1);
            this.Player1.SetPlayerName(name1);
            var invalidName = this.Player1.Name;

            if (this.Mode == GameModes.PlayerVsPlayer)
            {
                Console.Clear();
                var name2 = ConsoleInterface.GetPlayerNameSelection(2, invalidName);
                this.Player2.SetPlayerName(name2);
            }
            else
            {
                this.Player2.SetPlayerName();
            }
        }

        public void HandlePlayerTokenSetup()
        {
            Console.Clear();
            var token1 = ConsoleInterface.GetPlayerTokenSelection(1);
            this.Player1.SetPlayerToken(token1);
            var invalidToken = this.Player1.Token;

            if (this.Mode == GameModes.PlayerVsPlayer)
            {
                Console.Clear();
                var token2 = ConsoleInterface.GetPlayerTokenSelection(2, invalidToken);
                this.Player2.SetPlayerToken(token2);
            }
            else
            {
                this.Player2.SetPlayerToken(invalidToken);
            }
        }

        public void HandleNumRoundsSetup()
        {
            Console.Clear();
            this.SetRoundsToWin(ConsoleInterface.GetRoundsToWinSelection());
        }

        public void SetRoundsToWin(int roundsToWin)
        {  
            if (roundsToWin >= 1 && roundsToWin <= 9)
            {
                this.RoundsToWin = roundsToWin;
            }
            else
            {
                throw new ArgumentException("Rounds to win must be in range 1..9");
            }
        }

        public void HandleFirstPlayerChoice()
        {
            Console.Clear();
            int choice = (ConsoleInterface.GetFirstPlayerSelection(this.Player1, this.Player2));
            this.SetFirstPlayer(choice);
        }

        public void SetFirstPlayer(int playerNumber)
        {
            if (playerNumber == 1 || playerNumber == 2)
            {
                this.NextPlayerNumber = playerNumber;
            }
            else
            {
                throw new ArgumentException("Invalid first player selection.");
            }
        }
    }
}
