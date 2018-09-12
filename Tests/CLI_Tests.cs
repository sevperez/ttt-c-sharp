using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using TTTCore;
using IConsoleInterface.Tests;

namespace CLI_Class.UnitTests
{
    [TestFixture]
    public class CLI_Tests
    {
        [Test]
        public void WelcomeMessageShouldDisplayInConsole()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);

            subject.WelcomeMessage();
            var result = String.Join("", testConsole.ConsoleOutputList);
            var expected = Constants.MainBanner + Constants.Messages["welcome"];
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetGameModeSelectionShouldReturnStringInRange1To2()
        {
            var readInputs = new List<String>() { "1\n" };
            var testConsole = new FakeConsole(readInputs);
            var subject = new CLI(testConsole);

            string result = subject.GetGameModeSelection();
            int expectedIndex = testConsole.ConsoleOutputList.Count - 1;
            string expected = readInputs[0].Trim();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetRoundsToWinShouldReturnIntInRange1To9()
        {
            var readInputs = new List<String>() { "5\n" };
            var testConsole = new FakeConsole(readInputs);
            var subject = new CLI(testConsole);

            int result = subject.GetRoundsToWinSelection();
            int expectedIndex = testConsole.ConsoleOutputList.Count - 1;
            string expected = readInputs[0].Trim();

            Assert.That(result, Is.EqualTo(Int32.Parse(expected)));
        }

        [Test]
        public void GetPlayerNameSelectionShouldReturnString()
        {
            var inputString = "Fry\n";
            var testPlayerNumber = 1;
            var readInputs = new List<String>() { inputString };
            var testConsole = new FakeConsole(readInputs);
            var subject = new CLI(testConsole);

            string result = subject.GetPlayerNameSelection(testPlayerNumber);
            string expected = inputString.Trim();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetPlayerTokenSelectionShouldReturnString()
        {
            var inputString = "X\n";
            var testPlayerNumber = 1;
            var readInputs = new List<String>() { inputString };
            var testConsole = new FakeConsole(readInputs);
            var subject = new CLI(testConsole);

            string result = subject.GetPlayerTokenSelection(testPlayerNumber);
            var expected = inputString.Trim();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetFirstPlayerSelectionShouldReturnIntInRange1To2()
        {
            var inputString = "2\n";
            var readInputs = new List<String>() { inputString };
            var testConsole = new FakeConsole(readInputs);
            var subject = new CLI(testConsole);
            var player1 = new Player();
            var player2 = new Player();

            int result = subject.GetFirstPlayerSelection(player1, player2);
            var expected = Int32.Parse(inputString.Trim());

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetBoardSizeSelectionShouldReturnIntInRange1To3()
        {
            var inputString = "3\n";
            var readInputs = new List<String>() { inputString };
            var testConsole = new FakeConsole(readInputs);
            var subject = new CLI(testConsole);

            int result = subject.GetBoardSizeSelection();
            var expected = Int32.Parse(inputString.Trim());

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DrawRoundBannerShouldDrawCurrentScores()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);

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
            var result = String.Join("", testConsole.ConsoleOutputList);
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DrawGameBoardShouldDrawDefaultSizeBoardWithCurrentTokens()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);
            string[] currentTokens = new string[] {
                "X", "", "O", "O", "", "X", "X", "", "O"
            };
            var board = new Board(currentTokens);
            var boardSize = board.BoardSize;

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

            subject.DrawGameBoard(currentTokens, boardSize);
            var result = String.Join("", testConsole.ConsoleOutputList);
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DrawGameBoardShouldDrawDefautlSizeEmptyBoardIfNoTokens()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);
            string[] currentTokens = new string[] {
                "", "", "", "", "", "", "", "", ""
            };
            var board = new Board(currentTokens);
            var boardSize = board.BoardSize;

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

            subject.DrawGameBoard(currentTokens, boardSize);
            var result = String.Join("", testConsole.ConsoleOutputList);
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void DrawGameBoardShouldDraw4x4BoardWithCurrentTokens()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);
            string[] currentTokens = new string[] {
                "X", "", "O", "X",
                "O", "X", "", "X",
                "", "O", "", "X",
                "O", "X", "X", "O"
            };
            var boardSize = 4;
            var board = new Board(boardSize, currentTokens);

            var expected =
                "             |     |     |     \n" +
                "          X  |     |  O  |  X  \n" +
                "             |     |     |     \n" +
                "        -----------------------\n" +
                "             |     |     |     \n" +
                "          O  |  X  |     |  X  \n" +
                "             |     |     |     \n" +
                "        -----------------------\n" +
                "             |     |     |     \n" +
                "             |  O  |     |  X  \n" +
                "             |     |     |     \n" +
                "        -----------------------\n" +
                "             |     |     |     \n" +
                "          O  |  X  |  X  |  O  \n" +
                "             |     |     |     \n\n";

            subject.DrawGameBoard(currentTokens, boardSize);
            var result = String.Join("", testConsole.ConsoleOutputList);
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void BuildGameBoardShouldBuild3x3BoardString()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);

