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

        [Test]
        public void GetGameModeSelectionShouldReturnStringInRange1To2()
        {
            var subject = new CLI();
            var expected = "1";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            string result = subject.GetGameModeSelection();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetRoundsToWinShouldReturnIntInRange1To9()
        {
            var subject = new CLI();
            var expected = "5";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            int result = subject.GetRoundsToWinSelection();

            Assert.That(result, Is.EqualTo(Int32.Parse(expected)));
        }

        [Test]
        public void GetPlayerNameSelectionShouldReturnString()
        {
            var subject = new CLI();
            var testPlayerNumber = 1;
            var expected = "Fry";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            string result = subject.GetPlayerNameSelection(testPlayerNumber);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetPlayerTokenSelectionShouldReturnString()
        {
            var subject = new CLI();
            var testPlayerNumber = 1;
            var expected = "X";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            string result = subject.GetPlayerTokenSelection(testPlayerNumber);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetFirstPlayerSelectionShouldReturnIntInRange1To2()
        {
            var subject = new CLI();
            var player1 = new Player();
            var player2 = new Player();
            var expected = "2";

            sr = new StringReader(expected);
            Console.SetIn(sr);

            int result = subject.GetFirstPlayerSelection(player1, player2);

            Assert.That(result, Is.EqualTo(Int32.Parse(expected)));
        }
    }
}
