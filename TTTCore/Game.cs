using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TTTCore
{
    public class Game
    {
        public IGameInterface GameInterface;
        public int RoundsToWin { get; set; }
        public int NextPlayerNumber { get; set; }
        public int BoardSizeSelection { get; set; }
        public bool GameOver { get; set; }
        public GameModes Mode { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Board Board { get; set; }
        
        public Game(IGameInterface gameInterface = null)
        {
            if (gameInterface == null)
            {
                this.GameInterface = new CLI();
            }
            else
            {
                this.GameInterface = gameInterface;
            }
        }

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

            this.GameInterface.DrawGameEnd
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
            this.Board = new Board(this.BoardSizeSelection);

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

            this.GameInterface.DrawRoundEnd
            (
                this.Player1, this.Player2,
                this.RoundsToWin, this.Board, winnerName
            );
        }

        public string GetWinningToken()
        {
            int[,] winningLines = this.GetWinningLines();
            for (var i = 0; i < winningLines.GetLength(0); i += 1)
            {
                ArrayList lineTokens = new ArrayList();
                for (var j = 0; j < winningLines.GetLength(1); j += 1)
                {
                    var index = winningLines[i, j];
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

        public int[,] GetWinningLines()
        {
            var boardSize = this.Board.BoardSize;
            var winningLineLists = new List<int[]>();

            this.AddHorizontalWinningLines(boardSize, winningLineLists);
            this.AddVerticalWinningLines(boardSize, winningLineLists);
            this.AddDiagonalWinningLines(boardSize, winningLineLists);

            return this.ConvertWinningLineListsToMDArray(winningLineLists, boardSize);
        }

        public int[,] ConvertWinningLineListsToMDArray(List<int[]> list, int boardSize)
        {
            int[,] mdArray = new int[boardSize * 2 + 2, boardSize];

            for (var i = 0; i < list.Count; i += 1)
            {
                for (var j = 0; j < list[0].Length; j += 1)
                {
                    mdArray[i, j] = list[i][j];
                }
            }

            return mdArray;
        }

        public void AddHorizontalWinningLines(int boardSize, List<int[]> lines)
        {
            for (var i = 0; i < boardSize; i += 1)
            {
                var currentLine = new List<int>();
                for (var j = 0; j < boardSize; j += 1)
                {
                    currentLine.Add(j + i * boardSize);
                }

                lines.Add((int[])currentLine.ToArray());
            }
        }

        public void AddVerticalWinningLines(int boardSize, List<int[]> lines)
        {
            for (var i = 0; i < boardSize; i += 1)
            {
                var currentLine = new List<int>();
                for (var j = 0; j < boardSize; j += 1)
                {
                    currentLine.Add(i + j * boardSize);
                }

                lines.Add((int[])currentLine.ToArray());
            }
        }

        public void AddDiagonalWinningLines(int boardSize, List<int[]> lines)
        {
            var diagonalLeft = new List<int>();
            var diagonalRight = new List<int>();

            for (var i = 0; i < boardSize; i += 1)
            {
                for (var j = 0; j < boardSize; j += 1)
                {
                    if (i == j)
                    {
                        diagonalLeft.Add(j + i * boardSize);
                    }

                    if (i + j == boardSize - 1)
                    {
                        diagonalRight.Add(j + i * boardSize);
                    }
                }
            }

            lines.Add((int[])diagonalLeft.ToArray());
            lines.Add((int[])diagonalRight.ToArray());
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
            this.GameInterface.DrawMainScreen
            (
                this.Player1, this.Player2, this.RoundsToWin, 
                this.Board, this.NextPlayerNumber
            );
            return this.GameInterface.GetPlayerMoveSelection(currentPlayer, this.Board);
        }

        public int HandleComputerMoveAction(Player currentPlayer)
        {
            return currentPlayer.ai.GetMiniMaxMove(this.Board, true);
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
            GameInterface.WelcomeMessage();
            Thread.Sleep(1000);
        }

        public void GameSetup()
        {
            this.HandleGameModeSetup();
            this.InstantiatePlayers();
            this.HandlePlayerNamesSetup();
            this.HandlePlayerTokensSetup();
            this.HandleBoardSizeSelection();
            this.HandleNumRoundsSetup();
            this.HandleFirstPlayerSelection();

            if (this.Mode == GameModes.PlayerVsComputer)
            {
                this.Player2.ai = new AI(this.Player2.Token, this.Player1.Token);
            }
        }

        public void HandleGameModeSetup()
        {
            Console.Clear();
            this.SetGameMode(GameInterface.GetGameModeSelection());
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
            var name = GameInterface.GetPlayerNameSelection(playerNumber, invalidName);
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
            var token = GameInterface.GetPlayerTokenSelection(playerNumber, invalidToken);
            currentPlayer.SetPlayerToken(token);
        }

        public void HandleNumRoundsSetup()
        {
            Console.Clear();
            this.SetRoundsToWin(GameInterface.GetRoundsToWinSelection());
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

        public void HandleFirstPlayerSelection()
        {
            Console.Clear();
            int selection = (GameInterface.GetFirstPlayerSelection(this.Player1, this.Player2));
            this.SetFirstPlayer(selection);
        }

        public void HandleBoardSizeSelection()
        {
            Console.Clear();
            int selection = (GameInterface.GetBoardSizeSelection());

            if (selection == 1 || selection == 2 || selection == 3)
            {
                this.BoardSizeSelection = selection + 2;
            }
            else
            {
                throw new ArgumentException("Invalid board size selection.");
            }
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
