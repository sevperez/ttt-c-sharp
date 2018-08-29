using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TTTCore
{
    public class AI
    {
        public string OwnerToken { get; set; }
        public string OpponentToken { get; set; }

        public AI(string ownerToken, string opponentToken)
        {
            this.OwnerToken = ownerToken;
            this.OpponentToken = opponentToken;
        }

        public int[] GetEmptySquareIndices(Board board)
        {
            var emptyIndices = new ArrayList();

            for (var i = 0; i < board.Squares.Count; i += 1)
            {
                if (board.Squares[i].CurrentToken == "")
                {
                    emptyIndices.Add(i);
                }
            }

            return (int[])emptyIndices.ToArray(typeof(int));
        }

        public Board SimulateMove(Board inputBoard, int moveIndex, string moveToken)
        {
            var simulatedBoard = new Board();
            for (var i = 0; i < inputBoard.Squares.Count; i += 1)
            {
                var fillToken = inputBoard.Squares[i].CurrentToken;
                if (fillToken != "")
                {
                    simulatedBoard.Squares[i].Fill(fillToken);
                }
            }
            simulatedBoard.Squares[moveIndex].Fill(moveToken);
            
            return simulatedBoard;
        }

        public Board[] GetPossibleBoardStates(Board currentBoard, string nextMoveToken)
        {
            int[] emptySquareIndices = this.GetEmptySquareIndices(currentBoard);
            ArrayList possibleBoardStates = new ArrayList();
            
            for (var i = 0; i < emptySquareIndices.Length; i += 1)
            {
                var emptyIndex = emptySquareIndices[i];
                Board boardState = this.SimulateMove(currentBoard, emptyIndex, nextMoveToken);
                possibleBoardStates.Add(boardState);
            }

            Board[] result = (Board[])possibleBoardStates.ToArray(typeof(Board));
            return result;
        }

        public int GetMiniMaxScore(Board board, bool ownerMovesNext)
        {
            var winner = board.GetWinningToken();

            if (winner == this.OwnerToken)
            {
                return 10;
            }

            if (winner == this.OpponentToken)
            {
                return -10;
            }

            if (winner == null && board.IsFull())
            {
                return 0;
            }

            ownerMovesNext = !ownerMovesNext;
            var nextMoveToken = ownerMovesNext ? this.OwnerToken : this.OpponentToken;

            Board[] nextBoardStates = this.GetPossibleBoardStates(board, nextMoveToken);
            int[] miniMaxScores = nextBoardStates.Select
            (
                nextBoard => this.GetMiniMaxScore(nextBoard, ownerMovesNext)
            ).ToArray();
            
            if (ownerMovesNext)
            {
                return miniMaxScores.Max();
            }
            else
            {
                return miniMaxScores.Min();
            }
        }
    }
}
