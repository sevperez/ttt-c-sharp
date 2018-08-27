using System;
using NUnit.Framework;
using TTTCore;

namespace GameClass.UnitTests {
    [TestFixture]
    public class Game_Tests {
        private Game _subject;

        [SetUp] public void Init() {
            _subject = new Game();
        }

        [TestCase("1")]
        [TestCase("2")]
        public void SetGameModeShouldHandleValidInput(string gameModeNumberString) {
            _subject.SetGameMode(gameModeNumberString);

            var result = _subject.Mode;
            var expected = (GameModes)Enum.Parse(typeof(GameModes), gameModeNumberString);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("3")]
        [TestCase("")]
        [TestCase("a")]
        public void SetGameModeShouldThrowErrorOnInvalidInput(string gameModeNumberString) {
            Assert.That(
                () => _subject.SetGameMode(gameModeNumberString), Throws.ArgumentException
            );
        }

        [TestCase("1")]
        [TestCase("5")]
        public void SetRoundsToWinShouldHandleValidInput(string roundsToWinString) {
            _subject.SetRoundsToWin(roundsToWinString);
            
            int result = _subject.RoundsToWin;
            int expected = System.Int32.Parse(roundsToWinString);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("0")]
        [TestCase("10")]
        [TestCase("a")]
        public void SetRoundsToWinThrowsErrorOnInvalidInput(string roundsToWinString) {
            Assert.That(
                () => _subject.SetRoundsToWin(roundsToWinString), Throws.Exception
            );
        }

        [TestCase(GameModes.PlayerVsPlayer)]
        public void InstantiatePlayersHandlesPlayerVsPlayer(GameModes mode) {
            _subject.InstantiatePlayers(mode);

            bool result = _subject.Player1 is Human && _subject.Player2 is Human;

            Assert.IsTrue(result);
        }

        [TestCase(GameModes.PlayerVsComputer)]
        public void InstantiatePlayersHandlesPlayerVsComputer(GameModes mode) {
            _subject.InstantiatePlayers(mode);

            bool result = _subject.Player1 is Human && _subject.Player2 is Computer;

            Assert.IsTrue(result);
        }

        [TestCase("1")]
        [TestCase("2")]
        public void SetFirstPlayerShouldHandleValidInput(string playerNumberString) {
            _subject.SetFirstPlayer(playerNumberString);

            int result = _subject.NextPlayerNumber;
            int expected = System.Int32.Parse(playerNumberString);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("0")]
        [TestCase("3")]
        [TestCase("a")]
        public void SetFirstPlayerShouldHandleInvalidInput(string playerNumberString) {
            Assert.That(
                () => _subject.SetFirstPlayer(playerNumberString), Throws.Exception
            );
        }
    }
}