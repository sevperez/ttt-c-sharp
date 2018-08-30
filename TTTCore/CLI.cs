using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace TTTCore
{
    public class CLI
    {
        public void WelcomeMessage()
        {
            Console.Write(Constants.MainBanner);
            Console.Write(Constants.Messages["welcome"]);
        }

        public string GetGameModeSelection()
        {
            Console.Write(Constants.MainBanner);
            Console.Write(Constants.Messages["requestGameMode"]);
            string selection = Console.ReadLine();

            while (selection != "1" && selection != "2")
            {
                Console.Clear();
                Console.Write(Constants.MainBanner);
                Console.Write(Constants.Messages["gameModeInputError"]);
                selection = Console.ReadLine();
            }
            
            return selection;
        }

        public int GetRoundsToWinSelection()
        {
            Console.Write(Constants.MainBanner);
            Console.Write(Constants.Messages["requestRoundsToWin"]);

            string input = Console.ReadLine();
            int selection;
            bool successfulParse = Int32.TryParse(input, out selection);

            while (!successfulParse || selection < 1 || selection > 9)
            {
                Console.Clear();
                Console.Write(Constants.MainBanner);
                Console.Write(Constants.Messages["roundsToWinInputError"]);
                input = Console.ReadLine();
                successfulParse = Int32.TryParse(input, out selection);
            }
            
            return selection;
        }

        public string GetPlayerNameSelection(int playerNumber, string invalidName = "")
        {
            Console.Write(Constants.MainBanner);
            Console.Write(Constants.Messages["requestPlayerName"], playerNumber);
            string selection = Console.ReadLine();

            while (selection == null || selection == "" || selection == invalidName)
            {
                Console.Clear();
                Console.Write(Constants.MainBanner);
                Console.Write(Constants.Messages["playerNameInputError"], playerNumber);
                selection = Console.ReadLine();
            }
            
            return selection;
        }

        public string GetPlayerTokenSelection(int playerNumber, string invalidToken = "")
        {
            Console.Write(Constants.MainBanner);
            Console.Write(Constants.Messages["requestPlayerToken"], playerNumber);
            string selection = Console.ReadLine();

            while (selection == null || selection.Length != 1 || selection == invalidToken)
            {
                Console.Clear();
                Console.Write(Constants.MainBanner);
                Console.Write(Constants.Messages["playerTokenInputError"], playerNumber);
                selection = Console.ReadLine();
            }
            
            return selection;
        }

        public int GetFirstPlayerSelection(Player player1, Player player2)
        {
            var name1 = player1.Name;
            var name2 = player2.Name;

            Console.Write(Constants.MainBanner);
            Console.Write(Constants.Messages["requestFirstPlayer"], name1, name2);

            string selection = Console.ReadLine();

            while (selection != "1" && selection != "2")
            {
                Console.Clear();
                Console.Write(Constants.MainBanner);
                Console.Write(Constants.Messages["firstPlayerInputError"], name1, name2);
                selection = Console.ReadLine();
            }
            
            return Int32.Parse(selection);
        }

        public void DrawMainScreen(Player player1, Player player2, int numRounds, string[] tokens)
        {
            Console.Clear();
            Console.Write(Constants.MainBanner);
            this.DrawRoundBanner(player1, player2, numRounds);
            this.DrawGameBoard(tokens);
            Console.Write(Constants.Footer);
        }

        public void DrawRoundBanner(Player player1, Player player2, int numRounds)
        {
            var name1 = player1.Name;
            var token1 = player1.Token;
            var numWins1 = player1.NumWins;
            var name2 = player2.Name;
            var token2 = player2.Token;
            var numWins2 = player2.NumWins;

            Console.Write
            (
                Constants.RoundBanner, name1, token1, numWins1, name2, token2, numWins2, numRounds
            );
        }

        public void DrawGameBoard(string[] tokens)
        {
            var adjustedTokens = (string[])tokens.Select
            (
                token => token == "" ? " " : token
            ).ToArray();
            Console.Write(Constants.GameBoard, adjustedTokens);
        }
    }
}
