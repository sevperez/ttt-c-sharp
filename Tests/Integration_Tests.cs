using System;
using System.IO;
using NUnit.Framework;
using TTTCore;

namespace GameClass.IntegrationTests
{
    [TestFixture]
    public class Integration_Tests
    {
        public StringWriter sw { get; set; }
        public StringReader sr { get; set; }

        [SetUp]
        public void Init()
        {
            sw = new StringWriter();
            Console.SetOut(sw);
        }

        [TearDown]
        public void Cleanup()
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
        [Ignore("temporarily ignoring integration tests")]
        public void PlayIntegrationTest()
        {
            var subject = new Game();
            var gameModeSelection = "1\n";        // PlayerVsPlayer
            var player1NameSelection = "player1\n";
            var player2NameSelection = "player2\n";
            var player1TokenSelection = "X\n";
            var player2TokenSelection = "O\n";
            var roundSelection = "2\n";
            var firstPlayerSelection = "1\n";     // player1
            string[] round1moves = { "1", "2", "3", "4", "5", "6", "7" }; //player1 wins
            string[] round2moves = { "9", "8", "7", "6", "5", "4", "3" }; //player2 wins
            string[] round3moves = { "3", "5", "1", "2", "4", "6", "7" }; //player2 wins
            var operations = gameModeSelection + player1NameSelection + player2NameSelection
                + player1TokenSelection + player2TokenSelection + roundSelection
                + firstPlayerSelection + string.Join("\n", round1moves) + "\n"
                + string.Join("\n", round2moves) + "\n" + string.Join("\n", round3moves);

            sr = new StringReader(operations);
            Console.SetIn(sr);
            
            subject.Play();
            var result = sw.ToString();

            Assert.That(result, Is.EqualTo(""));
        }
    }
}