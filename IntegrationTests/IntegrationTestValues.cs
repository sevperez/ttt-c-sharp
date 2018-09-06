using System.Collections.Generic;

namespace TTTGame.IntegrationTests
{
    public class TestValues
    {
        public static Dictionary<string, string> inputs = new Dictionary<string, string>()
        {
            {
                "gameModeSelection",
                "1\n"
            },
            {
                "player1Name",
                "player1\n"
            },
            {
                "player2Name",
                "player2\n"
            },
            {
                "player1Token",
                "X\n"
            },
            {
                "player2Token",
                "O\n"
            },
            {
                "numRoundsSelection",
                "2\n"
            },
            {
                "firstPlayerSelection",
                "1\n"
            },
            {
                "round1Moves",
                "1\n2\n3\n4\n5\n6\n7\n"
            },
            {
                "round2Moves",
                "9\n8\n7\n6\n5\n4\n3\n"
            },
            {
                "round3Moves",
                "3\n5\n1\n2\n4\n6\n7\n"
            }
        };

        public static string[] expectedOutputs = new string[]
        {
            "player1 (X): 0/2; player2 (O): 0/2\n\n",
            "player1's Move!\nPlease choose a square:\n7, 8, 9\n",
            "             |     |     \n" +
            "          X  |  O  |  X  \n" +
            "             |     |     \n" +
            "        -----------------\n" +
            "             |     |     \n" +
            "          O  |  X  |  O  \n" +
            "             |     |     \n" +
            "        -----------------\n" +
            "             |     |     \n" +
            "          X  |     |     \n" +
            "             |     |     \n\n",
            "player1 wins the round!!!",
            "player1 (X): 1/2; player2 (O): 0/2\n\n",
            "player2's Move!\nPlease choose a square:\n1, 2, 3\n",
            "             |     |     \n" +
            "             |     |  O  \n" +
            "             |     |     \n" +
            "        -----------------\n" +
            "             |     |     \n" +
            "          X  |  O  |  X  \n" +
            "             |     |     \n" +
            "        -----------------\n" +
            "             |     |     \n" +
            "          O  |  X  |  O  \n" +
            "             |     |     \n\n",
            "player2 wins the round!!!",
            "player1 (X): 1/2; player2 (O): 1/2\n\n",
            "             |     |     \n" +
            "          X  |  O  |  X  \n" +
            "             |     |     \n" +
            "        -----------------\n" +
            "             |     |     \n" +
            "          X  |  O  |  O  \n" +
            "             |     |     \n" +
            "        -----------------\n" +
            "             |     |     \n" +
            "          X  |     |     \n" +
            "             |     |     \n\n",
            "player1 (X): 2/2; player2 (O): 1/2\n\n",
            "player1 wins the game!!!"
        };
    }
}


