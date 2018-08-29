using NUnit.Framework;
using TTTCore;

namespace AIClass.UnitTests
{
    [TestFixture]
    public class AI_Tests
    {
        private AI subject;

        [SetUp]
        public void Init()
        {
            subject = new AI("X");
        }

        [Test]
        public void GetEmptySquareIndicesShoudlReturnEmptyIndices()
        {
            string[] existingTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var testBoard = new Board(existingTokens);

            var result = subject.GetEmptySquareIndices(testBoard);
            var expected = new int[] { 1, 6, 7 };

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void SimulateMoveShouldReturnNewBoardWithSquareFilledAtIndex()
        {
            string[] existingTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var inputBoard = new Board(existingTokens);

            var result = subject.SimulateMove(inputBoard, 1);
            var newTokens = new string[] {
                "X", "X", "X", "O", "X", "O", "", "", "O"
            };
            var expected = new Board(newTokens);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void SimulateMoveShouldNotMutateInputBoard()
        {
            string[] existingTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var inputBoard = new Board(existingTokens);

            subject.SimulateMove(inputBoard, 1);
            var original = new Board(existingTokens);

            Assert.That(inputBoard, Is.EqualTo(original));
        }

        [Test]
        [Ignore("temp ignore")]
        public void GetPossibleBoardStatesShouldReturnPossibleNextBoards()
        {
            string[] currentTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var currentBoard = new Board(currentTokens);

            string[] newTokens1 = new string[] {
                "X", "X", "X", "O", "X", "O", "", "", "O"
            };
            var newBoard1 = new Board(newTokens1);
            
            string[] newTokens2 = new string[] {
                "X", "", "X", "O", "X", "O", "X", "", "O"
            };
            var newBoard2 = new Board(newTokens2);
            
            string[] newTokens3 = new string[] {
                "X", "", "X", "O", "X", "O", "", "X", "O"
            };
            var newBoard3 = new Board(newTokens3);

            Board[] result = subject.GetPossibleBoardStates(currentBoard);
            var expected = new Board[] { newBoard1, newBoard2, newBoard3 };

            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}
