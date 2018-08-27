using System;

namespace TTTCore {
    public class Square : IEquatable<Square> {
        private string _currentToken;

        public string CurrentToken { get; set; }

        public Square() {
            this.CurrentToken = "";
        }

        public Square(string token) {
            this.CurrentToken = token;
        }

        public void Fill(string token) {
            if (this.CurrentToken == null || this.CurrentToken == "") {
                this.CurrentToken = token;
            } else {
                throw new ArgumentException("Square is already filled.");
            }
        }

        // IEquatable Implementation
        public bool Equals(Square other) {
            if (other == null) {
                return false;
            } else if (this.CurrentToken == other.CurrentToken) {
                return true;
            } else {
                return false;
            }
        }

        public override bool Equals(Object obj) {
            if (obj == null) {
                return false;
            }

            Square squareObj = obj as Square;
            if (squareObj == null) {
                return false;
            } else {
                return Equals(squareObj);
            }
        }

        public override int GetHashCode() {
            return this.CurrentToken.GetHashCode();
        }

        public static bool operator == (Square square1, Square square2) {
            if (((object)square1) == null || ((object)square2) == null) {
                return Object.Equals(square1, square2);
            }

            return square1.Equals(square2);
        }

        public static bool operator != (Square square1, Square square2) {
            if (((object)square1) == null || ((object)square2) == null) {
                return ! Object.Equals(square1, square2);
            }

            return ! square1.Equals(square2);
        }
    }
}
