using System;
using NUnit.Framework;
using Breakout.Items;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;


namespace BreakoutTests.Items {

    public class HardenedBlockTests {
        private Block block;
        private Block hardenedBlock;


        [SetUp]
        public void Setup() {
            hardenedBlock = new HardenedBlock(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new NoImage(), new NoImage());
            block = new Block(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new NoImage());
        }

        [Test]
        public void TestHit() {
            int oldHealth = block.Health;
            block.Hit();
            Assert.True(block.Health < oldHealth);
        }

        public void TestHealth() {
            int blockdHealth = block.Health;
            int hardenedBlockHealth = hardenedBlock.Health;
           // block.Hit();
            Assert.True(block.Health == hardenedBlockHealth/2);
        }

        [Test]
        public void TestDie() { // Does hit return true when dead (6 times hit)
            block.Hit();
            block.Hit();
            block.Hit();
            block.Hit();
            block.Hit();
            Assert.True(block.Hit());
        }
    }

}