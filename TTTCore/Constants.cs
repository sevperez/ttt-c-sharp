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

        public static Dictionary<string, string> Messages = new Dictionary<string, string>()
        {
            {"welcome", "Welcome to Tic-Tac-Toe!\n"}
        };
    }
}
