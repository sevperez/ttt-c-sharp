using System;
using NUnit.Framework;
using TTTCore;

namespace WinningLineGeneratorClass.UnitTests
{
    [TestFixture]
    public class WinningLineGenerator_Tests
    {
        [Test]
        public void GetWinningLinesShouldReturnLinesForDefaultSize()
        {
            var subject = new WinningLineGenerator(3);
            int[,] expected = new int[8, 3] {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 },
                { 0, 3, 6 },
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 0, 4, 8 },
                { 2, 4, 6 }
            };

            var result = subject.GetWinningLines();

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetWinningLinesShouldReturnLinesFor4x4Size()
        {
            var subject = new WinningLineGenerator(4);
            int[,] expected = new int[10, 4] {
                { 0, 1, 2, 3 },
                { 4, 5, 6, 7 },
                { 8, 9, 10, 11 },
                { 12, 13, 14, 15 },
                { 0, 4, 8, 12 },
                { 1, 5, 9, 13 },
                { 2, 6, 10, 14 },
                { 3, 7, 11, 15 },
                { 0, 5, 10, 15},
                { 3, 6, 9, 12 }
            };

            var result = subject.GetWinningLines();

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetWinningLinesShouldReturnLinesFor5x5Size()
        {
            var subject = new WinningLineGenerator(5);
            int[,] expected = new int[12, 5] {
                { 0, 1, 2, 3, 4 },
                { 5, 6, 7, 8, 9},
                { 10, 11, 12, 13, 14 },
                { 15, 16, 17, 18, 19 },
                { 20, 21, 22, 23, 24},
                { 0, 5, 10, 15, 20},
                { 1, 6, 11, 16, 21},
                { 2, 7, 12, 17, 22},
                { 3, 8, 13, 18, 23},
                { 4, 9, 14, 19, 24},
                { 0, 6, 12, 18, 24},
                { 4, 8, 12, 16, 20},
            };

            var result = subject.GetWinningLines();

            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}