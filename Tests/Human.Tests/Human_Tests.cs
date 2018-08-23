using NUnit.Framework;

namespace HumanClass.UnitTests
{
    [TestFixture]
    public class Human_Tests
    {
        private readonly Human _human;

        public Human_Tests()
        {
            _human = new Human();
        }

        [TestCase("Fry")]
        [TestCase("    Fry")]
        [TestCase("  Fry    ")]
        [TestCase("")]
        [TestCase("     ")]
        public void SetPlayerNameShould(string str)
        {
            if (str.Trim().Length == 0)
            {
                Assert.That(() => _human.SetPlayerName(str), Throws.ArgumentException);
            }
            else
            {
                string result = _human.Name;
                Console.WriteLine(result);
                Assert.That(result, Is.EqualTo(str.Trim()));
            }
        }
    }
}