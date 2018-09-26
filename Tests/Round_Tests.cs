using System;
using NUnit.Framework;
using TTTCore;
using IConsoleInterface.Tests;

namespace RoundClass.UnitTests
{
    [TestFixture]
    public class Round_Tests
    {
        private Player Player1;
        private Player Player2;
        private GameModes Mode;
        private IGameInterface TestInterface;

        [SetUp]
        public void Init()
        {
            var testConsole = new FakeConsole();
            this.TestInterface = new CLI(testConsole);
            this.Mode = GameModes.PlayerVsPlayer;

            this.Player1 = new Human();
            this.Player1.Name = "Fry";
            this.Player1.Token = "X";
            this.Player1.NumWins = 0;

            this.Player2 = new Human();
            this.Player2.Name = "Leela";
            this.Player2.NumWins = 0;
            this.Player2.Token = "O";
        }

        [Test]
        public void IncrementWinnerScoreShouldUpdateWinnerNumWins()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, 1,
                this.Player1, this.Player2
            );

            subject.IncrementWinnerScore(Player1.Token);
            var result = Player1.NumWins;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void GetRoundWinnerNameShouldReturnNameIfWinner()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, 1,
                this.Player1, this.Player2
            );
            var winningToken = this.Player1.Token;

            var result = subject.GetRoundWinnerName(winningToken);

            Assert.That(result, Is.EqualTo("Fry"));
        }

        [Test]
        public void GetRoundWinnerNameShouldReturnNullIfNoWinner()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, 1,
                this.Player1, this.Player2
            );
            string winningToken = null;

            var result = subject.GetRoundWinnerName(winningToken);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void CheckRoundOverShouldReturnTrueIfRoundWinnerOnDefaultSize()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, 1,
                this.Player1, this.Player2
            );
            string[] tokens = new string[] { 
                "X", "X", "O", 
                "", "X", "O", 
                "O", "X", ""
            };
            subject.Board = new Board(tokens);

            var result = subject.IsOver();

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckRoundOverShouldReturnTrueIfRoundWinnerOnNxNSize()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 4, 10, 1,
                this.Player1, this.Player2
            );
            string[] tokens = new string[] {
                "X", "X", "X", "X",
                "O", "X", "O", "X",
                "X", "O", "O", "X",
                "O", "X", "O", "O"
            };
            subject.Board = new Board(subject.BoardSize, tokens);

            var result = subject.IsOver();

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckRoundOverShouldReturnTrueIfBoardFull()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, 1,
                this.Player1, this.Player2
            );
            string[] tokens = new string[] { 
                "X", "X", "O", 
                "O", "O", "X", 
                "X", "O", "X"
            };
            subject.Board = new Board(tokens);

            var result = subject.IsOver();

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckRoundOverShouldReturnFalseIfNoWinnerAndBoardNotFull()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, 1,
                this.Player1, this.Player2
            );
            string[] tokens = new string[] { 
                "X", "X", "O", 
                "", "X", "O", 
                "X", "O", ""
            };
            subject.Board = new Board(tokens);

            var result = subject.IsOver();

            Assert.IsFalse(result);
        }

        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void AlternateNextPlayerNumberShouldSwitchNextPlayerNumber(
            int nextPlayer, int expected
        )
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, nextPlayer,
                this.Player1, this.Player2
            );

            subject.AlternateNextPlayer();
            var result = subject.NextPlayerNumber;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetWinningTokenShouldReturnTokenIfWinnerOnDefaultSize()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, 1,
                this.Player1, this.Player2
            );
            string[] existingTokens = new string[] {
                "X", "X", "X", "O", "X", "O", "", "", "O"
            };
            subject.Board = new Board(existingTokens);

            var result = subject.GetWinningToken();

            Assert.That(result, Is.EqualTo("X"));
        }

        [Test]
        public void GetWinningTokenShouldReturnTokenIfWinnerOnNxNSize()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 4, 10, 1,
                this.Player1, this.Player2
            );
            string[] tokens = new string[] {
                "X", "X", "X", "X",
                "O", "X", "O", "X",
                "X", "O", "O", "X",
                "O", "X", "O", "O"
            };
            subject.Board = new Board(subject.BoardSize, tokens);

            var result = subject.GetWinningToken();

            Assert.That(result, Is.EqualTo("X"));
        }

        [Test]
        public void GetWinningTokenShouldReturnNullIfNoWinner()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, 1,
                this.Player1, this.Player2
            );
            string[] existingTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            subject.Board = new Board(existingTokens);

            var result = subject.GetWinningToken();

            Assert.IsNull(result);
        }

        [Test]
        public void GetWinningTokenShouldReturnNullForEmptyBoard()
        {
            var subject = new Round(
                this.TestInterface, this.Mode, 3, 10, 1,
                this.Player1, this.Player2
            );
            string[] existingTokens = new string[] {
                "", "", "", "", "", "", "", "", ""
            };
            subject.Board = new Board(existingTokens);

            var result = subject.GetWinningToken();

            Assert.IsNull(result);
        }
    }
}