using NUnit.Framework;
using TTTCore;

namespace HumanClass.UnitTests
{
    [TestFixture]
    public class Human_Tests
    {
        private Human subject;

        [SetUp] public void Init()
        {
            subject = new Human();
        }

        [TestCase("Fry")]
        public void SetPlayerNameShouldHandleValidInput(string name)
        {
            subject.SetPlayerName(name);

            var result = subject.Name;

            Assert.That(result, Is.EqualTo(name));
        }

        [TestCase("    Fry")]
        [TestCase("  Fry    ")]
        public void SetPlayerNameShouldTrimInput(string name)
        {
            subject.SetPlayerName(name);

            var result = subject.Name;
            var expected = name.Trim();

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("")]
        [TestCase("     ")]
        public void SetPlayerNameShouldThrowErrorIfEmptyInput(string name)
        {
            Assert.That
            (
                () => subject.SetPlayerName(name),
                Throws.ArgumentException
            );
        }

        [TestCase("X")]
        public void SetPlayerTokenShouldHandleValidInput(string token)
        {
            subject.SetPlayerToken(token);

            var result = subject.Token;

            Assert.That(result, Is.EqualTo(token));
        }

        [TestCase(" X   ")]
        public void SetPlayerTokenShouldTrimInput(string token)
        {
            subject.SetPlayerToken(token);

            var result = subject.Token;
            var expected = token.Trim();

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("")]
        [TestCase("xo")]
        public void SetPlayerTokenShouldThrowErrorIfInvalidInput(string token)
        {
            Assert.That
            (
                () => subject.SetPlayerToken(token),
                Throws.ArgumentException
            );
        }
    }
}
