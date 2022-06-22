using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Entities;

namespace BreakoutTests.Entities {

    public class BlockTests {

        private Block block;

        [SetUp]
        public void Setup() {
            block = new Block(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new NoImage());
        }

        /// <summary>
        /// Blackbox
        /// Precondition: StartHealt == Health
        /// Postcodition: calling Hit() 1 time and block will be marked for deletion.
        /// </summary>
        [Test]
        public void TestHitBlackbox() {
            // Precondition:
            Assert.AreEqual(block.StartHealt, block.Health, "Precondition");

            block.Hit();

            // Postcondition:
            Assert.True(block.IsDeleted(), "Postcondition");
        }

        /// <summary>
        /// Trivial method call (only for coverage)
        /// </summary>
        [Test]
        public void TestOnDeletion() {
            block.OnDeletion();
        }

    }

}