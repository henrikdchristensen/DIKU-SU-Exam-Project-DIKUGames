using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Entities;

namespace BreakoutTests.Entities {

    public class HardenedBlockTests {

        private Block block;
        private HardenedBlock hardenedBlock;

        [SetUp]
        public void Setup() {
            hardenedBlock = new HardenedBlock(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new NoImage(), new NoImage());
            block = new Block(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new NoImage());
        }

        /// <summary>
        /// Blackbox
        /// Precondition: hardenedBlock.StartHealt == block.Health*2
        /// Postcodition: calling Hit() reduces hardenedBlock.Health with 1 life.
        /// </summary>
        [Test]
        public void TestHitBlackbox() {
            // Precondition:
            Assert.AreEqual(block.Health * 2, hardenedBlock.StartHealt, "Precondition");

            block.Hit();

            // Postcondition:
            Assert.AreEqual(hardenedBlock.StartHealt - 1, hardenedBlock.Health, "Postcondition");
        }

        /// <summary>
        /// Whitebox: Test of both branches
        /// </summary>
        [Test]
        public void TestDieWhitebox() {
            for (int i = 0; i < hardenedBlock.StartHealt; i++) {
                if (hardenedBlock.Health < hardenedBlock.StartHealt / 2) {
                    Assert.AreEqual(hardenedBlock.Image, hardenedBlock.BlockStridesAlt, "Branch 1"); // B1
                }
                hardenedBlock.Hit();
            }
            Assert.True(hardenedBlock.IsDeleted(), "Branch 2"); // B2
        }

    }

}