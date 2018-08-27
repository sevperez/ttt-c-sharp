using NUnit.Framework;
using TTTCore;

namespace HumanClass.UnitTests {
    [TestFixture]
    public class Human_Tests {
        private Human _subject;

        [SetUp] public void Init() {
            _subject = new Human();
        }

        [TestCase("Fry")]
        public void SetPlayerNameShouldHandleValidInput(string name) {
            _subject.SetPlayerName(name);

            string result = _subject.Name;

            Assert.That(result, Is.EqualTo(name));
        }

        [TestCase("    Fry")]
        [TestCase("  Fry    ")]
        public void SetPlayerNameShouldTrimInput(string name) {
            _subject.SetPlayerName(name);

            string result = _subject.Name;
            string expected = name.Trim();

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("")]              // empty string
        [TestCase("     ")]         // empty string after trimming
        public void SetPlayerNameShouldThrowErrorIfEmptyInput(string name) {
            Assert.That(() => _subject.SetPlayerName(name), Throws.ArgumentException);
        }

        [TestCase("X")]
        public void SetPlayerTokenShouldHandleValidInput(string token) {
            _subject.SetPlayerToken(token);

            string result = _subject.Token;

            Assert.That(result, Is.EqualTo(token));
        }

        [TestCase(" X   ")]
        public void SetPlayerTokenShouldTrimInput(string token) {
            _subject.SetPlayerToken(token);

            string result = _subject.Token;
            string expected = token.Trim();

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("")]
        [TestCase("xo")]
        public void SetPlayerTokenShouldThrowErrorIfInvalidInput(string token) {
            Assert.That(() => _subject.SetPlayerToken(token), Throws.ArgumentException);
        }
    }
}
