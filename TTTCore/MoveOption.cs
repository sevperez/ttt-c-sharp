using System;
using System.Collections.Generic;

namespace TTTCore
{
    public class MoveOption : IEquatable<MoveOption>
    {
        public int Location { get; set; }
        public int Score { get; set; }

        public MoveOption(int location, int score)
        {
            this.Location = location;
            this.Score = score;
        }

        // IEquatable Implementation
        public bool Equals(MoveOption other)
        {
            if (other == null)
            {
                return false;
            }
            else if (this.Location == other.Location &&
                     this.Score == other.Score)
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
                hash = hash * 31 + this.Location.GetHashCode();
                hash = hash * 31 + this.Score.GetHashCode();
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
