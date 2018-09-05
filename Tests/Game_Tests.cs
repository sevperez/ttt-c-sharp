using System;
using NUnit.Framework;
using TTTCore;

namespace GameClass.UnitTests
{
    [TestFixture]
    public class Game_Tests
    {
        private Game subject;

        [SetUp] public void Init()
        {
            subject = new Game();
        }

        [TestCase("1")]
        [TestCase("2")]
        public void SetGameModeShouldHandleValidInput(string gameModeNumberString)
        {
            subject.SetGameMode(gameModeNumberString);

            var result = subject.Mode;
            var expected = (GameModes)Enum.Parse(typeof(GameModes), gameModeNumberString);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("3")]
        [TestCase("")]
        [TestCase("a")]
        public void SetGameModeShouldThrowErrorOnInvalidInput(string gameModeNumberString)
        {
            Assert.That
            (
                () => subject.SetGameMode(gameModeNumberString),
                Throws.ArgumentException
            );
        }

        [TestCase(1)]
        [TestCase(5)]
        public void SetRoundsToWinShouldHandleValidInput(int roundsToWin)
        {
            subject.SetRoundsToWin(roundsToWin);
            
            var result = subject.RoundsToWin;

            Assert.That(result, Is.EqualTo(roundsToWin));
        }

        [TestCase(0)]
        [TestCase(10)]
        public void SetRoundsToWinThrowsErrorOnInvalidInput(int roundsToWin)
        {
            Assert.That
            (
                () => subject.SetRoundsToWin(roundsToWin),
                Throws.Exception
            );
        }

        [TestCase(GameModes.PlayerVsPlayer)]
        public void InstantiatePlayersHandlesPlayerVsPlayer(GameModes mode)
        {
            subject.Mode = mode;
            subject.InstantiatePlayers();

            bool result = subject.Player1 is Human && subject.Player2 is Human;

            Assert.IsTrue(result);
        }

        [TestCase(GameModes.PlayerVsComputer)]
        public void InstantiatePlayersHandlesPlayerVsComputer(GameModes mode)
        {
            subject.Mode = mode;
            subject.InstantiatePlayers();

            bool result = subject.Player1 is Human && subject.Player2 is Computer;

            Assert.IsTrue(result);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void SetFirstPlayerShouldHandleValidInput(int playerNumber)
        {
            subject.SetFirstPlayer(playerNumber);

            var result = subject.NextPlayerNumber;

            Assert.That(result, Is.EqualTo(playerNumber));
        }

        [TestCase(0)]
        [TestCase(3)]
        public void SetFirstPlayerShouldHandleInvalidInput(int playerNumber)
        {
            Assert.That
            (
                () => subject.SetFirstPlayer(playerNumber),
                Throws.Exception
            );
        }

        [Test]
        public void IncrementWinnerScoreShouldUpdateWinnerNumWins()
        {
            var player1 = new Human();
            player1.Token = "X";
            player1.NumWins = 2;
            var player2 = new Human();
            player2.Token = "O";
            player2.NumWins = 0;
            subject.Player1 = player1;
            subject.Player2 = player2;

            subject.IncrementWinnerScore(player1.Token);
            var result = player1.NumWins;

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GetGameWinnerNameShouldReturnNameIfWinner()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            player1.NumWins = 2;
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            player2.NumWins = 5;
            subject.Player1 = player1;
            subject.Player2 = player2;
            subject.RoundsToWin = 5;

            var result = subject.GetGameWinnerName();

            Assert.That(result, Is.EqualTo("Leela"));
        }

        [Test]
        public void GetRoundWinnerNameShouldReturnNameIfWinner()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            subject.Player1 = player1;
            subject.Player2 = player2;
            var winningToken = player1.Token;

            var result = subject.GetRoundWinnerName(winningToken);

            Assert.That(result, Is.EqualTo("Fry"));
        }

        [Test]
        public void GetRoundWinnerNameShouldReturnNullIfNoWinner()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            subject.Player1 = player1;
            subject.Player2 = player2;
            string winningToken = null;

            var result = subject.GetRoundWinnerName(winningToken);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void CheckGameOverShouldReturnTrueIfGameOver()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            player1.NumWins = 2;
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            player2.NumWins = 5;
            subject.Player1 = player1;
            subject.Player2 = player2;
            subject.RoundsToWin = 5;

            var result = subject.CheckGameOver();

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckGameOverShouldReturnFalseIfGameNotOver()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            player1.NumWins = 2;
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            player2.NumWins = 2;
            subject.Player1 = player1;
            subject.Player2 = player2;
            subject.RoundsToWin = 5;

            var result = subject.CheckGameOver();

            Assert.IsFalse(result);
        }

        [Test]
        public void CheckRoundOverShouldReturnTrueIfRoundWinner()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            string[] tokens = new string[] { 
                "X", "X", "O", 
                "", "X", "O", 
                "O", "X", ""
            };
            var board = new Board(tokens);
            subject.Player1 = player1;
            subject.Player2 = player2;
            subject.Board = board;

            var result = subject.CheckRoundOver();

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckRoundOverShouldReturnTrueIfBoardFull()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            string[] tokens = new string[] { 
                "X", "X", "O", 
                "O", "O", "X", 
                "X", "O", "X"
            };
            var board = new Board(tokens);
            subject.Player1 = player1;
            subject.Player2 = player2;
            subject.Board = board;

            var result = subject.CheckRoundOver();

            Assert.IsTrue(result);
        }

        [Test]
        public void CheckRoundOverShouldReturnFalseIfNoWinnerAndBoardNotFull()
        {
            var player1 = new Human();
            player1.Name = "Fry";
            player1.Token = "X";
            var player2 = new Human();
            player2.Name = "Leela";
            player2.Token = "O";
            string[] tokens = new string[] { 
                "X", "X", "O", 
                "", "X", "O", 
                "X", "O", ""
            };
            var board = new Board(tokens);
            subject.Player1 = player1;
            subject.Player2 = player2;
            subject.Board = board;

            var result = subject.CheckRoundOver();

            Assert.IsFalse(result);
        }

        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void AlternateNextPlayerNumberShouldSwitchNextPlayerNumber(
            int nextPlayer, int expected
        )
        {
            subject.NextPlayerNumber = nextPlayer;

            subject.AlternateNextPlayer();
            var result = subject.NextPlayerNumber;

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}