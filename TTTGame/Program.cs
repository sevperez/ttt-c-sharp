using System;
using TTTCore;

namespace TTTGame
{
    class Program
    {
        static void Main(string[] args)
        {
      // var game = new Game();
      // game.Play();


            var testConsole = new FakeConsole(new string[] { "" });
            var subject = new CLI(testConsole);

            subject.WelcomeMessage();
        }
    }
}
