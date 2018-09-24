using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArtificialIntelligence;

namespace TTTCore
{
    public class Board : IEquatable<Board>, IBoard
    {
        public List<IBoardUnit> Units { get; set; }
        public int BoardSize { get; set; }

        public Board()
        {
            this.BoardSize = Constants.DefaultBoardSize;
            var numSquares = this.ConvertBoardSizeToNumSquares(this.BoardSize);
            this.Units = new List<IBoardUnit>(numSquares);
            for (var i = 0; i < this.Units.Capacity; i += 1)
            {
                this.Units.Add(new Square(""));
            }
        }

        public Board(int boardSize)
        {
            this.BoardSize = boardSize;
            var numSquares = this.ConvertBoardSizeToNumSquares(this.BoardSize);
            this.Units = new List<IBoardUnit>(numSquares);
            for (var i = 0; i < this.Units.Capacity; i += 1)
            {
                this.Units.Add(new Square(""));
            }
        }

        public Board(string[] tokens)
        {
            this.BoardSize = Constants.DefaultBoardSize;
            var numSquares = this.ConvertBoardSizeToNumSquares(this.BoardSize);
            this.Units = new List<IBoardUnit>(numSquares);
            for (var i = 0; i < this.Units.Capacity; i += 1)
            {
                this.Units.Add(new Square(tokens[i]));
            }
        }

        public Board(int boardSize, string[] tokens)
        {
            this.BoardSize = boardSize;
            var numSquares = this.ConvertBoardSizeToNumSquares(this.BoardSize);
            this.Units = new List<IBoardUnit>(numSquares);
            for (var i = 0; i < this.Units.Capacity; i += 1)
            {
                this.Units.Add(new Square(tokens[i]));
            }
        }

        public int ConvertBoardSizeToNumSquares(int boardSize)
        {
            return (int)Math.Pow(boardSize, 2);
        }

        public bool IsFull()
        {
            return !this.Units.Contains(new Square());
        }

        public string[] GetTokenArray()
        {
            return this.Units.Select( square => square.CurrentToken ).ToArray();
        }

        public int[] GetAvailableLocations()
        {
            var emptyIndices = new List<int>();

            for (var i = 0; i < this.Units.Count; i += 1)
            {
                if (this.Units[i].CurrentToken == "")
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
                for (var i = 0; i < this.Units.Count; i += 1)
                {
                    if (this.Units[i].CurrentToken != other.Units[i].CurrentToken)
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
            return this.Units.GetHashCode();
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
