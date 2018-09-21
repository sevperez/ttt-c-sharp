using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TTTCore
{
    public class Round
    {
        public IGameInterface GameInterface;
        public GameModes Mode { get; set; }
        public int NextPlayerNumber { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int RoundsToWin { get; set; }
        public int BoardSize { get; set; }
        public Board Board { get; set; }
        
        public Round(
            IGameInterface gameInterface, GameModes mode, int boardSize,
            int roundsToWin, int nextPlayerNumber, Player player1, Player player2
        )
        {
            this.GameInterface = gameInterface;
            this.Mode = mode;
            this.BoardSize = boardSize;
            this.RoundsToWin = roundsToWin;
            this.NextPlayerNumber = nextPlayerNumber;
            this.Player1 = player1;
            this.Player2 = player2;
        }

        public Round(Board board)
        {
            this.Board = board;
        }

        public void Play()
        {
            this.Board = new Board(this.BoardSize);

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
            var wlm = new WinningLineGenerator(this.Board.BoardSize);
            int[,] winningLines = wlm.GetWinningLines();

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
            return currentPlayer.ai.ChooseMove(this.Board, true);
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
    }
}
