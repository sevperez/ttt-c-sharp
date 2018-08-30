using System;
using System.Collections.Generic;

namespace TTTCore
{
    public class MoveOption : IEquatable<MoveOption>
    {
        public int SquareIndex { get; set; }
        public int MiniMaxScore { get; set; }

        public MoveOption(int squareIndex, int miniMaxScore)
        {
            this.SquareIndex = squareIndex;
            this.MiniMaxScore = miniMaxScore;
        }

        // IEquatable Implementation
        public bool Equals(MoveOption other)
        {
            if (other == null)
            {
                return false;
            }
            else if (this.SquareIndex == other.SquareIndex &&
                     this.MiniMaxScore == other.MiniMaxScore)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var moveOptionObj = obj as MoveOption;
            if (moveOptionObj == null)
            {
                return false;
            }
            else
            {
                return Equals(moveOptionObj);
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + this.SquareIndex.GetHashCode();
                hash = hash * 31 + this.MiniMaxScore.GetHashCode();
                return hash;
            }
        }
        
        public static bool operator == (MoveOption moveOption1, MoveOption moveOption2)
        {
            if (((object)moveOption1) == null || ((object)moveOption2) == null)
            {
                return Object.Equals(moveOption1, moveOption2);
            }

            return moveOption1.Equals(moveOption2);
        }

        public static bool operator != (MoveOption moveOption1, MoveOption moveOption2)
        {
            if (((object)moveOption1) == null || ((object)moveOption2) == null)
            {
                return ! Object.Equals(moveOption1, moveOption2);
            }

            return ! moveOption1.Equals(moveOption2);
        }
    }
}
