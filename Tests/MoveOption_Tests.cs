using NUnit.Framework;

namespace ArtificialIntelligence.MoveOptionClass.UnitTests
{
    [TestFixture]
    public class MoveOption_Tests
    {
        [Test]
        public void SquaresShouldCompareTrueIfSquareIndexAndMiniMaxScoreMatch()
        {
            var squareIndex = 2;
            var miniMaxScore = 10;

            var subject = new MoveOption(squareIndex, miniMaxScore);
            var compare = new MoveOption(squareIndex, miniMaxScore);

            bool result = subject == compare;
            
            Assert.IsTrue(result);
        }

        [Test]
        public void SquaresShouldCompareFalseIfSquareIndexAndMiniMaxScoreDoNotMatch()
        {
            var subject = new MoveOption(2, 10);
            var compare = new MoveOption(2, 0);

            bool result = subject == compare;
            
            Assert.IsFalse(result);
        }
    }
}
