using NUnit.Framework;
using TTTCore;

namespace ComputerClass.UnitTests
{
    [TestFixture]
    public class Computer_Tests
    {
        private readonly Computer _computer;

        public Computer_Tests()
        {
            _computer = new Computer();
        }

        [Test]
        public void SetPlayerNameShould()
        {
            _computer.SetPlayerName();
            string result = _computer.Name;
            Assert.That(_computer.ValidNames, Has.Member(_computer.Name));
        }

        [TestCase("O")]             // choose "X"
        [TestCase("X")]             // choose "O"
        [TestCase("q")]             // choose "O"
        public void SetPlayerTokenShould(string humanToken)
        {
            _computer.SetPlayerToken(humanToken);
            string result = _computer.Token;

            if (humanToken == "O")
            {
                Assert.That(result, Is.EqualTo("X"));
            }
            else
            {
                Assert.That(result, Is.EqualTo("O"));
            }
        }
    }
}