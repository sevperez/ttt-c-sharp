using System.Collections.Generic;

namespace TTTGame.IntegrationTests
{
    public class TestValues
    {
        public static Dictionary<string, string> inputs3x3 = new Dictionary<string, string>()
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
                "boardSizeSelection",
                "1\n"
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

        public static string[] expectedOutputs3x3 = new string[]
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

        public static Dictionary<string, string> inputs4x4 = new Dictionary<string, string>()
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
                "boardSizeSelection",
                "2\n"
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
                "round1Move8",
                "8\n"
            },
            {
                "round1Move9",
                "9\n"
            },
            {
                "round1Move10",
                "10\n"
            },
            {
                "round1Move11",
                "11\n"
            },
            {
                "round1Move112",
                "12\n"
            },
            {
                "round1Move13",
                "13\n"
            },
            {
                "round2Move1",
                "16\n"
            },
            {
                "round2Move2",
                "15\n"
            },
            {
                "round2Move3",
                "14\n"
            },
            {
                "round2Move4",
                "13\n"
            },
            {
                "round2Move5",
                "12\n"
            },
            {
                "round2Move6",
                "11\n"
            },
            {
                "round2Move7",
                "10\n"
            },
            {
                "round2Move8",
                "9\n"
            },
            {
                "round2Move9",
                "8\n"
            },
            {
                "round2Move10",
                "7\n"
            },
            {
                "round2Move11",
                "6\n"
            },
            {
                "round2Move12",
                "5\n"
            },
            {
                "round2Move13",
                "4\n"
            },
            {
                "round3Move1",
                "1\n"
            },
            {
                "round3Move2",
                "16\n"
            },
            {
                "round3Move3",
                "2\n"
            },
            {
                "round3Move4",
                "15\n"
            },
            {
                "round3Move5",
                "3\n"
            },
            {
                "round3Move6",
                "14\n"
            },
            {
                "round3Move7",
                "4\n"
            }
        };

        public static string[] expectedOutputs4x4 = new string[]
        {
            "player1 (X): 0/2; player2 (O): 0/2\n\n",
            "player1's Move!\nPlease choose a square:\n13, 14, 15, 16\n",
            "             |     |     |     \n" +
            "          X  |  O  |  X  |  O  \n" +
            "             |     |     |     \n" +
            "        -----------------------\n" +
            "             |     |     |     \n" +
            "          X  |  O  |  X  |  O  \n" +
            "             |     |     |     \n" +
            "        -----------------------\n" +
            "             |     |     |     \n" +
            "          X  |  O  |  X  |  O  \n" +
            "             |     |     |     \n" +
            "        -----------------------\n" +
            "             |     |     |     \n" +
            "          X  |     |     |     \n" +
            "             |     |     |     \n\n",
            "player1 wins the round!!!",
            "player1 (X): 1/2; player2 (O): 0/2\n\n",
            "player2's Move!\nPlease choose a square:\n1, 2, 3, 4\n",
            "             |     |     |     \n" +
            "             |     |     |  O  \n" +
            "             |     |     |     \n" +
            "        -----------------------\n" +
            "             |     |     |     \n" +
            "          X  |  O  |  X  |  O  \n" +
            "             |     |     |     \n" +
            "        -----------------------\n" +
            "             |     |     |     \n" +
            "          X  |  O  |  X  |  O  \n" +
            "             |     |     |     \n" +
            "        -----------------------\n" +
            "             |     |     |     \n" +
            "          X  |  O  |  X  |  O  \n" +
            "             |     |     |     \n\n",
            "player2 wins the round!!!",
            "player1 (X): 1/2; player2 (O): 1/2\n\n",
            "player1's Move!\nPlease choose a square:\n4, 5, 6, 7, 8, 9, 10, 11, 12, 13\n",
            "             |     |     |     \n" +
            "          X  |  X  |  X  |  X  \n" +
            "             |     |     |     \n" +
            "        -----------------------\n" +
            "             |     |     |     \n" +
            "             |     |     |     \n" +
            "             |     |     |     \n" +
            "        -----------------------\n" +
            "             |     |     |     \n" +
            "             |     |     |     \n" +
            "             |     |     |     \n" +
            "        -----------------------\n" +
            "             |     |     |     \n" +
            "             |  O  |  O  |  O  \n" +
            "             |     |     |     \n\n",
            "player1 (X): 2/2; player2 (O): 1/2\n\n",
            "player1 wins the game!!!"
        };
    }
}
