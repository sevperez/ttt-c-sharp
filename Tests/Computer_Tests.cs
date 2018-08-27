using NUnit.Framework;
using TTTCore;

namespace ComputerClass.UnitTests
{
    [TestFixture]
    public class Computer_Tests
    {
        private readonly Computer _subject;

        public Computer_Tests()
        {
            _subject = new Computer();
        }

        [Test]
        public void SetPlayerNameShouldChooseValidName()
        {
            _subject.SetPlayerName();

            string result = _subject.Name;

            Assert.That(_subject.ValidNames, Has.Member(_subject.Name));
        }

        [TestCase("O")]
        public void SetPlayerTokenChoosesXIfPlayerIsO(string humanToken)
        {
            _subject.SetPlayerToken(humanToken);

            string result = _subject.Token;

            Assert.That(result, Is.EqualTo("X"));
        }

        [TestCase("X")]
        [TestCase("q")]
        public void SetPlayerTokenChoosesOIfPlayerIsNotO(string humanToken)
        {
            _subject.SetPlayerToken(humanToken);

            string result = _subject.Token;

            Assert.That(result, Is.EqualTo("O"));
        }
    }
}