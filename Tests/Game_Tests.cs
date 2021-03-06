using System;
using NUnit.Framework;
using TTTCore;

namespace GameClass.UnitTests
{
    [TestFixture]
    public class Game_Tests
    {
        private Game subject;

        [SetUp] public void Init()
        {
            subject = new Game();
        }

        [TestCase("1")]
        [TestCase("2")]
        public void SetGameModeShouldHandleValidInput(string gameModeNumberString)
        {
            subject.SetGameMode(gameModeNumberString);

            var result = subject.Mode;
            var expected = (GameModes)Enum.Parse(typeof(GameModes), gameModeNumberString);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("3")]
        [TestCase("")]
        [TestCase("a")]
        public void SetGameModeShouldThrowErrorOnInvalidInput(string gameModeNumberString)
        {
            Assert.That
            (
                () => subject.SetGameMode(gameModeNumberString),
                Throws.ArgumentException
            );
        }

        [TestCase(1)]
        [TestCase(5)]
        public void SetRoundsToWinShouldHandleValidInput(int roundsToWin)
        {
            subject.SetRoundsToWin(roundsToWin);
            
            var result = subject.RoundsToWin;

            Assert.That(result, Is.EqualTo(roundsToWin));
        }

        [TestCase(0)]
        [TestCase(10)]
        public void SetRoundsToWinThrowsErrorOnInvalidInput(int roundsToWin)
        {
            Assert.That
            (
                () => subject.SetRoundsToWin(roundsToWin),
                Throws.Exception
            );
        }

        [TestCase(GameModes.PlayerVsPlayer)]
        public void InstantiatePlayersHandlesPlayerVsPlayer(GameModes mode)
        {
            subject.Mode = mode;
            subject.InstantiatePlayers();

            bool result = subject.Player1 is Human && subject.Player2 is Human;

            Assert.IsTrue(result);
        }

        [TestCase(GameModes.PlayerVsComputer)]
        public void InstantiatePlayersHandlesPlayerVsComputer(GameModes mode)
        {
            subject.Mode = mode;
            subject.InstantiatePlayers();

            bool result = subject.Player1 is Human && subject.Player2 is Computer;

            Assert.IsTrue(result);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void SetFirstPlayerShouldHandleValidInput(int playerNumber)
        {
            subject.SetFirstPlayer(playerNumber);

            var result = subject.NextPlayerNumber;

            Assert.That(result, Is.EqualTo(playerNumber));
        }

        [TestCase(0)]
        [TestCase(3)]
        public void SetFirstPlayerShouldHandleInvalidInput(int playerNumber)
        {
            Assert.That
            (
                () => subject.SetFirstPlayer(playerNumber),
                Throws.Exception
            );
        }

        [Test]
        public void GetGameWinnerNameShouldReturnNameIfWinner()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            player1.NumWins = 2;
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            player2.NumWins = 5;
            subject.Player1 = player1;
            subject.Player2 = player2;
            subject.RoundsToWin = 5;

            var result = subject.GetGameWinnerName();

            Assert.That(result, Is.EqualTo("Leela"));
        }

        [Test]
        public void CheckGameOverShouldReturnTrueIfGameOver()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            player1.NumWins = 2;
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            player2.NumWins = 5;
            subject.Player1 = player1;
            subject.Player2 = player2;
            subject.RoundsToWin = 5;

            var result = subject.CheckGameOver();

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckGameOverShouldReturnFalseIfGameNotOver()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            player1.NumWins = 2;
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            player2.NumWins = 2;
            subject.Player1 = player1;
            subject.Player2 = player2;
            subject.RoundsToWin = 5;

            var result = subject.CheckGameOver();

            Assert.IsFalse(result);
        }

        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void AlternateNextPlayerNumberShouldSwitchNextPlayerNumber(
            int nextPlayer, int expected
        )
        {
            subject.NextPlayerNumber = nextPlayer;

            subject.AlternateNextPlayer();
            var result = subject.NextPlayerNumber;

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}