using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TTTCore
{
    public class Board : IEquatable<Board>
    {
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

        public bool IsFull()
        {
            if (this.Squares.Contains(new Square()))
            {
                return false;
            }
            else
            {
                return true;
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
            return line.All( token => line[0] != "" && token == line[0] );
        }

        public string[] GetTokenArray()
        {
            return (string[])this.Squares.Select( square => square.CurrentToken ).ToArray();
        }

        public int[] GetEmptySquareIndices()
        {
            var emptyIndices = new ArrayList();

            for (var i = 0; i < this.Squares.Count; i += 1)
            {
                if (this.Squares[i].CurrentToken == "")
                {
                emptyIndices.Add(i);
                }
            }

            return (int[])emptyIndices.ToArray(typeof(int));
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
