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
        public Dictionary<Board, int> MemoOwnerNext { get; set; }
        public Dictionary<Board, int> MemoOpponentNext { get; set; }

        public AI(string ownerToken, string opponentToken)
        {
            this.OwnerToken = ownerToken;
            this.OpponentToken = opponentToken;
            this.MemoOwnerNext = new Dictionary<Board, int>();
            this.MemoOpponentNext = new Dictionary<Board, int>();
        }
        
        public int GetTopMoveIndex(Board board, bool ownerMovesNext)
        {
            var allMoveOptions = this.GetAllMoveOptions(board, ownerMovesNext);
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

        public MoveOption[] GetAllMoveOptions(Board board, bool ownerMovesNext)
        {
            int[] emptyIndices = board.GetEmptySquareIndices();
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

        public int GetMiniMaxScore(Board board, bool ownerMovesNext)
        {
            var game = new Game();
            game.Board = board;

            if (game.CheckRoundOver())
            {
                var score = this.GetRoundOverMiniMaxScore(game);
                this.UpdateMemo(board, ownerMovesNext, score);
                return score;
            }

            var memoScore = this.GetMiniMaxMemoScore(board, ownerMovesNext);
            if (memoScore != -1)
            {
                return memoScore;
            }
            
            int[] miniMaxScores = this.GetMiniMaxScoreArray(board, ownerMovesNext);
            if (ownerMovesNext)
            {
                var score = miniMaxScores.Max();
                this.UpdateMemo(board, ownerMovesNext, score);
                return score;
            }
            else
            {
                var score = miniMaxScores.Min();
                this.UpdateMemo(board, ownerMovesNext, score);
                return score;
            }
        }

        public int GetMiniMaxMemoScore(Board board, bool ownerMovesNext)
        {
            if (ownerMovesNext && this.MemoOwnerNext.ContainsKey(board))
            {
                return this.MemoOwnerNext[board];
            }
            else if (!ownerMovesNext && this.MemoOpponentNext.ContainsKey(board))
            {
                return this.MemoOpponentNext[board];
            }
            else
            {
                return -1;      // sentinel value
            }
        }

        public void UpdateMemo(Board board, bool ownerMovesNext, int score)
        {
            if (ownerMovesNext && !this.MemoOwnerNext.ContainsKey(board))
            {
                this.MemoOwnerNext.Add(board, score);
            }
            else if (!ownerMovesNext && !this.MemoOpponentNext.ContainsKey(board))
            {
                this.MemoOpponentNext.Add(board, score);
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

            List<int> miniMaxScores = new List<int>();
            foreach (Board currentBoard in nextBoardStates)
            {
                var score = this.GetMiniMaxScore(currentBoard, !ownerMovesNext);
                miniMaxScores.Add(score);

                if ((ownerMovesNext && miniMaxScores.Contains(10)) ||
                    (!ownerMovesNext && miniMaxScores.Contains(-10)))
                {
                    break;
                }
            }

            return (int[])miniMaxScores.ToArray();
        }
    }
}
