using System;
using TTTCore;

namespace TTTGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Play();

            // string[] testTokens = new string[] {
            //     "X", "", "X", "O", "X", "O", "", "", "O"
            // };
            // var testBoard = new Board(testTokens);
            // var ownerMovesNext = true;
            // var ai = new AI("X", "O");
            // var idx = ai.GetTopMoveIndex(testBoard, ownerMovesNext);
            // Console.WriteLine(idx);
        }
    }
}
