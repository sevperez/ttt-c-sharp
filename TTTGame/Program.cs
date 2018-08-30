using System;
using TTTCore;

namespace TTTGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var human = new Human();
            var computer = new Computer();

            human.SetPlayerName("Fry");
            human.SetPlayerToken("O");
            Console.WriteLine(human.Name);
            Console.WriteLine(human.Token);

            computer.SetPlayerName();
            computer.SetPlayerToken(human.Token);
            Console.WriteLine(computer.Name);
            Console.WriteLine(computer.Token);

            game.SetGameMode("2");
            Console.WriteLine(game.Mode);

            game.SetRoundsToWin(5);
            Console.WriteLine(game.RoundsToWin);

            string[] testTokens = new string[] {
                "O", "", "O",
                "", "X", "O",
                "X", "X", ""
            };
            var board = new Board(testTokens);
            var ai = new AI("X", "O");
            
            var result = ai.GetTopMoveIndex(board, true);

            Console.WriteLine(result);
            Console.WriteLine(Constants.Messages["welcome"]);
        }
    }
}
