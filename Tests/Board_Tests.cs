using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TTTCore;

namespace BoardClass.UnitTests
{
    [TestFixture]
    public class Board_Tests
    {
        [Test]
        public void ConstructorShouldInitializeEmptySquaresAsDefault()
        {
            var subject = new Board();

            var result = subject.Squares;
            var expected = new List<Square>(Constants.NumSquares);
            for (var i = 0; i < expected.Capacity; i += 1)
            {
                expected.Add(new Square(""));
            }

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void ConstructorShouldInitializeWithFilledSquares()
        {
            string[] existingTokens = new string[] { 
                "X", "X", "O", "", "X", "O", "X", "O", ""
            };
            var subject = new Board(existingTokens);
            
            var result = subject.Squares;
            var expected = new List<Square>(Constants.NumSquares);
            for (var i = 0; i < expected.Capacity; i += 1) {
                expected.Add(new Square(existingTokens[i]));
            }

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void IsFullReturnsTrueIfBoardIsFull()
        {
            string[] existingTokens = new string[] {
                "X", "X", "O", "O", "X", "X", "X", "O", "O"
            };
            var subject = new Board(existingTokens);

            var result = subject.IsFull();

            Assert.IsTrue(result);
        }
        
        [Test]
        public void IsFullReturnsFalseIfBoardIsNotFull()
        {
            string[] existingTokens = new string[] {
                "X", "X", "", "O", "X", "", "X", "O", "O"
            };
            var subject = new Board(existingTokens);

            var result = subject.IsFull();

            Assert.IsFalse(result);
        }

        [Test]
        public void BoardsShouldCompareEqualIfSameSquareTokens()
        {
            string[] existingTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var subject = new Board(existingTokens);
            var compare = new Board(existingTokens);

            bool result = subject == compare;

            Assert.IsTrue(result);
        }

        [Test]
        public void BoardsShouldNotCompareEqualIfDifferentSquareTokens()
        {
            string[] subjectTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var subject = new Board(subjectTokens);
            string[] compareTokens = new string[] {
                "O", "", "X", "O", "X", "O", "", "", "X"
            };
            var compare = new Board(compareTokens);

            bool result = subject == compare;

            Assert.IsFalse(result);
        }

        [Test]
        public void GetTokenArrayShouldReturnCurrentTokens()
        {
            string[] subjectTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var subject = new Board(subjectTokens);

            string[] result = subject.GetTokenArray();

            Assert.That(result, Is.EquivalentTo(subjectTokens));
        }

        [Test]
        public void GetEmptySquareIndicesShouldReturnEmptyIndices()
        {
            string[] existingTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var subject = new Board(existingTokens);

            var result = subject.GetEmptySquareIndices();
            var expected = new int[] { 1, 6, 7 };

            Assert.That(result, Is.EquivalentTo(expected));
        }
  }
}