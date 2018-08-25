using System;
using TTTCore;

namespace TTTGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Human hum = new Human();
            Computer comp = new Computer();

            hum.SetPlayerName("Fry");
            hum.SetPlayerToken("O");
            Console.WriteLine(hum.Name);
            Console.WriteLine(hum.Token);

            comp.SetPlayerName();
            comp.SetPlayerToken(hum.Token);
            Console.WriteLine(comp.Name);
            Console.WriteLine(comp.Token);
        }
    }
}
