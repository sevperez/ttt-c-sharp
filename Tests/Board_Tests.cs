using NUnit.Framework;
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
    }
}