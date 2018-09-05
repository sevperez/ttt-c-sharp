using System;
using System.Collections;
using System.Linq;
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
            while (!this.CheckGameOver())
            {
                this.PlayRound();
                Thread.Sleep(1000);
            }
            
            this.HandleGameEnd();
        }

        public bool CheckGameOver()
        {
            var gameOver = this.RoundsToWin == this.Player1.NumWins ||
                this.RoundsToWin == this.Player2.NumWins;
            
            return gameOver;
        }

        public void HandleGameEnd()
        {
            string winnerName = this.GetGameWinnerName();

            this.ConsoleInterface.DrawGameEnd
            (
                this.Player1, this.Player2,
                this.RoundsToWin, this.Board, winnerName
            );
        }

        public string GetGameWinnerName()
        {
            if (this.Player1.NumWins == this.RoundsToWin)
            {
                return this.Player1.Name;
            }
            else
            {
                return this.Player2.Name;
            }
        }

        public void PlayRound()
        {
            this.Board = new Board();

            while (!this.CheckRoundOver())
            {
                this.HandlePlayerMoves();
            }

            this.HandleRoundEnd();
        }

        public bool CheckRoundOver()
        {
            if (this.GetWinningToken() != null || this.Board.IsFull())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HandleRoundEnd()
        {
            var winningToken = this.GetWinningToken();
            var winnerName = this.GetRoundWinnerName(winningToken);

            this.IncrementWinnerScore(winningToken);

            this.ConsoleInterface.DrawRoundEnd
            (
                this.Player1, this.Player2,
                this.RoundsToWin, this.Board, winnerName
            );
        }

        public string GetWinningToken()
        {
            for (var i = 0; i < Constants.WinningLines.GetLength(0); i += 1)
            {
                ArrayList lineTokens = new ArrayList();
                for (var j = 0; j < Constants.WinningLines.GetLength(1); j += 1)
                {
                    var index = Constants.WinningLines[i, j];
                    lineTokens.Add(this.Board.Squares[index].CurrentToken);
                }

                string[] tokenStrings = (string[])lineTokens.ToArray(typeof(string));
                if (this.IsWinningLine(tokenStrings))
                {
                    return tokenStrings[0];
                }
            }

            return null;
        }

        public bool IsWinningLine(string[] line)
        {
            return line.All(token => line[0] != "" && token == line[0]);
        }

        public string GetRoundWinnerName(string winningToken)
        {
            if (winningToken == Player1.Token)
            {
                return Player1.Name;
            }
            else if (winningToken == Player2.Token)
            {
                return Player2.Name;
            }
            else
            {
                return null;
            }
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

        public void HandlePlayerMoves()
        {
            this.HandlePlayerMoveAction();
            this.AlternateNextPlayer();
        }

        public void HandlePlayerMoveAction()
        {
            var currentPlayer = this.NextPlayerNumber == 1 ? this.Player1 : this.Player2;

            int moveSelection;
            if (currentPlayer == this.Player2 && this.Mode == GameModes.PlayerVsComputer)
            {
                moveSelection = this.HandleComputerMoveAction(currentPlayer);
            }
            else
            {
                moveSelection = this.HandleHumanMoveAction(currentPlayer);
            }
            
            this.Board.Squares[moveSelection].Fill(currentPlayer.Token);
        }

        public int HandleHumanMoveAction(Player currentPlayer)
        {
            this.ConsoleInterface.DrawMainScreen
            (
                this.Player1, this.Player2, this.RoundsToWin, 
                this.Board, this.NextPlayerNumber
            );
            return this.ConsoleInterface.GetPlayerMoveSelection(currentPlayer, this.Board);
        }

        public int HandleComputerMoveAction(Player currentPlayer)
        {
            return currentPlayer.ai.GetTopMoveIndex(this.Board, true);
        }

        public void AlternateNextPlayer()
        {
            if (this.NextPlayerNumber == 1)
            {
                this.NextPlayerNumber = 2;
            }
            else
            {
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
            this.HandlePlayerNamesSetup();
            this.HandlePlayerTokensSetup();
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

        public void HandlePlayerNamesSetup()
        {
            this.HandleHumanPlayerNameSetup(1);

            if (this.Mode == GameModes.PlayerVsPlayer)
            {
                this.HandleHumanPlayerNameSetup(2, this.Player1.Name);
            }
            else
            {
                this.Player2.SetPlayerName();
            }
        }

        public void HandleHumanPlayerNameSetup(int playerNumber, string invalidName = "")
        {
            var currentPlayer = playerNumber == 1 ? this.Player1 : this.Player2;

            Console.Clear();
            var name = ConsoleInterface.GetPlayerNameSelection(playerNumber, invalidName);
            currentPlayer.SetPlayerName(name);
        }

        public void HandlePlayerTokensSetup()
        {
            this.HandleHumanPlayerTokenSetup(1);

            if (this.Mode == GameModes.PlayerVsPlayer)
            {
                this.HandleHumanPlayerTokenSetup(2, this.Player1.Token);
            }
            else
            {
                this.Player2.SetPlayerToken(this.Player1.Token);
            }
        }

        public void HandleHumanPlayerTokenSetup(int playerNumber, string invalidToken = "")
        {
            var currentPlayer = playerNumber == 1 ? this.Player1 : this.Player2;

            Console.Clear();
            var token = ConsoleInterface.GetPlayerTokenSelection(playerNumber, invalidToken);
            currentPlayer.SetPlayerToken(token);
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
