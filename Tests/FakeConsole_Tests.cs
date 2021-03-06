using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TTTCore;

namespace IConsoleInterface.Tests
{
    [TestFixture]
    public class Fake_Console_Tests
    {
        [Test]
        public void ReadLineShouldStoreNextLineInReadBuffer()
        {
            var testReadInputs = new List<String>() { "testLine" };
            var subject = new FakeConsole(testReadInputs);
            
            subject.ReadLine();
            var result = subject.CurrentReadItem;

            Assert.That(result, Is.EqualTo(testReadInputs[0]));
        }

        [Test]
        public void IncrementCurrentReadIndexShouldUpdateCurrentReadIndex()
        {
            var testReadInputs = new List<String>() { "testLine" };
            var subject = new FakeConsole(testReadInputs);

            subject.IncrementCurrentReadIndex();
            var result = subject.CurrentReadIndex;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void WriteShouldAddNewItemToConsoleOutputList()
        {
            var testReadInputs = new List<String>() { "" };
            var subject = new FakeConsole(testReadInputs);
            var input = "test item";

            subject.Write(input);
            var result = subject.ConsoleOutputList;

            Assert.That(result, Has.Member(input));
        }
    }
}