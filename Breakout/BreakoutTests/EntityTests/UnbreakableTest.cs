﻿using NUnit.Framework;
using Breakout.Items;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace BreakoutTests.EntityTests {

    [TestFixture]
    public class UnbreakableTest {

        [SetUp]
        public void Setup() {

        }

        [Test]
        public void TestUnbrakable() {
            Block block = new Unbreakable(new StationaryShape(0, 0, 0, 0), new NoImage());

            block.BallCollision(null);

            Assert.True(!block.IsDeleted());
        }

    }
}
