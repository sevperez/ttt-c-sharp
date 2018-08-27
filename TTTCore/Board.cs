using System;
using System.Collections.Generic;

namespace TTTCore {
    public class Board {
        private bool _isFull;
        private string _winningToken;
        private List<Square> _squares;

        public bool IsFull { get; set; }
        public string WinningToken { get; set; }
        public List<Square> Squares { get; set; }

        public Board() {
            this.Squares = new List<Square>(Constants.NumSquares);
            for (int i = 0; i < this.Squares.Capacity; i += 1) {
                this.Squares.Add(new Square(""));
            }
        }

        public Board(string[] tokens) {
            this.Squares = new List<Square>(Constants.NumSquares);
            for (int i = 0; i < this.Squares.Capacity; i += 1) {
                this.Squares.Add(new Square(tokens[i]));
            }
        }

        public void UpdateFullStatus() {
            if (!this.Squares.Contains(new Square())) {
                this.IsFull = true;
            }
        }

        public void UpdateWinningToken() {
            for (int lineIdx = 0; lineIdx < Constants.WinningLines.Length; lineIdx += 1) {
                string[] lineTokens = new string[] {};
                for (int colIdx = 0; colIdx < Constants.WinningLines[0].Length; colIdx += 1) {
                    
                }
            }
        }
    }
}
