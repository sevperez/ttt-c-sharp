using NUnit.Framework;
using TTTCore;

namespace SquareClass.UnitTests {
    [TestFixture]
    public class Square_Tests {
        private Square _subject;

        [Test]
        public void ConstructorShouldInitializeWithEmptyCurrentTokenByDefault() {
            _subject = new Square();

            var result = _subject.CurrentToken;

            Assert.That(result, Is.EqualTo(""));
        }

        [TestCase("X")]
        public void ConstructorShouldInitializeWithToken(string token) {
            _subject = new Square(token);

            var result = _subject.CurrentToken;

            Assert.That(result, Is.EqualTo(token));
        }

        [TestCase("X")]
        [TestCase("O")]
        public void FillShouldUpdateCurrentTokenIfSquareIsEmpty(string token) {
            _subject = new Square();
            _subject.Fill(token);

            var result = _subject.CurrentToken;

            Assert.That(result, Is.EqualTo(token));
        }
        
        [TestCase("X")]
        public void FillShouldNotChangeCurrentTokenIfSquareIsOccupied(string token) {
            _subject = new Square("O");

            var result = _subject.CurrentToken;

            Assert.That(result, Is.Not.EqualTo(token));
        }

        [TestCase("X")]
        public void FillShouldThrowArgumentErrorIfSquareIsOccupied(string token) {
            _subject = new Square("O");

            Assert.That(() => _subject.Fill(token), Throws.ArgumentException);
        }

        [TestCase("X", "X", true)]
        [TestCase("X", "O", false)]
        [TestCase("X", "", false)]
        public void SquaresShouldCompareByToken(
            string firstToken, string secondToken, bool expected
        ) {
            _subject = new Square(firstToken);
            Square compare = new Square(secondToken);

            bool result = _subject == compare;
            
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