            var result = subject.BuildGameBoard(3);

            var expected =
                "             |     |     \n" +
                "          {0}  |  {1}  |  {2}  \n" +
                "             |     |     \n" +
                "        -----------------\n" +
                "             |     |     \n" +
                "          {3}  |  {4}  |  {5}  \n" +
                "             |     |     \n" +
                "        -----------------\n" +
                "             |     |     \n" +
                "          {6}  |  {7}  |  {8}  \n" +
                "             |     |     \n\n";
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void BuildGameBoardShouldBuild4x4BoardString()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);

            var result = subject.BuildGameBoard(4);

            var expected =
                "             |     |     |     \n" +
                "          {0}  |  {1}  |  {2}  |  {3}  \n" +
                "             |     |     |     \n" +
                "        -----------------------\n" +
                "             |     |     |     \n" +
                "          {4}  |  {5}  |  {6}  |  {7}  \n" +
                "             |     |     |     \n" +
                "        -----------------------\n" +
                "             |     |     |     \n" +
                "          {8}  |  {9}  |  {10}  |  {11}  \n" +
                "             |     |     |     \n" +
                "        -----------------------\n" +
                "             |     |     |     \n" +
                "          {12}  |  {13}  |  {14}  |  {15}  \n" +
                "             |     |     |     \n\n";
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void RequestMoveOptionsShouldDisplayAllOptionsOnEmpty3x3Board()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);
            var board = new Board();
            var emptyIndices = board.GetEmptySquareIndices();
            var player = new Human();
            player.Name = "Fry";

            subject.RequestMoveMessage(player, emptyIndices);
            var result = String.Join("", testConsole.ConsoleOutputList);
            var expected = "Fry's Move!\n" + 
                           "Please choose a square:\n" +
                           "1, 2, 3, 4, 5, 6, 7, 8, 9\n";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void RequestMoveOptionsShouldDisplayAllOptionsOnEmpty4x4Board()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);
            var boardSize = 4;
            var board = new Board(boardSize);
            var emptyIndices = board.GetEmptySquareIndices();
            var player = new Human();
            player.Name = "Fry";

            subject.RequestMoveMessage(player, emptyIndices);
            var result = String.Join("", testConsole.ConsoleOutputList);
            var expected = "Fry's Move!\n" + 
                           "Please choose a square:\n" +
                           "1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16\n";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void RequestMoveOptionsShouldDisplayAvailableOptionsOn3x3Board()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);
            string[] currentTokens = new string[] {
                "X", "", "O", "O", "", "X", "X", "", "O"
            };
            var board = new Board(currentTokens);
            var emptyIndices = board.GetEmptySquareIndices();
            var player = new Human();
            player.Name = "Fry";

            subject.RequestMoveMessage(player, emptyIndices);
            var result = String.Join("", testConsole.ConsoleOutputList);
            var expected = "Fry's Move!\n" + 
                           "Please choose a square:\n" +
                           "2, 5, 8\n";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void RequestMoveOptionsShouldDisplayAvailableOptionsOn4x4Board()
        {
            var testConsole = new FakeConsole();
            var subject = new CLI(testConsole);
            string[] currentTokens = new string[] {
                "X", "", "O", "X",
                "O", "X", "", "X",
                "", "O", "", "X",
                "O", "X", "X", "O"
            };
            var boardSize = 4;
            var board = new Board(boardSize, currentTokens);
            var emptyIndices = board.GetEmptySquareIndices();
            var player = new Human();
            player.Name = "Fry";

            subject.RequestMoveMessage(player, emptyIndices);
            var result = String.Join("", testConsole.ConsoleOutputList);
            var expected = "Fry's Move!\n" + 
                           "Please choose a square:\n" +
                           "2, 7, 9, 11\n";

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetPlayerMoveSelectionShouldReturnInt()
        {
            var inputString = "1\n";
            var readInputs = new List<String>() { inputString };
            var testConsole = new FakeConsole(readInputs);
            var subject = new CLI(testConsole);
            var board = new Board();
            var player = new Human();
            player.Name = "Fry";
            var expected = 0;

            int result = subject.GetPlayerMoveSelection(player, board);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
