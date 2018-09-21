using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TTTCore;

namespace ArtificialIntelligence
{
    public class Minimax : IAlgorithm
    {
        private string TestToken { get; set; }
        private string OtherToken { get; set; }

        public Minimax(string testToken, string otherToken)
        {
            this.TestToken = testToken;
            this.OtherToken = otherToken;
        }

        public int CalculateScore(Board board, int depth, bool ownerNext, int alpha, int beta)
        {
            var scorer = new BoardScorer(board, this.TestToken, this.OtherToken);
            if (this.IsLeaf(board))
            {
                return scorer.GetTerminalScore();
            }

            if (depth == 0)
            {
                return scorer.GetHeuristicScore();
            }

            var nextMoveToken = this.GetNextMoveToken(ownerNext);
            if (ownerNext)
            {
                return this.GetMaximizerScore(board, nextMoveToken, depth, alpha, beta);
            }
            else
            {
                return this.GetMinimizerScore(board, nextMoveToken, depth, alpha, beta);
            }
        }

        private int GetMaximizerScore(Board board, string nextMoveToken, int depth, int alpha, int beta)
        {
            int best = MMConstants.MIN;
            var childBoards = this.GetPossibleBoardStates(board, nextMoveToken);
            foreach (Board child in childBoards)
            {
                var score = this.CalculateScore(child, depth - 1, false, alpha, beta);
                best = new int[] { best, score }.Max();
                alpha = new int[] { alpha, best }.Max();

                if (beta <= alpha)
                {
                    break;
                }
            }

            return best;
        }

        private int GetMinimizerScore(Board board, string nextMoveToken, int depth, int alpha, int beta)
        {
            int best = MMConstants.MAX;
            var childBoards = this.GetPossibleBoardStates(board, nextMoveToken);
            foreach (Board child in childBoards)
            {
                var score = this.CalculateScore(child, depth - 1, true, alpha, beta);
                best = new int[] { best, score }.Min();
                beta = new int[] { beta, best }.Min();

                if (beta <= alpha)
                {
                    break;
                }
            }

            return best;
        }

        public bool IsLeaf(Board board)
        {
            var round = new Round(board);
            return round.CheckRoundOver();
        }

        public Board[] GetPossibleBoardStates(Board currentBoard, string nextMoveToken)
        {
            int[] emptyIndices = currentBoard.GetAvailableLocations();
            ArrayList possibleBoardStates = new ArrayList();

            for (var i = 0; i < emptyIndices.Length; i += 1)
            {
                var emptyIndex = emptyIndices[i];
                Board boardState = this.SimulateMove(currentBoard, emptyIndex, nextMoveToken);
                possibleBoardStates.Add(boardState);
            }

            Board[] result = (Board[])possibleBoardStates.ToArray(typeof(Board));
            return result;
        }

        public Board SimulateMove(Board inputBoard, int moveIndex, string moveToken)
        {
            var simulatedBoard = new Board(inputBoard.BoardSize);
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

        public string GetNextMoveToken(bool ownerMovesNext)
        {
            return ownerMovesNext ? this.TestToken : this.OtherToken;
        }
    }
}
