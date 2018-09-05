using NUnit.Framework;
using System;
using System.IO;
using TTTCore;

namespace CLI_Class.UnitTests
{
    [TestFixture]
    public class CLI_Tests
    {
        public StringWriter sw { get; set; }
        public StringReader sr { get; set; }

        [SetUp] public void Init()
        {
            sw = new StringWriter();
            Console.SetOut(sw);
        }

        [TearDown] public void Cleanup()
        {
            var stdout = new StreamWriter(Console.OpenStandardOutput());
            stdout.AutoFlush = true;
            Console.SetOut(stdout);
            sw.Dispose();

            if (sr != null)
            {
                var stdin = new StreamReader(Console.OpenStandardInput());
                Console.SetIn(stdin);
                sr.Dispose();
            }
        }

        [Test]
        public void WelcomeMessageShouldDisplayInConsole()
        {
            var subject = new CLI();

            subject.WelcomeMessage();
            var result = sw.ToString();
            var expected = Constants.MainBanner + Constants.Messages["welcome"];
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetGameModeSelectionShouldReturnStringInRange1To2()
        {
            var subject = new CLI();
            var expected = "1";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            string result = subject.GetGameModeSelection();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetRoundsToWinShouldReturnIntInRange1To9()
        {
            var subject = new CLI();
            var expected = "5";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            int result = subject.GetRoundsToWinSelection();

            Assert.That(result, Is.EqualTo(Int32.Parse(expected)));
        }

        [Test]
        public void GetPlayerNameSelectionShouldReturnString()
        {
            var subject = new CLI();
            var testPlayerNumber = 1;
            var expected = "Fry";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            string result = subject.GetPlayerNameSelection(testPlayerNumber);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetPlayerTokenSelectionShouldReturnString()
        {
            var subject = new CLI();
            var testPlayerNumber = 1;
            var expected = "X";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            string result = subject.GetPlayerTokenSelection(testPlayerNumber);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetFirstPlayerSelectionShouldReturnIntInRange1To2()
        {
            var subject = new CLI();
            var player1 = new Player();
            var player2 = new Player();
            var expected = "2";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            int result = subject.GetFirstPlayerSelection(player1, player2);

            Assert.That(result, Is.EqualTo(Int32.Parse(expected)));
        }

        [Test]
        public void DrawRoundBannerShouldDrawCurrentScores()
        {
            var subject = new CLI();

            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            player1.NumWins = 2;
            
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            player2.NumWins = 5;
            
            var numRounds = 9;

            var expected = "Fry (X): 2/9; Leela (O): 5/9\n\n";

            subject.DrawRoundBanner(player1, player2, numRounds);
            var result = sw.ToString();
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DrawGameBoardShouldDrawBoardWithCurrentTokens()
        {
            var subject = new CLI();
            string[] currentTokens = new string[] {
                "X", "", "O", "O", "", "X", "X", "", "O"
            };
            var board = new Board(currentTokens);

            var expected =
                "             |     |     \n" +
                "          X  |     |  O  \n" +
                "             |     |     \n" +
                "        -----------------\n" +
                "             |     |     \n" +
                "          O  |     |  X  \n" +
                "             |     |     \n" +
                "        -----------------\n" +
                "             |     |     \n" +
                "          X  |     |  O  \n" +
                "             |     |     \n\n";

            subject.DrawGameBoard(currentTokens);
            var result = sw.ToString();
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DrawGameBoardShouldDrawEmptyBoardIfNoTokens()
        {
            var subject = new CLI();
            string[] currentTokens = new string[] {
                "", "", "", "", "", "", "", "", ""
            };

            var expected =
                "             |     |     \n" +
                "             |     |     \n" +
                "             |     |     \n" +
                "        -----------------\n" +
                "             |     |     \n" +
                "             |     |     \n" +
                "             |     |     \n" +
                "        -----------------\n" +
                "             |     |     \n" +
                "             |     |     \n" +
                "             |     |     \n\n";

            subject.DrawGameBoard(currentTokens);
            var result = sw.ToString();
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void RequestMoveOptionsShouldDisplayAllOptionsOnEmptyBoard()
        {
            var subject = new CLI();
            var board = new Board();
            var emptyIndices = board.GetEmptySquareIndices();
            var player = new Human();
            player.Name = "Fry";

            subject.RequestMoveMessage(player, emptyIndices);
            var result = sw.ToString();
            var expected = "Fry's Move!\n" + 
                           "Please choose a square:\n" +
                           "1, 2, 3, 4, 5, 6, 7, 8, 9\n";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void RequestMoveOptionsShouldDisplayAvailableOptionsOnBoard()
        {
            var subject = new CLI();
            string[] currentTokens = new string[] {
                "X", "", "O", "O", "", "X", "X", "", "O"
            };
            var board = new Board(currentTokens);
            var emptyIndices = board.GetEmptySquareIndices();
            var player = new Human();
            player.Name = "Fry";

            subject.RequestMoveMessage(player, emptyIndices);
            var result = sw.ToString();
            var expected = "Fry's Move!\n" + 
                           "Please choose a square:\n" +
                           "2, 5, 8\n";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetPlayerMoveSelectionShouldReturnInt()
        {
            var subject = new CLI();
            var board = new Board();
            var player = new Human();
            int expected = 0;
            player.Name = "Fry";

            sr = new StringReader("1");
            Console.SetIn(sr);

            int result = subject.GetPlayerMoveSelection(player, board);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
