using NUnit.Framework;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using Breakout.Collision;
using Breakout.Items;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;
using Breakout;


namespace BreakoutTests {

    [TestFixture]
    public class CollisionTest {

        private CollisionHandler collision = CollisionHandler.GetInstance();
       

        [SetUp]
        public void InitiateTest() {
        }

        [Test]
        public void TestCollisionBallAndBlock() {
            Ball ball = new Ball(new DynamicShape(0.4f, 0.5f, 0.1f, 0.1f, 0.0f, 1.0f), new NoImage());
            Block block = new Block(new StationaryShape(0.4f, 0.5f, 0.1f, 0.1f), new NoImage());
            int blockOldHealth = block.Health;
            collision.Subsribe(ball);
            collision.Subsribe(block);
            collision.Update();
            Assert.True(blockOldHealth > block.Health);
            Assert.True(true);
        }

        [Test]
        public void TestCollisionBallAndPlayer() {
            Ball ball = new Ball(new DynamicShape(0.4f, 0.5f, 0.1f, 0.1f, 0.0f, -1.0f), new NoImage());
            Player player = new Player(new DynamicShape(0.4f, 0.01f, 0.1f, 0.1f), new NoImage());

        }

    }
}
