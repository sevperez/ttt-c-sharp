using System;
using NUnit.Framework;
using TTTCore;

namespace GameClass.UnitTests
{
    [TestFixture]
    public class Game_Tests
    {
        private readonly Game _subject;

        public Game_Tests()
        {
            _subject = new Game();
        }

        [TestCase("1")]
        [TestCase("2")]
        public void SetGameModeShouldHandleValidInput(string input)
        {
            _subject.SetGameMode(input);

            var result = _subject.Mode;
            var expected = (GameModes)Enum.Parse(typeof(GameModes), input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("3")]
        [TestCase("")]
        [TestCase("a")]
        public void SetGameModeShouldThrowErrorOnInvalidInput(string input)
        {
            Assert.That(() => _subject.SetGameMode(input), Throws.ArgumentException);
        }

        [TestCase(1)]
        [TestCase(5)]
        public void SetRoundsToWinShouldHandleValidInput(int input)
        {
            _subject.SetRoundsToWin(input);
            
            int result = _subject.RoundsToWin;

            Assert.That(result, Is.EqualTo(input));
        }

        [TestCase(0)]
        [TestCase(10)]
        public void SetRoundsToWinThrowsErrorOnInvalidInput(int input)
        {
            Assert.That(() => _subject.SetRoundsToWin(input), Throws.ArgumentException);
        }

        [TestCase(GameModes.PlayerVsPlayer)]
        public void InstantiatePlayersHandlesPlayerVsPlayer(GameModes mode)
        {
            _subject.InstantiatePlayers(mode);

            bool result = _subject.Player1 is Human && _subject.Player2 is Human;

            Assert.IsTrue(result);
        }

        [TestCase(GameModes.PlayerVsComputer)]
        public void InstantiatePlayersHandlesPlayerVsComputer(GameModes mode)
        {
        _subject.InstantiatePlayers(mode);

        bool result = _subject.Player1 is Human && _subject.Player2 is Computer;

        Assert.IsTrue(result);
        }
    }
}