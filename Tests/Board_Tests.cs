using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TTTCore;

namespace BoardClass.UnitTests
{
    [TestFixture]
    public class Board_Tests
    {
        [Test]
        public void ConstructorShouldInitializeWithEmpty3x3SquaresAsDefault()
        {
            var subject = new Board();

            var result = subject.Units;
            var defaultNumSquares = (int)Math.Pow(Constants.DefaultBoardSize, 2);
            var expected = new List<Square>(defaultNumSquares);
            for (var i = 0; i < expected.Capacity; i += 1)
            {
                expected.Add(new Square(""));
            }

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ConstructorShouldInitializeWithEmptyNxNSquares(int boardSize)
        {
            var subject = new Board(boardSize);

            var result = subject.Units;
            var expected = new List<Square>((int)Math.Pow(boardSize, 2));
            for (var i = 0; i < expected.Capacity; i += 1)
            {
                expected.Add(new Square(""));
            }

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void ConstructorShouldInitializeWithFilled3x3Squares()
        {
            string[] existingTokens = new string[] { 
                "X", "X", "O", "", "X", "O", "X", "O", ""
            };
            var subject = new Board(existingTokens);
            
            var result = subject.Units;
            var defaultNumSquares = (int)Math.Pow(Constants.DefaultBoardSize, 2);
            var expected = new List<Square>(defaultNumSquares);
            for (var i = 0; i < expected.Capacity; i += 1) {
                expected.Add(new Square(existingTokens[i]));
            }

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void ConstructorShouldInitializeWithFilledNxNSquares()
        {
            string[] existingTokens = new string[] { 
                "X", "X", "O", "O",
                "", "X", "O", "X",
                "X", "O", "", "",
                "O", "X", "", ""
            };
            var boardSize = 4;
            var subject = new Board(boardSize, existingTokens);
            
            var result = subject.Units;
            var expected = new List<Square>((int)Math.Pow(boardSize, 2));
            for (var i = 0; i < expected.Capacity; i += 1) {
                expected.Add(new Square(existingTokens[i]));
            }

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void IsFullReturnsTrueIfBoardIsFullAtDefaultSize()
        {
            string[] existingTokens = new string[] {
                "X", "X", "O", "O", "X", "X", "X", "O", "O"
            };
            var subject = new Board(existingTokens);

            var result = subject.IsFull();

            Assert.IsTrue(result);
        }

        [Test]
        public void IsFullReturnsTrueIfBoardIsFullAtNSize()
        {
            var boardSize = 4;
            string[] existingTokens = new string[] {
                "X", "X", "O", "X",
                "O", "X", "O", "X",
                "X", "O", "O", "X",
                "O", "X", "X", "O"
            };
            var subject = new Board(boardSize, existingTokens);

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
        public void GetEmptySquareIndicesShouldReturnEmptyIndicesAtDefaultSize()
        {
            string[] existingTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var subject = new Board(existingTokens);

            var result = subject.GetAvailableLocations();
            var expected = new int[] { 1, 6, 7 };

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetEmptySquareIndicesShouldReturnEmptyIndicesAtNxNSize()
        {
            var boardSize = 4;
            string[] existingTokens = new string[] {
                "X", "X", "O", "O",
                "", "X", "O", "X",
                "X", "O", "", "",
                "O", "X", "", ""
            };
            var subject = new Board(boardSize, existingTokens);

            var result = subject.GetAvailableLocations();
            var expected = new int[] { 4, 10, 11, 14, 15 };

            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}