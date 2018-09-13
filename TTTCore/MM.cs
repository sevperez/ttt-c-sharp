using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TTTCore
{
    public class MM
    {
        public string OwnerToken { get; set; }
        public string OpponentToken { get; set; }

        public MM(string ownerToken, string opponentToken)
        {
            this.OwnerToken = ownerToken;
            this.OpponentToken = opponentToken;
        }

        public int GetMiniMaxMove(Board board, bool ownerNext)
        {
            var moveOptions = this.GetMoveOptions(board, ownerNext);
            var sortedMoveOptions = moveOptions.OrderBy(move => move.MiniMaxScore);

            if (ownerNext)
            {
                return sortedMoveOptions.Last().SquareIndex;
            }
            else
            {
                return sortedMoveOptions.First().SquareIndex;
            }
        }

        public MoveOption[] GetMoveOptions(Board board, bool ownerNext)
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
            var score = this.Minimax(nextBoard, 0, ownerNext, alpha, beta);

            return new MoveOption(moveIndex, score);
        }

        public int Minimax(Board board, int depth, bool ownerNext, int alpha, int beta)
        {
            if (this.IsLeaf(board))
            {
                return this.GetTerminalBoardScore(board);
            }

            var nextMoveToken = this.GetNextMoveToken(ownerNext);
            if (ownerNext)
            {
                int best = Constants.MIN;
                var childBoards = this.GetPossibleBoardStates(board, nextMoveToken);
                foreach (Board child in childBoards)
                {
                    var score = this.Minimax(child, depth + 1, false, alpha, beta);
                    best = new int[] { best, score }.Max();
                    alpha = new int[] { alpha, best }.Max();

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return best;
            }
            else
            {
                int best = Constants.MAX;
                var childBoards = this.GetPossibleBoardStates(board, nextMoveToken);
                foreach (Board child in childBoards)
                {
                    var score = this.Minimax(child, depth + 1, true, alpha, beta);
                    best = new int[] { best, score }.Min();
                    beta = new int[] { beta, best }.Min();

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return best;
            }
        }

        public bool IsLeaf(Board board)
        {
            var game = new Game();
            game.Board = board;

            return game.CheckRoundOver();
        }

        public int GetTerminalBoardScore(Board board)
        {
            var game = new Game();
            game.Board = board;

            var winningToken = game.GetWinningToken();

            if (winningToken == this.OwnerToken)
            {
                return 10;
            }
            else if (winningToken == this.OpponentToken)
            {
                return -10;
            }
            else
            {
                return 0;
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


// minimax w/ alpha-beta pruning

// minimax(board, depth = 0, ownerMove, alpha = MIN, beta = MAX)
    // if board (ie. node) is leaf (ie. roundOver)
        // return roundOver score
    // if ownerMove (ie. isMaximizingPlayer)
        // int best = MIN
        // get child boards (ie. nodes)
        // for each child board
            // val = minimax(board, depth + 1, false, alpha, beta)
            // best = new int[] { best, val }.MAX()
            // alpha = new int[] { alpha, best }.MAX()
            // if beta <= alpha
                // break
        // return best
    // else (is !ownerMove; ie. isMinimizingPlayer)
        // int best = MAX
        // get child boards (ie. nodes)
        // for each child board
            // val = minimax(board, depth + 1, true, alpha, beta)
            // best = new int[] { best, val }.MIN()
            // beta = new int[] { beta, best }.MIN()
            // if beta <= alpha
                // break
        // return best

