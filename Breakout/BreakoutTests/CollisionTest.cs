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
using DIKUArcade.Math;
using System;

namespace BreakoutTests {

    [TestFixture]
    public class CollisionTest {

        private CollisionHandler collision = CollisionHandler.GetInstance();

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void InitiateTest() {
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestCollisionBallAndBlock() {
            DynamicShape shape = new DynamicShape(0.4f, 0.1f, 0.1f, 0.1f);
            Ball ball = new Ball(shape, new NoImage());
            shape.ChangeDirection(new Vec2F(0.0f, 1.0f));
            Block block = new Block(new StationaryShape(0.4f, 0.5f, 0.1f, 0.1f), new NoImage());

            int oldHealth = block.Health;
            Vec2F oldDir = shape.Direction.Copy();
            collision.Subsribe(ball);
            collision.Subsribe(block);
            collision.Update();
            Assert.True(oldHealth > block.Health);
            Assert.True(oldDir.X != shape.Direction.X || oldDir.Y != shape.Direction.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestCollisionBallAndPlayer() {
            DynamicShape shape = new DynamicShape(0.4f, 0.11f, 0.1f, 0.1f);
            Ball ball = new Ball(shape, new NoImage());
            shape.ChangeDirection(new Vec2F(0.0f, -0.3f));
            Player player = new Player(new DynamicShape(0.4f, 0.01f, 0.1f, 0.1f), new NoImage());

            Vec2F oldDir = shape.Direction.Copy();
            collision.Subsribe(ball);
            collision.Subsribe(player);
            collision.Update();
            Assert.True(oldDir.X != shape.Direction.X || oldDir.Y != shape.Direction.Y);

        }

    }

}
