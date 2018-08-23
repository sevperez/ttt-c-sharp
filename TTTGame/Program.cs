using System;
using GameClass;
using HumanClass;

namespace TTTGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Human hum = new Human();
            hum.SetPlayerName("Fry");
            Console.WriteLine(hum.Name);
            Hello.Greet();
        }
    }
}
