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
            subject = new AI("X", "O");
        }

        [Test]
        public void GetEmptySquareIndicesShouldReturnEmptyIndices()
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
            var moveIndex = 1;
            var moveToken = "X";

            var result = subject.SimulateMove(inputBoard, moveIndex, moveToken);
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
            var moveIndex = 1;
            var moveToken = "X";

            subject.SimulateMove(inputBoard, moveIndex, moveToken);
            var originalContent = new Board(existingTokens);

            Assert.That(inputBoard, Is.EqualTo(originalContent));
        }

        [Test]
        public void GetPossibleBoardStatesShouldReturnPossibleNextBoards()
        {
            string[] currentTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var currentBoard = new Board(currentTokens);
            var nextMoveToken = "X";

            string[] newTokens1 = new string[] {
                "X", nextMoveToken, "X", "O", "X", "O", "", "", "O"
            };
            var newBoard1 = new Board(newTokens1);
            
            string[] newTokens2 = new string[] {
                "X", "", "X", "O", "X", "O", nextMoveToken, "", "O"
            };
            var newBoard2 = new Board(newTokens2);
            
            string[] newTokens3 = new string[] {
                "X", "", "X", "O", "X", "O", "", nextMoveToken, "O"
            };
            var newBoard3 = new Board(newTokens3);

            Board[] result = subject.GetPossibleBoardStates(currentBoard, nextMoveToken);
            var expected = new Board[] { newBoard1, newBoard2, newBoard3 };

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetMiniMaxScoreShouldReturnPositiveIfAIWin()
        {
            string[] testTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = true;
            
            var result = subject.GetMiniMaxScore(testBoard, ownerMovesNext);
            var expected = 10;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetMiniMaxScoreShouldReturnNegativeIfOpponentWin()
        {
            string[] testTokens = new string[] {
                "O", "X", "X", "X", "", "X", "", "O", "O"
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = false;

            var result = subject.GetMiniMaxScore(testBoard, ownerMovesNext);
            var expected = -10;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [Ignore("Not yet implemented")]
        public void GetMiniMaxScoreShouldReturnZeroIfDraw()
        {
            string[] testTokens = new string[] {
                "O", "O", "", "X", "X", "O", "O", "X", "X"
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = true;

            var result = subject.GetMiniMaxScore(testBoard, ownerMovesNext);
            var expected = 0;

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
