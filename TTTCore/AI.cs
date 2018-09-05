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
        
        public int GetTopMoveIndex(Board board, bool ownerMovesNext)
        {
            var moveOptions = this.GetMoveOptions(board, ownerMovesNext);
            ArrayList topMoves = new ArrayList();
            var bestMiniMaxScore = moveOptions[0].MiniMaxScore;

            for (var i = 0; i < moveOptions.Length; i += 1)
            {
                var currentMoveOption = moveOptions[i];
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

            var random = new Random();
            var randomIndex = random.Next(topMoves.Count);
            MoveOption[] goodMoves = (MoveOption[])topMoves.ToArray(typeof(MoveOption));
            
            return goodMoves[randomIndex].SquareIndex;
        }

        public MoveOption[] GetMoveOptions(Board board, bool ownerMovesNext)
        {
            int[] emptyIndices = this.GetEmptySquareIndices(board);
            var nextMoveToken = ownerMovesNext ? this.OwnerToken : this.OpponentToken;
            
            var moveOptions = new ArrayList();
            for (var i = 0; i < emptyIndices.Length; i += 1)
            {
                var emptyIndex = emptyIndices[i];
                var nextBoard = this.SimulateMove(board, emptyIndex, nextMoveToken);
                var newMoveStatus = !ownerMovesNext;
                
                var miniMaxScore = this.GetMiniMaxScore(nextBoard, newMoveStatus);
                
                moveOptions.Add(new MoveOption(emptyIndex, miniMaxScore));
            }
            
            return (MoveOption[])moveOptions.ToArray(typeof(MoveOption));
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
            var game = new Game();
            game.Board = board;

            if (game.CheckRoundOver())
            {
                return this.GetRoundOverMiniMaxScore(game);
            }
            
            int[] miniMaxScores = this.GetMiniMaxScoreArray(board, ownerMovesNext);
            // ownerMovesNext = !ownerMovesNext;

            if (ownerMovesNext)
            {
                return miniMaxScores.Max();
            }
            else
            {
                return miniMaxScores.Min();
            }
        }

        public int GetRoundOverMiniMaxScore(Game game)
        {
            var winner = game.GetWinningToken();
            if (winner == this.OwnerToken)
            {
                return 10;
            }
            else if (winner == this.OpponentToken)
            {
                return -10;
            }
            else
            {
                return 0;
            }
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

        public int[] GetMiniMaxScoreArray(Board board, bool ownerMovesNext)
        {
            var nextMoveToken = this.GetNextMoveToken(ownerMovesNext);
            Board[] nextBoardStates = this.GetPossibleBoardStates(board, nextMoveToken);

            int[] miniMaxScores = nextBoardStates.Select
            (
                nextBoard => this.GetMiniMaxScore(nextBoard, !ownerMovesNext)
            ).ToArray();

            return miniMaxScores;
        }
    }
}
