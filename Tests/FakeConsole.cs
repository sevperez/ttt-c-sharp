using System;
using System.IO;
using System.Collections.Generic;

namespace TTTGame.IntegrationTests
{
    public class FakeConsole : StringWriter, IConsole
    {
        public List<String> ConsoleOutputList { get; set; }
        public StringWriter SW { get; set; }
        public StringReader SR { get; set; }

        public FakeConsole()
        {
            this.ConsoleOutputList = new List<String>();
            this.SW = new StringWriter();
        }

        public override void Write(string value)
        {
            if (value.Length > 0)
            {
                this.ConsoleOutputList.Add(value);
            }
        }
    }
}