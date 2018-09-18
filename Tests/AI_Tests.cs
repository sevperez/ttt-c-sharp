using NUnit.Framework;
using TTTCore;
using System.Linq;

namespace MM.AI.AIClass.UnitTests
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
        public void GetTopMoveIndexShouldReturnIndexOfAWinningMoveIfAvailable()
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
            var winningMoveIndices = winningMoves.Select(move => move.SquareIndex);

            var result = subject.GetMiniMaxMove(testBoard, ownerMovesNext);

            Assert.That(winningMoveIndices, Has.Member(result));
        }

        [Test]
        public void GetTopMoveIndexShouldReturnIndexOfABlockingMoveIfLoseImminent()
        {
            string[] testTokens = new string[] {
                "", "", "O", 
                "", "X", "O", 
                "X", "", ""
            };
            var testBoard = new Board(testTokens);
            var ownerMovesNext = true;

            var result = subject.GetMiniMaxMove(testBoard, ownerMovesNext);
            var expected = 8;

            Assert.That(result, Is.EqualTo(expected));
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

            var result = subject.Minimax(testBoard, depth, ownerMovesNext, alpha, beta);
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

            var result = subject.Minimax(testBoard, depth, ownerMovesNext, alpha, beta);
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

            var result = subject.Minimax(testBoard, depth, ownerMovesNext, alpha, beta);
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

            var result = subject.Minimax(testBoard, depth, ownerMovesNext, alpha, beta);
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
        public void SimulateMoveShouldReturnNew3x3BoardWithSquareFilledAtIndex()
        {
            string[] existingTokens = new string[] {
                "X", "", "O", "X",
                "O", "X", "", "X",
                "", "O", "", "X",
                "O", "X", "X", "O"
            };
            var boardSize = 4;
            var inputBoard = new Board(boardSize, existingTokens);
            var moveIndex = 1;
            var moveToken = "X";

            var result = subject.SimulateMove(inputBoard, moveIndex, moveToken);
            var newTokens = new string[] {
                "X", "X", "O", "X",
                "O", "X", "", "X",
                "", "O", "", "X",
                "O", "X", "X", "O"
            };
            var expected = new Board(newTokens);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void SimulateMoveShouldReturnNew4x4BoardWithSquareFilledAtIndex()
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
    }
}
