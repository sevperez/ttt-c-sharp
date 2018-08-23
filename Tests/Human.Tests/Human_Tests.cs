using NUnit.Framework;
using PlayerClass;
using HumanClass;

namespace HumanClass.UnitTests
{
    [TestFixture]
    public class Human_Tests
    {
        private readonly Human _human;

        public Human_Tests()
        {
            _human = new Human();
        }

        [TestCase("Fry")]
        [TestCase("    Fry")]       // needs trimming
        [TestCase("  Fry    ")]     // needs trimming
        [TestCase("")]              // empty string
        [TestCase("     ")]         // empty string after trimming
        public void SetPlayerNameShould(string str)
        {
            if (str.Trim().Length == 0)
            {
                Assert.That(() => _human.SetPlayerName(str), Throws.ArgumentException);
            }
            else
            {
                _human.SetPlayerName(str);
                string result = _human.Name;
                Assert.That(result, Is.EqualTo(str.Trim()));
            }
        }

        [TestCase("X")]
        [TestCase(" X   ")]         // needs trimming
        [TestCase("")]              // empty string
        [TestCase("xo")]            // too long
        public void SetPlayerTokenShould(string str)
        {
            if (str.Trim().Length != 1)
            {
                Assert.That(() => _human.SetPlayerToken(str), Throws.ArgumentException);
            }
            else
            {
                _human.SetPlayerToken(str);
                string result = _human.Token;
                Assert.That(result, Is.EqualTo(str.Trim()));
            }
        }
    }
}