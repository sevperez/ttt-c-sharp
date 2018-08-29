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
        public void UpdateFullStatusShouldUpdateToTrueIfBoardIsFull()
        {
            string[] existingTokens = new string[] {
                "X", "X", "O", "O", "X", "X", "X", "O", "O"
            };
            var subject = new Board(existingTokens);
            subject.UpdateFullStatus();

            var result = subject.IsFull;

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void UpdateFullStatusShouldNotUpdateIfBoardNotFull()
        {
            string[] existingTokens = new string[] {
                "X", "X", "", "O", "X", "", "X", "O", "O"
            };
            var subject = new Board(existingTokens);
            subject.UpdateFullStatus();

            var result = subject.IsFull;

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void GetWinningTokenShouldReturnTokenIfWinner()
        {
            string[] existingTokens = new string[] {
                "X", "X", "X", "O", "X", "O", "", "", "O"
            };
            var subject = new Board(existingTokens);

            var result = subject.GetWinningToken();

            Assert.That(result, Is.EqualTo("X"));
        }

        [Test]
        public void GetWinningTokenShouldReturnNullIfNoWinner()
        {
            string[] existingTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var subject = new Board(existingTokens);

            var result = subject.GetWinningToken();

            Assert.IsNull(result);
        }

        [Test]
        public void IsWinningLineShouldReturnTrueIfWinningLine()
        {
            var inputLine = new string[] { "X", "X", "X"};
            var subject = new Board();

            var result = subject.IsWinningLine(inputLine);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsWinningLineShouldReturnFalseIfNotWinningLine()
        {
            var inputLine = new string[] { "X", "O", "X" };
            var subject = new Board();

            var result = subject.IsWinningLine(inputLine);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsWinningLineShouldReturnFalseIfUnfilledLine()
        {
            var inputLine = new string[] { "X", "X", "" };
            var subject = new Board();

            var result = subject.IsWinningLine(inputLine);

            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateWinningTokenShouldUpdateIfWinner()
        {
            string[] existingTokens = new string[] {
                "X", "X", "X", "O", "X", "O", "", "", "O"
            };
            var subject = new Board(existingTokens);
            subject.UpdateWinningToken();

            var result = subject.WinningToken;

            Assert.That(result, Is.EqualTo("X"));
        }

        [Test]
        public void UpdateWinningTokenShouldNotUpdateIfNoWinner()
        {
            string[] existingTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var subject = new Board(existingTokens);
            subject.UpdateWinningToken();

            var result = subject.WinningToken;

            Assert.That(result, Is.EqualTo(null));
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
    }
}