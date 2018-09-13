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

        public int GetMiniMaxMove(Board board, bool ownerNext)
        {
            var allMoveOptions = this.GetAllMoveOptions(board, ownerNext);
            var topMoves = this.GetTopMoveOptions(allMoveOptions);

            var random = new Random();
            var randomIndex = random.Next(topMoves.Length);
            return topMoves[randomIndex].SquareIndex;
        }

        public MoveOption[] GetTopMoveOptions(MoveOption[] allMoveOptions)
        {
            ArrayList topMoves = new ArrayList();
            var bestMiniMaxScore = allMoveOptions[0].MiniMaxScore;

            for (var i = 0; i < allMoveOptions.Length; i += 1)
            {
                var currentMoveOption = allMoveOptions[i];
                if (currentMoveOption.MiniMaxScore > bestMiniMaxScore)
                {
                    topMoves = new ArrayList();
                    topMoves.Add(currentMoveOption);
                    bestMiniMaxScore = currentMoveOption.MiniMaxScore;
                }
                else if (currentMoveOption.MiniMaxScore == bestMiniMaxScore)
                {
                    topMoves.Add(currentMoveOption);
                }
            }

            return (MoveOption[])topMoves.ToArray(typeof(MoveOption));
        }

        public MoveOption[] GetAllMoveOptions(Board board, bool ownerNext)
        {
            var moveOptions = new ArrayList();

            int[] emptyIndices = board.GetEmptySquareIndices();
            foreach (int idx in emptyIndices)
            {
                var move = this.GenerateMoveOption(board, idx, ownerNext);
                moveOptions.Add(move);
            }

            return (MoveOption[])moveOptions.ToArray(typeof(MoveOption));
        }

        public MoveOption GenerateMoveOption(Board board, int moveIndex, bool ownerNext)
        {
            var token = this.GetNextMoveToken(ownerNext);
            var nextBoard = this.SimulateMove(board, moveIndex, token);

            var alpha = Constants.MIN;
            var beta = Constants.MAX;
            var initialDepth = this.GetInitialDepth(board);
            var score = this.Minimax(nextBoard, initialDepth, !ownerNext, alpha, beta);

            return new MoveOption(moveIndex, score);
        }

        public int Minimax(Board board, int depth, bool ownerNext, int alpha, int beta)
        {
            var boardScorer = new BoardScorer(board, this.OwnerToken, this.OpponentToken);
            if (this.IsLeaf(board))
            {
                return boardScorer.GetTerminalBoardScore();
            }

            if (depth == 0)
            {
                return boardScorer.GetHeuristicScore();
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

        public int GetMaximizerScore(Board board, string nextMoveToken, int depth, int alpha, int beta)
        {
            int best = Constants.MIN;
            var childBoards = this.GetPossibleBoardStates(board, nextMoveToken);
            foreach (Board child in childBoards)
            {
                var score = this.Minimax(child, depth - 1, false, alpha, beta);
                best = new int[] { best, score }.Max();
                alpha = new int[] { alpha, best }.Max();

                if (beta <= alpha)
                {
                    break;
                }
            }

            return best;
        }

        public int GetMinimizerScore(Board board, string nextMoveToken, int depth, int alpha, int beta)
        {
            int best = Constants.MAX;
            var childBoards = this.GetPossibleBoardStates(board, nextMoveToken);
            foreach (Board child in childBoards)
            {
                var score = this.Minimax(child, depth - 1, true, alpha, beta);
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
            var game = new Game();
            game.Board = board;

            return game.CheckRoundOver();
        }

        public int GetInitialDepth(Board board)
        {
            if (board.BoardSize <= Constants.MAX_MINIMAX_DEPTH)
            {
                return board.BoardSize - 1;
            }
            else
            {
                return Constants.MAX_MINIMAX_DEPTH;
            }
        }

        public Board[] GetPossibleBoardStates(Board currentBoard, string nextMoveToken)
        {
            int[] emptyIndices = currentBoard.GetEmptySquareIndices();
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
            if (ownerMovesNext)
            {
                return this.OwnerToken;
            }
            else
            {
                return this.OpponentToken;
            }
        }
    }
}
