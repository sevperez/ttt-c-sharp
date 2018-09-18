using NUnit.Framework;
using TTTCore;
using System.Linq;

namespace MM.AI.BoardScorerClass.UnitTests
{
    [TestFixture]
    public class BoardScorer_Tests
    {
        [Test]
        public void GetTerminalBoardScoreReturnsPositiveIfOwnerWinner()
        {
            string[] tokens = new string[] {
                "", "O", "X",
                "O", "X", "O",
                "X", "X", ""
            };
            var board = new Board(tokens);
            var subject = new BoardScorer(board, "X", "O");

            var result = subject.GetTerminalBoardScore();

            Assert.That(result, Is.EqualTo(MMConstants.MINIMAX_MAX));
        }

        [Test]
        public void GetTerminalBoardScoreReturnsNegativeIfOpponentWinner()
        {
            string[] tokens = new string[] {
                "O", "O", "O",
                "X", "X", "O",
                "X", "", ""
            };
            var board = new Board(tokens);
            var subject = new BoardScorer(board, "X", "O");

            var result = subject.GetTerminalBoardScore();

            Assert.That(result, Is.EqualTo(MMConstants.MINIMAX_MIN));
        }

        [Test]
        public void GetTerminalBoardScoreReturnsZeroIfDraw()
        {
            string[] tokens = new string[] {
                "X", "O", "X",
                "O", "X", "O",
                "O", "X", "O"
            };
            var board = new Board(tokens);
            var subject = new BoardScorer(board, "X", "O");

            var result = subject.GetTerminalBoardScore();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetHeuristicScoreReturnsAppropriateScore()
        {
            string[] tokens = new string[] {
                "X", "X", "",
                "O", "", "X",
                "O", "", ""
            };
            var board = new Board(tokens);
            var subject = new BoardScorer(board, "X", "O");

            var result = subject.GetHeuristicScore();

            Assert.That(result, Is.EqualTo(30));
        }

        [Test]
        public void GetHeuristicLineScoreReturnsZeroIfNoPossibleWinners()
        {
            string[] tokens = new string[] {
                "", "O", "X",
                "O", "X", "O",
                "", "X", ""
            };
            var board = new Board(tokens);
            var subject = new BoardScorer(board, "X", "O");
            int[] line = new int[] { 0, 1, 2 };

            var result = subject.GetHeuristicLineScore(line);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetHeuristicLineScoreReturnsPositiveIfOwnerCanWin()
        {
            string[] tokens = new string[] {
                "O", "", "",
                "O", "X", "",
                "", "X", ""
            };
            var board = new Board(tokens);
            var subject = new BoardScorer(board, "X", "O");
            int[] line = new int[] { 1, 4, 7 };

            var result = subject.GetHeuristicLineScore(line);

            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void GetHeuristicLineScoreReturnsNegativeIfOpponentCanWin()
        {
            string[] tokens = new string[] {
                "O", "O", "",
                "O", "X", "",
                "", "X", ""
            };
            var board = new Board(tokens);
            var subject = new BoardScorer(board, "X", "O");
            int[] line = new int[] { 0, 1, 2 };

            var result = subject.GetHeuristicLineScore(line);

            Assert.That(result, Is.EqualTo(-20));
        }
    }
}
