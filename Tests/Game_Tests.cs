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
    }
}