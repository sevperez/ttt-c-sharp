using System;
using System.Collections;
using System.Collections.Generic;

namespace TTTCore
{
    public class Board
    {
        public bool IsFull { get; set; }
        public string WinningToken { get; set; }
        public List<Square> Squares { get; set; }

        public Board()
        {
            this.Squares = new List<Square>(Constants.NumSquares);
            for (var i = 0; i < this.Squares.Capacity; i += 1)
            {
                this.Squares.Add(new Square(""));
            }
        }

        public Board(string[] tokens)
        {
            this.Squares = new List<Square>(Constants.NumSquares);
            for (var i = 0; i < this.Squares.Capacity; i += 1)
            {
                this.Squares.Add(new Square(tokens[i]));
            }
        }

        public void UpdateFullStatus()
        {
            if (!this.Squares.Contains(new Square()))
            {
                this.IsFull = true;
            }
        }

        public string GetWinningToken()
        {
            for (var i = 0; i < Constants.WinningLines.GetLength(0); i += 1)
            {
                ArrayList lineTokens = new ArrayList();
                for (var j = 0; j < Constants.WinningLines.GetLength(1); j += 1)
                {
                    var index = Constants.WinningLines[i, j];
                    lineTokens.Add(this.Squares[index].CurrentToken);
                }
                
                string[] tokenStrings = (string[]) lineTokens.ToArray(typeof(string));
                if (this.IsWinningLine(tokenStrings))
                {
                    return tokenStrings[0];
                }
            }

            return null;
        }

        public bool IsWinningLine(string[] line)
        {
            var compareItem = line[0];
            for (var i = 0; i < line.Length; i += 1)
            {
                if (line[i] != compareItem)
                {
                    return false;
                }
            }

            return true;
        }

        public void UpdateWinningToken()
        {
            var winningToken = this.GetWinningToken();

            if (winningToken != null)
            {
                this.WinningToken = winningToken;
            }
        }
    }
}
