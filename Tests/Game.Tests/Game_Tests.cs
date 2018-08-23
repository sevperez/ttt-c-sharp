using NUnit.Framework;
using GameClass;
using PlayerClass;
using HumanClass;
using ComputerClass;

namespace GameClass.UnitTests
{
    [TestFixture]
    public class Game_Tests
    {
        private readonly Game _game;

        public Game_Tests()
        {
            _game = new Game();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)] // invalid argument
        public void SetGameTypeShouldSetVsComputer(int value)
        {
            if (value == 1 || value == 2)
            {
                _game.SetGameType(value);
                bool result = _game.VsComputer;

                if (value == 1)
                {
                    Assert.IsFalse(result, "vsComputer should be false with input of 1");
                }
                else
                {
                    Assert.IsTrue(result, "vsComputer should be true with input of 2");
                }
            }
            else
            {
                Assert.That(() => _game.SetGameType(value), Throws.ArgumentException);
            }
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(0)]  // outside range
        [TestCase(10)] // outside range
        public void SetRoundsToWinShould(int value)
        {
            if (value >= 1 && value <= 9)
            {
                _game.SetRoundsToWin(value);
                int result = _game.RoundsToWin;
                Assert.That(result, Is.EqualTo(value), "roundsToWin should be set to input value");
            }
            else
            {
                Assert.That(() => _game.SetRoundsToWin(value), Throws.ArgumentException);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void InstantiatePlayers(bool vsComp)
        {
            _game.InstantiatePlayers(vsComp);

            if (vsComp)
            {
                bool result = _game.Player1 is Human && _game.Player2 is Computer;
                Assert.IsTrue(result, "player2 should be computer if vsComp is true");
            }
            else
            {
                bool result = _game.Player1 is Human && _game.Player2 is Human;
                Assert.IsTrue(result, "player2 should be human if vsComp is false");
            }
        }
    }
}