using NUnit.Framework;
using TTTCore;

namespace ComputerClass.UnitTests
{
    [TestFixture]
    public class Computer_Tests
    {
        private Computer subject;

        [SetUp] public void Init()
        {
            subject = new Computer();
        }

        [Test]
        public void SetPlayerNameShouldChooseValidName()
        {
            subject.SetPlayerName();

            var result = subject.Name;

            Assert.That(subject.ValidNames, Has.Member(subject.Name));
        }

        [TestCase("O")]
        public void SetPlayerTokenChoosesXIfPlayerIsO(string humanToken)
        {
            subject.SetPlayerToken(humanToken);

            var result = subject.Token;

            Assert.That(result, Is.EqualTo("X"));
        }

        [TestCase("X")]
        [TestCase("q")]
        public void SetPlayerTokenChoosesOIfPlayerIsNotO(string humanToken)
        {
            subject.SetPlayerToken(humanToken);

            var result = subject.Token;

            Assert.That(result, Is.EqualTo("O"));
        }
    }
}