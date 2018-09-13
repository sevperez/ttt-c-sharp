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
        public void PlayIntegrationTest3x3()
        {
            var readInputs = new List<String>();
            foreach(KeyValuePair<string, string> entry in TestValues.inputs3x3)
            {
                readInputs.Add(entry.Value);
            };
            var testConsole = new FakeConsole(readInputs);
            var testCLI = new CLI(testConsole);
            var subject = new Game(testCLI);

            subject.Play();

            var result = testConsole.ConsoleOutputList;
            foreach (string output in TestValues.expectedOutputs3x3)
            {
                Assert.That(result, Has.Member(output));
            }
        }

        [Test]
        public void PlayIntegrationTest4x4()
        {
            var readInputs = new List<String>();
            foreach(KeyValuePair<string, string> entry in TestValues.inputs4x4)
            {
                readInputs.Add(entry.Value);
            };
            var testConsole = new FakeConsole(readInputs);
            var testCLI = new CLI(testConsole);
            var subject = new Game(testCLI);

            subject.Play();

            var result = testConsole.ConsoleOutputList;
            foreach (string output in TestValues.expectedOutputs4x4)
            {
                Assert.That(result, Has.Member(output));
            }
        }
    }
}