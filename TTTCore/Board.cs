using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TTTCore
{
    public class Board : IEquatable<Board>
    {
        public List<Square> Squares { get; set; }
        public int BoardSize { get; set; }

        public Board()
        {
            this.BoardSize = Constants.DefaultBoardSize;
            var numSquares = this.ConvertBoardSizeToNumSquares(this.BoardSize);
            this.Squares = new List<Square>(numSquares);
            for (var i = 0; i < this.Squares.Capacity; i += 1)
            {
                this.Squares.Add(new Square(""));
            }
        }

        public Board(int boardSize)
        {
            this.BoardSize = boardSize;
            var numSquares = this.ConvertBoardSizeToNumSquares(this.BoardSize);
            this.Squares = new List<Square>(numSquares);
            for (var i = 0; i < this.Squares.Capacity; i += 1)
            {
                this.Squares.Add(new Square(""));
            }
        }

        public Board(string[] tokens)
        {
            this.BoardSize = Constants.DefaultBoardSize;
            var numSquares = this.ConvertBoardSizeToNumSquares(this.BoardSize);
            this.Squares = new List<Square>(numSquares);
            for (var i = 0; i < this.Squares.Capacity; i += 1)
            {
                this.Squares.Add(new Square(tokens[i]));
            }
        }

        public Board(int boardSize, string[] tokens)
        {
            this.BoardSize = boardSize;
            var numSquares = this.ConvertBoardSizeToNumSquares(this.BoardSize);
            this.Squares = new List<Square>(numSquares);
            for (var i = 0; i < this.Squares.Capacity; i += 1)
            {
                this.Squares.Add(new Square(tokens[i]));
            }
        }

        public int ConvertBoardSizeToNumSquares(int boardSize)
        {
            return (int)Math.Pow(boardSize, 2);
        }

        public bool IsFull()
        {
            return !this.Squares.Contains(new Square());
        }

        public string[] GetTokenArray()
        {
            return this.Squares.Select( square => square.CurrentToken ).ToArray();
        }

        public int[] GetEmptySquareIndices()
        {
            var emptyIndices = new List<int>();

            for (var i = 0; i < this.Squares.Count; i += 1)
            {
                if (this.Squares[i].CurrentToken == "")
                {
                    emptyIndices.Add(i);
                }
            }

            return emptyIndices.ToArray();
        }

        // IEquatable Implementation
        public bool Equals(Board other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                var areEqual = true;
                for (var i = 0; i < this.Squares.Count; i += 1)
                {
                    if (this.Squares[i].CurrentToken != other.Squares[i].CurrentToken)
                    {
                        areEqual = false;
                        break;
                    }
                }
                
                return areEqual;
            }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var boardObj = obj as Board;
            if (boardObj == null)
            {
                return false;
            }
            else
            {
                return Equals(boardObj);
            }
        }

        public override int GetHashCode()
        {
            return this.Squares.GetHashCode();
        }

        public static bool operator ==(Board board1, Board board2)
        {
            if (((object)board1) == null || ((object)board2) == null)
            {
                return Object.Equals(board1, board2);
            }

            return board1.Equals(board2);
        }

        public static bool operator !=(Board board1, Board board2)
        {
            if (((object)board1) == null || ((object)board2) == null)
            {
                return ! Object.Equals(board1, board2);
            }

            return ! board1.Equals(board2);
        }
    }
}
