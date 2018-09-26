using NUnit.Framework;
using TTTCore;
using System.Linq;

namespace ArtificialIntelligence.AIClass.UnitTests
{
    [TestFixture]
    public class Minimax_Tests
    {
        private Minimax subject;

        [SetUp]
        public void Init()
        {
            subject = new Minimax("X", "O");
            subject.Scorer = new TTTBoardScorer("X", "O");
            subject.BoardAnalyzer = new TTTBoardAnalyzer();
        }

        [Test]
        public void MinimaxShouldReturnPositiveIfAIWin()
        {
            string[] testTokens = new string[] {
                "X", "", "X",
                "O", "X", "O",
                "", "", "O"
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = true;
            var alpha = MMConstants.MIN;
            var beta = MMConstants.MAX;
            var depth = 2;

            var result = subject.CalculateScore(testBoard, depth, ownerMovesNext, alpha, beta);
            var expected = MMConstants.MINIMAX_MAX;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void MinimaxShouldReturnNegativeIfOpponentWin()
        {
            string[] testTokens = new string[] {
                "X", "", "O",
                "", "X", "O",
                "X", "", ""
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = false;
            var alpha = MMConstants.MIN;
            var beta = MMConstants.MAX;
            var depth = 2;

            var result = subject.CalculateScore(testBoard, depth, ownerMovesNext, alpha, beta);
            var expected = MMConstants.MINIMAX_MIN;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void MinimaxShouldReturnNegativeIfOpponentGuaranteedWin()
        {
            string[] testTokens = new string[] {
                "", "O", "O",
                "", "X", "O",
                "X", "", ""
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = true;
            var alpha = MMConstants.MIN;
            var beta = MMConstants.MAX;
            var depth = 2;

            var result = subject.CalculateScore(testBoard, depth, ownerMovesNext, alpha, beta);
            var expected = MMConstants.MINIMAX_MIN;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void MinimaxShouldReturnZeroIfDraw()
        {
            string[] testTokens = new string[] {
                "O", "O", "",
                "X", "X", "O",
                "O", "X", "X"
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = true;
            var alpha = MMConstants.MIN;
            var beta = MMConstants.MAX;
            var depth = 2;

            var result = subject.CalculateScore(testBoard, depth, ownerMovesNext, alpha, beta);
            var expected = 0;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void IsLeafReturnsTrueIfBoardTerminalFull()
        {
            string[] tokens = new string[] {
                "X", "O", "X",
                "O", "X", "O",
                "O", "X", "O"
            };
            var board = new Board(tokens);

            var result = subject.IsLeaf(board);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsLeafReturnsTrueIfBoardTerminalWithWinner()
        {
            string[] tokens = new string[] {
                "", "O", "X",
                "O", "X", "O",
                "X", "X", ""
            };
            var board = new Board(tokens);

            var result = subject.IsLeaf(board);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsLeafReturnsFalseIfBoardNotTerminal()
        {
            string[] tokens = new string[] {
                "", "O", "X",
                "O", "X", "O",
                "", "X", ""
            };
            var board = new Board(tokens);

            var result = subject.IsLeaf(board);

            Assert.IsFalse(result);
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

            IBoard[] result = subject.GetPossibleBoardStates(currentBoard, nextMoveToken);
            var expected = new Board[] { newBoard1, newBoard2, newBoard3 };

            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}
