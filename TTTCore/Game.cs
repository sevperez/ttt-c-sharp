using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MM.AI;

namespace TTTCore
{
    public class Game
    {
        public IGameInterface GameInterface;
        public int RoundsToWin { get; set; }
        public int NextPlayerNumber { get; set; }
        public int BoardSizeSelection { get; set; }
        public GameModes Mode { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Round Round { get; set; }
        
        public Game(IGameInterface gameInterface = null)
        {
            if (gameInterface == null)
            {
                this.GameInterface = new CLI();
            }
            else
            {
                this.GameInterface = gameInterface;
            }
        }

        public void Start()
        {
            this.WelcomeScreen();
            this.GameSetup();
            this.Play();
        }

        public void Play()
        {
            while (!this.CheckGameOver())
            {
                this.Round = new Round(
                    this.GameInterface, this.Mode, this.BoardSizeSelection,
                    this.RoundsToWin, this.NextPlayerNumber, this.Player1, this.Player2
                );
                this.Round.Play();
                this.AlternateNextPlayer();
                Thread.Sleep(1000);
            }
            
            this.HandleGameEnd();
        }

        public bool CheckGameOver()
        {
            var gameOver = this.RoundsToWin == this.Player1.NumWins ||
                this.RoundsToWin == this.Player2.NumWins;
            
            return gameOver;
        }

        public void HandleGameEnd()
        {
            string winnerName = this.GetGameWinnerName();

            this.GameInterface.DrawGameEnd
            (
                this.Player1, this.Player2,
                this.RoundsToWin, this.Round.Board, winnerName
            );
        }

        public string GetGameWinnerName()
        {
            if (this.Player1.NumWins == this.RoundsToWin)
            {
                return this.Player1.Name;
            }
            else
            {
                return this.Player2.Name;
            }
        }

        public void AlternateNextPlayer()
        {
            if (this.NextPlayerNumber == 1)
            {
                this.NextPlayerNumber = 2;
            }
            else
            {
                this.NextPlayerNumber = 1;
            }
        }

        public void WelcomeScreen()
        {
            Console.Clear();
            GameInterface.WelcomeMessage();
            Thread.Sleep(1000);
        }

        public void GameSetup()
        {
            this.HandleGameModeSetup();
            this.InstantiatePlayers();
            this.HandlePlayerNamesSetup();
            this.HandlePlayerTokensSetup();
            this.HandleBoardSizeSelection();
            this.HandleNumRoundsSetup();
            this.HandleFirstPlayerSelection();

            if (this.Mode == GameModes.PlayerVsComputer)
            {
                this.Player2.ai = new AI(this.Player2.Token, this.Player1.Token);
            }
        }

        public void InstantiatePlayers()
        {
            this.Player1 = new Human();

            if (this.Mode == GameModes.PlayerVsComputer)
            {
                this.Player2 = new Computer();
            }
            else
            {
                this.Player2 = new Human();
            }
        }

        public void HandleGameModeSetup()
        {
            Console.Clear();
            this.SetGameMode(GameInterface.GetGameModeSelection());
        }

        public void SetGameMode(string gameModeNumber)
        {
            var mode = (GameModes)Enum.Parse(typeof(GameModes), gameModeNumber);
            
            if (Enum.IsDefined(typeof(GameModes), mode))
            {
                this.Mode = mode;
            }
            else
            {
                throw new ArgumentException("Invalid game mode selection.");
            }
        }

        public void HandlePlayerNamesSetup()
        {
            this.HandleHumanPlayerNameSetup(1);

            if (this.Mode == GameModes.PlayerVsPlayer)
            {
                this.HandleHumanPlayerNameSetup(2, this.Player1.Name);
            }
            else
            {
                this.Player2.SetPlayerName();
            }
        }

        public void HandleHumanPlayerNameSetup(int playerNumber, string invalidName = "")
        {
            var currentPlayer = playerNumber == 1 ? this.Player1 : this.Player2;

            Console.Clear();
            var name = GameInterface.GetPlayerNameSelection(playerNumber, invalidName);
            currentPlayer.SetPlayerName(name);
        }

        public void HandlePlayerTokensSetup()
        {
            this.HandleHumanPlayerTokenSetup(1);

            if (this.Mode == GameModes.PlayerVsPlayer)
            {
                this.HandleHumanPlayerTokenSetup(2, this.Player1.Token);
            }
            else
            {
                this.Player2.SetPlayerToken(this.Player1.Token);
            }
        }

        public void HandleHumanPlayerTokenSetup(int playerNumber, string invalidToken = "")
        {
            var currentPlayer = playerNumber == 1 ? this.Player1 : this.Player2;

            Console.Clear();
            var token = GameInterface.GetPlayerTokenSelection(playerNumber, invalidToken);
            currentPlayer.SetPlayerToken(token);
        }

        public void HandleNumRoundsSetup()
        {
            Console.Clear();
            this.SetRoundsToWin(GameInterface.GetRoundsToWinSelection());
        }

        public void SetRoundsToWin(int roundsToWin)
        {  
            if (roundsToWin >= 1 && roundsToWin <= 9)
            {
                this.RoundsToWin = roundsToWin;
            }
            else
            {
                throw new ArgumentException("Rounds to win must be in range 1..9");
            }
        }

        public void HandleFirstPlayerSelection()
        {
            Console.Clear();
            int selection = (GameInterface.GetFirstPlayerSelection(this.Player1, this.Player2));
            this.SetFirstPlayer(selection);
        }

        public void HandleBoardSizeSelection()
        {
            Console.Clear();
            int selection = (GameInterface.GetBoardSizeSelection());

            if (selection == 1 || selection == 2 || selection == 3)
            {
                this.BoardSizeSelection = selection + 2;
            }
            else
            {
                throw new ArgumentException("Invalid board size selection.");
            }
        }

        public void SetFirstPlayer(int playerNumber)
        {
            if (playerNumber == 1 || playerNumber == 2)
            {
                this.NextPlayerNumber = playerNumber;
            }
            else
            {
                throw new ArgumentException("Invalid first player selection.");
            }
        }
    }
}
