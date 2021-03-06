using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TTTCore;

namespace IConsoleInterface.Tests
{
    public class FakeConsole : IConsole
    {
        public TextReader TextIn { get; set; }
        public TextWriter TextOut { get; set; }

        public List<String> ConsoleOutputList { get; set; }
        public List<String> ConsoleInputList { get; set; }
        public int CurrentReadIndex { get; set; }
        public string CurrentReadItem { get; set; }

        public FakeConsole(List<String> readInputs = null)
        {
            if (readInputs == null)
            {
                readInputs = new List<String>();
            }

            this.TextIn = new StringReader(String.Join("\n", readInputs));
            this.TextOut = new StringWriter();
            this.ConsoleOutputList = new List<String>();
            this.ConsoleInputList = readInputs;
            this.CurrentReadItem = "";
            this.CurrentReadIndex = 0;
        }

        public void Write(string value)
        {
            if (value.Length > 0)
            {
                this.ConsoleOutputList.Add(value);
            }
        }

        public void Write(string value, object arg0)
        {
            if (value.Length > 0)
            {
                this.ConsoleOutputList.Add(String.Format(value, arg0));
            }
        }

        public void Write(string value, object arg0, object arg1)
        {
            if (value.Length > 0)
            {
                this.ConsoleOutputList.Add(String.Format(value, arg0, arg1));
            }
        }

        public void Write(string value, object[] args)
        {
            if (value.Length > 0)
            {
                this.ConsoleOutputList.Add(String.Format(value, args));
            }
        }

        public string ReadLine()
        {
            this.CurrentReadItem = this.ConsoleInputList[this.CurrentReadIndex];
            this.IncrementCurrentReadIndex();

            return this.CurrentReadItem;
        }

        public void IncrementCurrentReadIndex()
        {
            this.CurrentReadIndex += 1;
        }

        public void Clear()
        {
            return;
        }
    }
}