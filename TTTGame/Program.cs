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

            string[] currentTokens = new string[] {
                "X", "", "X", "O", "X", "O", "", "", "O"
            };
            var board = new Board(currentTokens);
            var ai = new AI("X");
            var result = ai.GetPossibleBoardStates(board);
            Console.WriteLine(result);
        }
    }
}
