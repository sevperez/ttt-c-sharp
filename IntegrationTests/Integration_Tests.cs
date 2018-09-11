using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using TTTCore;
using IConsoleInterface.Tests;

namespace TTTGame.IntegrationTests
{
    [TestFixture]
    public class Integration_Tests
    {
        [Test]
        public void PlayIntegrationTest()
        {
            var readInputs = new List<String>();
            foreach(KeyValuePair<string, string> entry in TestValues.inputs)
            {
                readInputs.Add(entry.Value);
            };
            var testConsole = new FakeConsole(readInputs);
            var testCLI = new CLI(testConsole);
            var subject = new Game(testCLI);

            subject.Play();

            var result = testConsole.ConsoleOutputList;
            foreach (string output in TestValues.expectedOutputs)
            {
                Assert.That(result, Has.Member(output));
            }
        }
    }
}