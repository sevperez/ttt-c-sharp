using System;
using System.IO;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace TTTCore
{
    public class CLI : IGameInterface
    {
        public IConsole GameConsole { get; set; }

        public CLI(IConsole console = null)
        {
            if (console == null)
            {
                this.GameConsole = new GameConsole();
            }
            else
            {
                this.GameConsole = console;
            }
        }

        public void WelcomeMessage()
        {
            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.GameConsole.Write(Constants.Messages["welcome"]);
        }

        public string GetGameModeSelection()
        {
            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.GameConsole.Write(Constants.Messages["requestGameMode"]);
            string selection = this.GameConsole.ReadLine().Trim();

            while (selection != "1" && selection != "2")
            {
                this.GameConsole.Clear();
                this.GameConsole.Write(Constants.MainBanner);
                this.GameConsole.Write(Constants.Messages["gameModeInputError"]);
                selection = this.GameConsole.ReadLine().Trim();
            }
            
            return selection;
        }

        public int GetRoundsToWinSelection()
        {
            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.GameConsole.Write(Constants.Messages["requestRoundsToWin"]);

            string input = this.GameConsole.ReadLine().Trim();
            int selection;
            bool successfulParse = Int32.TryParse(input, out selection);

            while (!successfulParse || selection < 1 || selection > 9)
            {
                this.GameConsole.Clear();
                this.GameConsole.Write(Constants.MainBanner);
                this.GameConsole.Write(Constants.Messages["roundsToWinInputError"]);
                input = this.GameConsole.ReadLine().Trim();
                successfulParse = Int32.TryParse(input, out selection);
            }
            
            return selection;
        }

        public string GetPlayerNameSelection(int playerNumber, string invalidName = "")
        {
            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.GameConsole.Write(Constants.Messages["requestPlayerName"], playerNumber);
            string selection = this.GameConsole.ReadLine().Trim();

            while (selection == null || selection == "" || selection == invalidName)
            {
                this.GameConsole.Clear();
                this.GameConsole.Write(Constants.MainBanner);
                this.GameConsole.Write(Constants.Messages["playerNameInputError"], playerNumber);
                selection = this.GameConsole.ReadLine().Trim();
            }
            
            return selection;
        }

        public string GetPlayerTokenSelection(int playerNumber, string invalidToken = "")
        {
            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.GameConsole.Write(Constants.Messages["requestPlayerToken"], playerNumber);
            string selection = this.GameConsole.ReadLine().Trim();

            while (selection == null || selection.Length != 1 || selection == invalidToken)
            {
                this.GameConsole.Clear();
                this.GameConsole.Write(Constants.MainBanner);
                this.GameConsole.Write(Constants.Messages["playerTokenInputError"], playerNumber);
                selection = this.GameConsole.ReadLine().Trim();
            }
            
            return selection;
        }

        public int GetFirstPlayerSelection(Player player1, Player player2)
        {
            var name1 = player1.Name;
            var name2 = player2.Name;
            
            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.GameConsole.Write(Constants.Messages["requestFirstPlayer"], name1, name2);

            string selection = this.GameConsole.ReadLine().Trim();

            while (selection != "1" && selection != "2")
            {
                this.GameConsole.Clear();
                this.GameConsole.Write(Constants.MainBanner);
                this.GameConsole.Write(Constants.Messages["firstPlayerInputError"], name1, name2);
                selection = this.GameConsole.ReadLine().Trim();
            }
            
            return Int32.Parse(selection);
        }

        public int GetBoardSizeSelection()
        {
            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.GameConsole.Write(Constants.Messages["requestBoardSize"]);
            string selection = this.GameConsole.ReadLine().Trim();

            while (selection != "1" && selection != "2" && selection != "3")
            {
                this.GameConsole.Clear();
                this.GameConsole.Write(Constants.MainBanner);
                this.GameConsole.Write(Constants.Messages["boardSizeInputError"]);
                selection = this.GameConsole.ReadLine().Trim();
            }
            
            return Int32.Parse(selection);
        }

        public int GetPlayerMoveSelection(Player player, Board board)
        {
            var emptyIndices = board.GetAvailableLocations();

            string input = this.GameConsole.ReadLine().Trim();
            int selection;
            bool successfulParse = Int32.TryParse(input, out selection);

            while (!successfulParse || !emptyIndices.Contains(selection - 1))
            {
                input = this.GameConsole.ReadLine().Trim();
                successfulParse = Int32.TryParse(input, out selection);
            }

            return selection - 1;
        }

        public void RequestMoveMessage(Player player, int[] emptyIndices)
        {
            var updatedIndices = (int[])emptyIndices.Select( index => index + 1 ).ToArray();
            this.GameConsole.Write
            (
                Constants.Messages["requestPlayerMove"],
                player.Name, string.Join(", ", updatedIndices)
            );
        }

        public void DrawMainScreen(
            Player player1, Player player2, int numRounds, 
            Board board, int nextPlayerNumber
        )
        {
            var tokens = board.GetTokenArray();
            var emptyIndices = board.GetAvailableLocations();
            var nextPlayer = nextPlayerNumber == 1 ? player1 : player2;

            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.DrawRoundBanner(player1, player2, numRounds);
            this.DrawGameBoard(tokens, board.BoardSize);
            this.RequestMoveMessage(nextPlayer, emptyIndices);
            this.GameConsole.Write(Constants.Footer);
        }

        public void DrawRoundEnd(
            Player player1, Player player2, int numRounds, 
            Board board, string winnerName
        )
        {
            var tokens = board.GetTokenArray();
            string message;
            if (winnerName == null)
            {
                message = Constants.Messages["roundDrawMessage"];
            }
            else
            {
                message = Constants.Messages["playerRoundWinMessage"];
            }

            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.DrawRoundBanner(player1, player2, numRounds);
            this.DrawGameBoard(tokens, board.BoardSize);
            this.GameConsole.Write(message, winnerName);
            this.GameConsole.Write(Constants.Footer);
        }

        public void DrawGameEnd(
            Player player1, Player player2, int numRounds, 
            Board board, string winnerName
        )
        {
            var tokens = board.GetTokenArray();

            this.GameConsole.Clear();
            this.GameConsole.Write(Constants.MainBanner);
            this.DrawRoundBanner(player1, player2, numRounds);
            this.DrawGameBoard(tokens, board.BoardSize);
            this.GameConsole.Write(Constants.Messages["playerGameWinMessage"], winnerName);
            this.GameConsole.Write(Constants.Footer);
        }

        public void DrawRoundBanner(Player player1, Player player2, int numRounds)
        {
            var name1 = player1.Name;
            var token1 = player1.Token;
            var numWins1 = player1.NumWins;
            var name2 = player2.Name;
            var token2 = player2.Token;
            var numWins2 = player2.NumWins;
            var writeArgs = new object[] { name1, token1, numWins1, 
                                           name2, token2, numWins2,
                                           numRounds };

            this.GameConsole.Write(Constants.RoundBanner, writeArgs);
        }

        public void DrawGameBoard(string[] tokens, int boardSize)
        {
            var adjustedTokens = (string[])tokens.Select
            (
                token => token == "" ? " " : token
            ).ToArray();

            var gameBoard = this.BuildGameBoard(boardSize);

            this.GameConsole.Write(gameBoard, adjustedTokens);
        }

        public string BuildGameBoard(int boardSize)
        {
            var boardString = "";
            for (var i = 0; i < boardSize; i += 1)
            {
                boardString += this.BuildSectionEmptyLine(boardSize);
                boardString += this.BuildSectionFillLine(boardSize, i);
                boardString += this.BuildSectionEmptyLine(boardSize);

                if (i < boardSize - 1)
                {
                    boardString += this.BuildHorizontalEdgeLine(boardSize);
                }
            }
            boardString += "\n";

            return boardString;
        }

        public string BuildSectionEmptyLine(int boardSize)
        {
            var line = Constants.BoardPieces["leftPadding"];
            for (var i = 0; i < boardSize - 1; i += 1)
            {
                line += String.Format(Constants.BoardPieces["leftAndCenterSection"], " ");
            }
            line += String.Format(Constants.BoardPieces["rightSection"], " ");

            return line;
        }

        public string BuildSectionFillLine(int boardSize, int rowNumber)
        {
            var line = Constants.BoardPieces["leftPadding"];
            for (var i = 0; i < boardSize - 1; i += 1)
            {
                var cellNumber = rowNumber * boardSize + i;
                var cellNumberHolder = "{" + cellNumber.ToString() + "}";
                line += String.Format(Constants.BoardPieces["leftAndCenterSection"], cellNumberHolder);
            }
            var lastCellNumber = (rowNumber + 1) * boardSize - 1;
            var lastCellNumberHolder = "{" + lastCellNumber.ToString() + "}";
            line += String.Format(Constants.BoardPieces["rightSection"], lastCellNumberHolder);

            return line;
        }

        public string BuildHorizontalEdgeLine(int boardSize)
        {
            var line = Constants.BoardPieces["leftPadding"];
            for (var i = 0; i < boardSize; i += 1)
            {
                line += Constants.BoardPieces["horizontalEdge"];

                if (i < boardSize - 1)
                {
                    line += "-";
                }
            }
            line += "\n";

            return line;
        }
    }
}
