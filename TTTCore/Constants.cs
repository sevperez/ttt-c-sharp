using System.Collections.Generic;

namespace TTTCore
{
    public static class Constants
    {
        public static int NumSquares = 9;

        public static int[,] WinningLines = new int[8, 3] {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }
        };

        public static string MainBanner =
            "---------------------------------\n" +
            "Tic-Tac-Toe\n" +
            "---------------------------------\n";
        
        public static string RoundBanner =
            "{0} ({1}): {2}/{6}; {3} ({4}): {5}/{6}\n\n";

        public static string GameBoard =
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
            "             |     |     \n";
        
        public static string Footer = "\n---------------------------------\n\n";

        public static Dictionary<string, string> Messages = new Dictionary<string, string>()
        {
            {
                "welcome",
                "Welcome to Tic-Tac-Toe!\n"
            },
            {
                "requestGameMode",
                "Please choose a game mode: \n" +
                "1. Player-vs-Player\n" +
                "2. Player-vs-Computer\n"
            },
            {
                "gameModeInputError",
                "Error: Invalid selection.\n" +
                "Please select a valid game mode: \n" +
                "1. Player-vs-Player\n" +
                "2. Player-vs-Computer\n"
            },
            {
                "requestPlayerName",
                "Please choose a name for Player {0}: \n"
            },
            {
                "playerNameInputError",
                "Error: Invalid name.\n" +
                "Please choose a name for Player {0}: \n"
            },
            {
                "requestPlayerToken",
                "Please choose a token for Player {0}: \n"
            },
            {
                "playerTokenInputError",
                "Error: Invalid token.\n" +
                "Please choose a name for Player {0}: \n"
            },
            {
                "requestRoundsToWin",
                "Please enter number of rounds to win: (Range: 1-9)\n"
            },
            {
                "roundsToWinInputError",
                "Error: Invalid selection.\n" +
                "Please enter number of rounds to win: (Range: 1-9)\n"
            },
            {
                "requestFirstPlayer",
                "Please choose a first player:\n" +
                "1. {0}\n" +
                "2. {1}\n"
            },
            {
                "firstPlayerInputError",
                "Error: Invalid selection.\n" +
                "Please choose a first player:\n" +
                "1. {0}\n" +
                "2. {1}\n"
            }
        };
    }
}
