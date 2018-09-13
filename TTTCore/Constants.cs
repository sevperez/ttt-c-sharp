using System;
using System.Collections.Generic;

namespace TTTCore
{
    public static class Constants
    {
        public static int MAX = Int32.MaxValue;
        public static int MIN = Int32.MinValue;
        
        public static int MINIMAX_MAX = 1000;

        public static int MINIMAX_MIN = -1000;

        public static int MAX_MINIMAX_DEPTH = 3;

        public static int DefaultBoardSize = 3;

        public static string MainBanner =
            "---------------------------------\n" +
            "Tic-Tac-Toe\n" +
            "---------------------------------\n";
        
        public static string RoundBanner =
            "{0} ({1}): {2}/{6}; {3} ({4}): {5}/{6}\n\n";

        public static Dictionary<string, string> BoardPieces = new Dictionary<string, string>()
        {
            {
                "leftPadding",
                "        "
            },
            {
                "horizontalEdge",
                "-----"
            },
            {
                "leftAndCenterSection",
                "  {0}  |"
            },
            {
                "rightSection",
                "  {0}  \n"
            }
        };
        
        public static int SquareHeight = 3;

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
            },
            {
                "requestBoardSize",
                "Please choose a board size:\n" +
                "1. 3x3 Board\n" + 
                "2. 4x4 Board\n" + 
                "3. 5x5 Board\n"
            },
            {
                "boardSizeInputError",
                "Error: Invalid selection.\n" +
                "Please choose a board size:\n" +
                "1. 3x3 Board\n" + 
                "2. 4x4 Board\n" + 
                "3. 5x5 Board\n"
            },
            {
                "requestPlayerMove",
                "{0}'s Move!\n" +
                "Please choose a square:\n" +
                "{1}\n"
            },
            {
                "roundDrawMessage",
                "Draw!"
            },
            {
                "playerRoundWinMessage",
                "{0} wins the round!!!"
            },
            {
                "playerGameWinMessage",
                "{0} wins the game!!!"
            }
        };
    }
}
