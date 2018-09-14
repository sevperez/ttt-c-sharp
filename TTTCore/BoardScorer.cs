using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TTTCore
{
    public class BoardScorer
    {
        public Board Board { get; set; }
        public string TestToken { get; set; }
        public string OtherToken { get; set; }

        public BoardScorer(Board board, string testToken, string otherToken)
        {
            this.Board = board;
            this.TestToken = testToken;
            this.OtherToken = otherToken;
        }

        public int GetTerminalBoardScore()
        {
            var round = new Round(this.Board);
            var winningToken = round.GetWinningToken();

            if (winningToken == this.TestToken)
            {
                return Constants.MINIMAX_MAX;
            }
            else if (winningToken == this.OtherToken)
            {
                return Constants.MINIMAX_MIN;
            }
            else
            {
                return 0;
            }
        }

        public int GetHeuristicScore()
        {
            var wlm = new WinningLineGenerator(this.Board.BoardSize);
            int[,] winningLines = wlm.GetWinningLines();

            var score = 0;
            for (var i = 0; i < winningLines.GetLength(0); i += 1)
            {
                List<int> currentLineList = new List<int>();
                for (var j = 0; j < winningLines.GetLength(1); j += 1)
                {
                    currentLineList.Add(winningLines[i, j]);
                }
                var currentLine = (int[])currentLineList.ToArray();

                score += this.GetHeuristicLineScore(currentLine);
            }

            return score;
        }

        public int GetHeuristicLineScore(int[] line)
        {
            if (this.OwnerCanWinLine(line))
            {
                return this.GetFilledCount(line) * 10;
            }
            else if (this.OpponentCanWinLine(line))
            {
                return this.GetFilledCount(line) * -10;
            }
            else
            {
                return 0;
            }
        }

        public bool OwnerCanWinLine(int[] line)
        {
            var lineTokens = line.Select(squareIdx => this.Board.Squares[squareIdx].CurrentToken);
            return !lineTokens.Contains(this.OtherToken);
        }

        public bool OpponentCanWinLine(int[] line)
        {
            var lineTokens = line.Select(squareIdx => this.Board.Squares[squareIdx].CurrentToken);
            return !lineTokens.Contains(this.TestToken);
        }

        public int GetFilledCount(int[] line)
        {
            var count = 0;
            var lineTokens = line.Select(squareIdx => this.Board.Squares[squareIdx].CurrentToken);
            foreach (string fillToken in lineTokens)
            {
                if (fillToken != "")
                {
                    count += 1;
                }
            }

            return count;
        }
    }
}
