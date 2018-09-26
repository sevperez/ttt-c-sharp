using NUnit.Framework;
using TTTCore;
using System.Linq;

namespace ArtificialIntelligence.AIClass.UnitTests
{
    [TestFixture]
    public class AI_Tests
    {
        private AI subject;

        [SetUp]
        public void Init()
        {
            var analyzer = new TTTBoardAnalyzer();
            var scorer = new TTTBoardScorer("X", "O");
            subject = new AI("X", "O", analyzer, scorer);
        }

        [Test]
        public void ChooseMoveShouldReturnIndexOfAWinningMoveIfAvailable()
        {
            string[] testTokens = new string[] {
                "O", "", "",
                "O", "X", "O",
                "", "X", "O"
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = true;
            var winningMoveOption1 = new MoveOption(1, MMConstants.MINIMAX_MAX);
            var winningMoveOption2 = new MoveOption(6, MMConstants.MINIMAX_MAX);
            var winningMoves = new MoveOption[] { winningMoveOption1, winningMoveOption2 };
            var winningMoveIndices = winningMoves.Select(move => move.Location);

            var result = subject.ChooseMove(testBoard, ownerMovesNext);

            Assert.That(winningMoveIndices, Has.Member(result));
        }

        [Test]
        public void ChooseMoveShouldReturnIndexOfABlockingMoveIfLoseImminent()
        {
            string[] testTokens = new string[] {
                "", "", "O", 
                "", "X", "O", 
                "X", "", ""
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = true;

            var result = subject.ChooseMove(testBoard, ownerMovesNext);
            var expected = 8;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetInitialDepthShouldReturnMaxConstantIfBoardIsLarge()
        {
            var board = new Board(5);

            var result = subject.GetInitialDepth(board);

            Assert.That(result, Is.EqualTo(MMConstants.MAX_MINIMAX_DEPTH));
        }

        [Test]
        public void GetInitialDepthShouldReturBoardSizeMinusOne()
        {
            var board = new Board(3);

            var result = subject.GetInitialDepth(board);

            Assert.That(result, Is.EqualTo(2));
        }
    }
}
