using System;
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

        public void UpdateWinningToken()
        {
            throw new NotImplementedException("TODO");
        }
    }
}
