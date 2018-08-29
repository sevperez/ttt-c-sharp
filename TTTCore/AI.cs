using System;
using System.Collections;
using System.Collections.Generic;

namespace TTTCore
{
    public class AI
    {
        public string OwnerToken { get; set; }

        public AI(string ownerToken)
        {
            this.OwnerToken = ownerToken;
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

        public Board SimulateMove(Board inputBoard, int moveIndex)
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
            simulatedBoard.Squares[moveIndex].Fill(this.OwnerToken);
            
            return simulatedBoard;
        }

        public Board[] GetPossibleBoardStates(Board currentBoard)
        {
            int[] emptySquareIndices = this.GetEmptySquareIndices(currentBoard);
            ArrayList possibleBoardStates = new ArrayList();
            
            for (var i = 0; i < emptySquareIndices.Length; i += 1)
            {
                var emptyIndex = emptySquareIndices[i];
                Board boardState = this.SimulateMove(currentBoard, emptyIndex);
                possibleBoardStates.Add(boardState);
            }

            Board[] result = (Board[])possibleBoardStates.ToArray(typeof(Board));
            return result;
        }
    }
}
