using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArtificialIntelligence
{
    public class Minimax : IAlgorithm
    {
        private string TestToken { get; set; }
        private string OtherToken { get; set; }
        public IScorer Scorer { get; set; }
        public IBoardAnalyzer BoardAnalyzer { get; set; }

        public Minimax(string testToken, string otherToken)
        {
            this.TestToken = testToken;
            this.OtherToken = otherToken;
        }

        public int CalculateScore(IBoard board, int depth, bool ownerNext, int alpha, int beta)
        {
            this.Scorer.Board = board;
            if (this.IsLeaf(board))
            {
                return this.Scorer.GetTerminalScore();
            }

            if (depth == 0)
            {
                return this.Scorer.GetHeuristicScore();
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

        private int GetMaximizerScore(IBoard board, string nextMoveToken, int depth, int alpha, int beta)
        {
            int best = MMConstants.MIN;
            var childBoards = this.GetPossibleBoardStates(board, nextMoveToken);
            foreach (IBoard child in childBoards)
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

        private int GetMinimizerScore(IBoard board, string nextMoveToken, int depth, int alpha, int beta)
        {
            int best = MMConstants.MAX;
            var childBoards = this.GetPossibleBoardStates(board, nextMoveToken);
            foreach (IBoard child in childBoards)
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

        public bool IsLeaf(IBoard board)
        {
            return this.BoardAnalyzer.IsEndState(board);
        }

        public IBoard[] GetPossibleBoardStates(IBoard currentBoard, string nextMoveToken)
        {
            int[] emptyIndices = currentBoard.GetAvailableLocations();
            ArrayList possibleBoardStates = new ArrayList();

            for (var i = 0; i < emptyIndices.Length; i += 1)
            {
                var emptyIndex = emptyIndices[i];
                IBoard boardState = this.BoardAnalyzer.SimulateMove(currentBoard, emptyIndex, nextMoveToken);
                possibleBoardStates.Add(boardState);
            }

            IBoard[] result = (IBoard[])possibleBoardStates.ToArray(typeof(IBoard));
            return result;
        }

        public string GetNextMoveToken(bool ownerMovesNext)
        {
            return ownerMovesNext ? this.TestToken : this.OtherToken;
        }
    }
}
