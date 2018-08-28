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

        [TestCase("1")]
        [TestCase("5")]
        public void SetRoundsToWinShouldHandleValidInput(string roundsToWinString)
        {
            subject.SetRoundsToWin(roundsToWinString);
            
            var result = subject.RoundsToWin;
            var expected = System.Int32.Parse(roundsToWinString);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("0")]
        [TestCase("10")]
        [TestCase("a")]
        public void SetRoundsToWinThrowsErrorOnInvalidInput(string roundsToWinString)
        {
            Assert.That
            (
                () => subject.SetRoundsToWin(roundsToWinString),
                Throws.Exception
            );
        }

        [TestCase(GameModes.PlayerVsPlayer)]
        public void InstantiatePlayersHandlesPlayerVsPlayer(GameModes mode)
        {
            subject.InstantiatePlayers(mode);

            bool result = subject.Player1 is Human && subject.Player2 is Human;

            Assert.IsTrue(result);
        }

        [TestCase(GameModes.PlayerVsComputer)]
        public void InstantiatePlayersHandlesPlayerVsComputer(GameModes mode)
        {
            subject.InstantiatePlayers(mode);

            bool result = subject.Player1 is Human && subject.Player2 is Computer;

            Assert.IsTrue(result);
        }

        [TestCase("1")]
        [TestCase("2")]
        public void SetFirstPlayerShouldHandleValidInput(string playerNumberString)
        {
            subject.SetFirstPlayer(playerNumberString);

            var result = subject.NextPlayerNumber;
            var expected = System.Int32.Parse(playerNumberString);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("0")]
        [TestCase("3")]
        [TestCase("a")]
        public void SetFirstPlayerShouldHandleInvalidInput(string playerNumberString)
        {
            Assert.That
            (
                () => subject.SetFirstPlayer(playerNumberString),
                Throws.Exception
            );
        }
    }
}