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
        public Board Board { get; set; }
        
        public void Play()
        {
            this.WelcomeScreen();
            this.GameSetup();
            this.PlayGame();
        }

        public void PlayGame()
        {
            while (this.RoundsToWin != this.Player1.NumWins &&
                   this.RoundsToWin != this.Player2.NumWins)
            {
                this.PlayRound();
                Thread.Sleep(1000);
            }

            string winnerName;
            if (this.Player1.NumWins == this.RoundsToWin)
            {
                winnerName = this.Player1.Name;
            }
            else
            {
                winnerName = this.Player2.Name;
            }

            this.ConsoleInterface.DrawGameEnd
            (
                this.Player1, this.Player2,
                this.RoundsToWin, this.Board, winnerName
            );
        }

        public void PlayRound()
        {
            this.Board = new Board();

            while (this.Board.GetWinningToken() == null && !this.Board.IsFull())
            {
                this.HandlePlayerMove();
            }

            var winningToken = this.Board.GetWinningToken();
            this.IncrementWinnerScore(winningToken);

            string winnerName = null;
            if (winningToken == Player1.Token)
            {
                winnerName = Player1.Name;
            }
            else if (winningToken == Player2.Token)
            {
                winnerName = Player2.Name;
            }

            this.ConsoleInterface.DrawRoundEnd
            (
                this.Player1, this.Player2,
                this.RoundsToWin, this.Board, winnerName
            );
        }

        public void IncrementWinnerScore(string winningToken)
        {
            if (winningToken == null) return;

            if (winningToken == this.Player1.Token)
            {
                this.Player1.NumWins += 1;
            }

            if (winningToken == this.Player2.Token)
            {
                this.Player2.NumWins += 1;
            }   
        }

        public void HandlePlayerMove()
        {
            if (this.NextPlayerNumber == 1)
            {
                this.ConsoleInterface.DrawMainScreen
                (
                    this.Player1, this.Player2,
                    this.RoundsToWin, this.Board, this.NextPlayerNumber
                );
                int moveSelection = 
                    this.ConsoleInterface.GetPlayerMoveSelection(this.Player1, this.Board);
                this.Board.Squares[moveSelection].Fill(this.Player1.Token);
                this.NextPlayerNumber = 2;
            }
            else if (this.NextPlayerNumber == 2 && this.Mode == GameModes.PlayerVsPlayer)
            {
                this.ConsoleInterface.DrawMainScreen
                (
                    this.Player1, this.Player2,
                    this.RoundsToWin, this.Board, this.NextPlayerNumber
                );
                int moveSelection = 
                    this.ConsoleInterface.GetPlayerMoveSelection(this.Player1, this.Board);
                this.Board.Squares[moveSelection].Fill(this.Player2.Token);
                this.NextPlayerNumber = 1;
            }
            else
            {
                int moveSelection =
                    this.Player2.ai.GetTopMoveIndex(this.Board, true);
                this.Board.Squares[moveSelection].Fill(this.Player2.Token);
                this.NextPlayerNumber = 1;
            }
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

            if (this.Mode == GameModes.PlayerVsComputer)
            {
                this.Player2.ai = new AI(this.Player2.Token, this.Player1.Token);
            }
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
