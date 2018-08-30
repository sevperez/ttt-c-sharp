using NUnit.Framework;
using System;
using System.IO;
using TTTCore;

namespace CLI_Class.UnitTests
{
    [TestFixture]
    public class CLI_Tests
    {
        public StringWriter sw { get; set; }
        public StringReader sr { get; set; }

        [SetUp] public void Init()
        {
            sw = new StringWriter();
            Console.SetOut(sw);
        }

        [TearDown] public void Cleanup()
        {
            var stdout = new StreamWriter(Console.OpenStandardOutput());
            stdout.AutoFlush = true;
            Console.SetOut(stdout);
            sw.Dispose();

            if (sr != null)
            {
                var stdin = new StreamReader(Console.OpenStandardInput());
                Console.SetIn(stdin);
                sr.Dispose();
            }
        }

        [Test]
        public void WelcomeMessageShouldDisplayInConsole()
        {
            var subject = new CLI();

            subject.WelcomeMessage();
            var result = sw.ToString();
            var expected = Constants.Messages["banner"] + Constants.Messages["welcome"];
            
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
