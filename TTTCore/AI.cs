using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArtificialIntelligence
{
    public class AI
    {
        private string OwnerToken { get; set; }
        private string OpponentToken { get; set; }
        private IAlgorithm Algorithm { get; set; }

        public AI(
            string ownerToken, string opponentToken, IBoardAnalyzer analyzer, IScorer scorer
        )
        {
            this.OwnerToken = ownerToken;
            this.OpponentToken = opponentToken;
            this.Algorithm = new Minimax(this.OwnerToken, this.OpponentToken);
            this.Algorithm.Scorer = scorer;
            this.Algorithm.BoardAnalyzer = analyzer;
        }

        public int ChooseMove(IBoard board, bool ownerNext)
        {
            var allMoveOptions = this.GetAllMoveOptions(board, ownerNext);
            var topMoves = this.GetTopMoveOptions(allMoveOptions);

            var random = new Random();
            var randomIndex = random.Next(topMoves.Count);

            return topMoves[randomIndex].Location;
        }

        private List<MoveOption> GetTopMoveOptions(List<MoveOption> allMoveOptions)
        {
            List<MoveOption> topMoves = new List<MoveOption>();
            var bestScore = allMoveOptions[0].Score;

            for (var i = 0; i < allMoveOptions.Count; i += 1)
            {
                var currentMoveOption = allMoveOptions[i];
                if (currentMoveOption.Score > bestScore)
                {
                    topMoves = new List<MoveOption>();
                    topMoves.Add(currentMoveOption);
                    bestScore = currentMoveOption.Score;
                }
                else if (currentMoveOption.Score == bestScore)
                {
                    topMoves.Add(currentMoveOption);
                }
            }

            return topMoves;
        }

        private List<MoveOption> GetAllMoveOptions(IBoard board, bool ownerNext)
        {
            var allMoveOptions = new List<MoveOption>();

            int[] emptyIndices = board.GetAvailableLocations();
            foreach (int idx in emptyIndices)
            {
                var move = this.GenerateMoveOption(board, idx, ownerNext);
                allMoveOptions.Add(move);
            }

            return allMoveOptions;
        }

        private MoveOption GenerateMoveOption(IBoard board, int moveIndex, bool ownerNext)
        {
            var token = this.GetNextMoveToken(ownerNext);
            var nextBoard = this.Algorithm.BoardAnalyzer.SimulateMove(board, moveIndex, token);

            var alpha = MMConstants.MIN;
            var beta = MMConstants.MAX;
            var initialDepth = this.GetInitialDepth(board);
            var score = this.Algorithm.CalculateScore(
                nextBoard, initialDepth, !ownerNext, alpha, beta
            );

            return new MoveOption(moveIndex, score);
        }

        public int GetInitialDepth(IBoard board)
        {
            if (board.BoardSize <= MMConstants.MAX_MINIMAX_DEPTH)
            {
                return board.BoardSize - 1;
            }
            else
            {
                return MMConstants.MAX_MINIMAX_DEPTH;
            }
        }

        public string GetNextMoveToken(bool ownerMovesNext)
        {
            return ownerMovesNext ? this.OwnerToken : this.OpponentToken;
        }
    }
}
