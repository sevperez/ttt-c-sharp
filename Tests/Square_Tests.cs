using NUnit.Framework;
using TTTCore;

namespace SquareClass.UnitTests
{
    [TestFixture]
    public class Square_Tests
    {
        [Test]
        public void ConstructorShouldInitializeWithEmptyCurrentTokenByDefault()
        {
            var subject = new Square();

            var result = subject.CurrentToken;

            Assert.That(result, Is.EqualTo(""));
        }

        [TestCase("X")]
        public void ConstructorShouldInitializeWithToken(string token)
        {
            var subject = new Square(token);

            var result = subject.CurrentToken;

            Assert.That(result, Is.EqualTo(token));
        }

        [TestCase("X")]
        [TestCase("O")]
        public void FillShouldUpdateCurrentTokenIfSquareIsEmpty(string token)
        {
            var subject = new Square();
            subject.Fill(token);

            var result = subject.CurrentToken;

            Assert.That(result, Is.EqualTo(token));
        }
        
        [TestCase("X")]
        public void FillShouldNotChangeCurrentTokenIfSquareIsOccupied(string token)
        {
            var subject = new Square("O");

            var result = subject.CurrentToken;

            Assert.That(result, Is.Not.EqualTo(token));
        }

        [TestCase("X")]
        public void FillShouldThrowArgumentErrorIfSquareIsOccupied(string token)
        {
            var subject = new Square("O");

            Assert.That
            (
                () => subject.Fill(token),
                Throws.ArgumentException
            );
        }

        [TestCase("X", "X", true)]
        [TestCase("X", "O", false)]
        [TestCase("X", "", false)]
        public void SquaresShouldCompareByToken(
            string firstToken, string secondToken, bool expected
        )
        {
            var subject = new Square(firstToken);
            var compare = new Square(secondToken);

            bool result = subject == compare;
            
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
