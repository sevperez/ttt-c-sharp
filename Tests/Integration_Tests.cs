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
        public FakeConsole TestConsole { get; set; }

        [SetUp]
        public void Init()
        {
            this.TestConsole = new FakeConsole();
            Console.SetOut(TestConsole);
        }

        [TearDown]
        public void Cleanup()
        {
            var stdout = new StreamWriter(Console.OpenStandardOutput());
            stdout.AutoFlush = true;
            Console.SetOut(stdout);
            this.TestConsole.SW.Dispose();

            if (this.TestConsole.SR != null)
            {
                var stdin = new StreamReader(Console.OpenStandardInput());
                Console.SetIn(stdin);
                this.TestConsole.SR.Dispose();
            }
        }

        [Test]
        [Ignore("ignore integration test")]
        public void PlayIntegrationTest()
        {
            var subject = new Game();
            var operations = "";
            foreach(KeyValuePair<string, string> entry in TestValues.inputs)
            {
                operations += entry.Value;
            }
            this.TestConsole.SR = new StringReader(operations);
            Console.SetIn(TestConsole.SR);

            subject.Play();
            var result = (string[])TestConsole.ConsoleOutputList.ToArray();

            foreach (string output in TestValues.expectedOutputs)
            {
                Assert.That(result, Has.Member(output));
            }
        }
    }
}