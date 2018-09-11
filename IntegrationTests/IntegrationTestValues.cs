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
                "round1Move1",
                "1\n"
            },
            {
                "round1Move2",
                "2\n"
            },
            {
                "round1Move3",
                "3\n"
            },
            {
                "round1Move4",
                "4\n"
            },
            {
                "round1Move5",
                "5\n"
            },
            {
                "round1Move6",
                "6\n"
            },
            {
                "round1Move7",
                "7\n"
            },
            {
                "round2Move1",
                "9\n"
            },
            {
                "round2Move2",
                "8\n"
            },
            {
                "round2Move3",
                "7\n"
            },
            {
                "round2Move4",
                "6\n"
            },
            {
                "round2Move5",
                "5\n"
            },
            {
                "round2Move6",
                "4\n"
            },
            {
                "round2Move7",
                "3\n"
            },
            {
                "round3Move1",
                "3\n"
            },
            {
                "round3Move2",
                "5\n"
            },
            {
                "round3Move3",
                "1\n"
            },
            {
                "round3Move4",
                "2\n"
            },
            {
                "round3Move5",
                "4\n"
            },
            {
                "round3Move6",
                "6\n"
            },
            {
                "round3Move7",
                "7\n"
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
