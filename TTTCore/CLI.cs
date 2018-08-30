using System;
using System.Threading;

namespace TTTCore
{
    public class CLI
    {
        public void WelcomeMessage()
        {
            Console.Write(Constants.Messages["banner"]);
            Console.Write(Constants.Messages["welcome"]);
        }

        public string GetGameModeSelection()
        {
            Console.Write(Constants.Messages["banner"]);
            Console.Write(Constants.Messages["requestGameMode"]);
            string selection = Console.ReadKey(true).KeyChar.ToString();

            while (selection != "1" && selection != "2")
            {
                Console.Clear();
                Console.Write(Constants.Messages["banner"]);
                Console.Write(Constants.Messages["gameModeInputError"]);
                selection = Console.ReadKey(true).KeyChar.ToString();
            }
            
            return selection;
        }

        public int GetRoundsToWinSelection()
        {
            Console.Write(Constants.Messages["banner"]);
            Console.Write(Constants.Messages["requestRoundsToWin"]);

            string input = Console.ReadKey(true).KeyChar.ToString();
            int selection;
            bool successfulParse = Int32.TryParse(input, out selection);

            while (!successfulParse || selection < 1 || selection > 9)
            {
                Console.Clear();
                Console.Write(Constants.Messages["banner"]);
                Console.Write(Constants.Messages["roundsToWinInputError"]);
                input = Console.ReadKey(true).KeyChar.ToString();
                successfulParse = Int32.TryParse(input, out selection);
            }
            
            return selection;
        }

        public string GetPlayerNameSelection(int playerNumber, string invalidName = "")
        {
            Console.Write(Constants.Messages["banner"]);
            Console.Write(Constants.Messages["requestPlayerName"], playerNumber);
            string selection = Console.ReadLine();

            while (selection == null || selection == "" || selection == invalidName)
            {
                Console.Clear();
                Console.Write(Constants.Messages["banner"]);
                Console.Write(Constants.Messages["playerNameInputError"], playerNumber);
                selection = Console.ReadLine();
            }
            
            return selection;
        }

        public string GetPlayerTokenSelection(int playerNumber, string invalidToken = "")
        {
            Console.Write(Constants.Messages["banner"]);
            Console.Write(Constants.Messages["requestPlayerToken"], playerNumber);
            string selection = Console.ReadLine();

            while (selection == null || selection.Length != 1 || selection == invalidToken)
            {
                Console.Clear();
                Console.Write(Constants.Messages["banner"]);
                Console.Write(Constants.Messages["playerTokenInputError"], playerNumber);
                selection = Console.ReadLine();
            }
            
            return selection;
        }

        public int GetFirstPlayerSelection(Player player1, Player player2)
        {
            var name1 = player1.Name;
            var name2 = player2.Name;

            Console.Write(Constants.Messages["banner"]);
            Console.Write(Constants.Messages["requestFirstPlayer"], name1, name2);

            string selection = Console.ReadKey(true).KeyChar.ToString();

            while (selection != "1" && selection != "2")
            {
                Console.Clear();
                Console.Write(Constants.Messages["banner"]);
                Console.Write(Constants.Messages["firstPlayerInputError"], name1, name2);
                selection = Console.ReadKey(true).KeyChar.ToString();
            }
            
            return Int32.Parse(selection);
        }
    }
}
