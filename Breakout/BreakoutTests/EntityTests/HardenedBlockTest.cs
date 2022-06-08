using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Entities;

namespace BreakoutTests.Items {

    public class HardenedBlockTests {

        private Block block;
        private Block hardenedBlock;

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup() {
            hardenedBlock = new HardenedBlock(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new NoImage(), new NoImage());
            block = new Block(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new NoImage());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestHit() {
            int oldHealth = block.Health;
            block.Hit();
            Assert.True(block.Health < oldHealth);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestHealth() {
            Assert.True(block.Health == hardenedBlock.StartHealt / 2);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestDieBlock() { // Does hit return true when dead (6 times hit)
            int startHealth = block.StartHealt;
            for (int i = 0; i < startHealth; i++)
                block.Hit();
            Assert.True(block.IsDeleted());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestDieHardened() { // Does hit return true when dead (6 times hit)
            int startHealth = hardenedBlock.StartHealt;
            for (int i = 0; i < startHealth; i++)
                hardenedBlock.Hit();
            Assert.True(hardenedBlock.IsDeleted());
        }

    }

}