using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Entities;

namespace BreakoutTests.Entities {

    public class Tests {

        private Block block;

        [SetUp]
        public void Setup() {
            block = new Block(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new NoImage());
        }

        [Test]
        public void TestHit() {
            int oldHealth = block.Health;
            block.Hit();
            Assert.True(block.Health < oldHealth);
        }

        [Test]
        public void TestDie() { // Does hit return true when dead (6 times hit)
            block.Hit();
            block.Hit();
            block.Hit();
            block.Hit();
            block.Hit();
            Assert.True(block.IsDeleted());
        }

    }

}