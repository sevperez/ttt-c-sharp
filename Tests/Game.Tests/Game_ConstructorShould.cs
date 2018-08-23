using NUnit.Framework;
using GameClass;

namespace GameClass.UnitTests
{
    [TestFixture]
    public class Game_ConstructorShould
    {
        private readonly Game _game;

        public Game_ConstructorShould()
        {
            _game = new Game();
        }

        [Test]
        public void Test1()
        {
            var result = _game.returnsFalse();
            Assert.IsFalse(result, "should be false");
        }
    }
}