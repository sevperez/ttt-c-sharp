using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TTTCore
{
    public class GameConsole : IConsole
    {
        public TextReader TextIn { get; set; }
        public TextWriter TextOut { get; set; }

        public GameConsole()
        {
            this.TextIn = Console.In;
            this.TextOut = Console.Out;
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void Write(string value, object arg0)
        {
            Console.Write(value, arg0);
        }

        public void Write(string value, object arg0, object arg1)
        {
            Console.Write(value, arg0, arg1);
        }

        public void Write(string value, object[] args)
        {
            Console.Write(value, args);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}