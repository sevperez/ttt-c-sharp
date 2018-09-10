using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using TTTCore;

namespace TTTGame.IntegrationTests
{
    [TestFixture]
    public class Integration_Tests
    {
        [Test]
        public void PlayIntegrationTest()
        {
            var operations = new List<String>();
            foreach(KeyValuePair<string, string> entry in TestValues.inputs)
            {
                operations.Add(entry.Value);
            }
            var readInputs = (string[])operations.ToArray();
            var testConsole = new FakeConsole(readInputs);
            var testCLI = new CLI(testConsole);
            var subject = new Game(testCLI);

            subject.Play();
            var result = (string[])testConsole.ConsoleOutputList.ToArray();

            foreach (string output in TestValues.expectedOutputs)
            {
                Assert.That(result, Has.Member(output));
            }
        }
    }
}