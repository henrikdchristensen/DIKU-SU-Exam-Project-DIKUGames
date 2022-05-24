using Breakout.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using DIKUArcade.GUI;
using System.IO;
using Breakout.Input;
using System;
using Breakout.Items;
using DIKUArcade.Physics;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace BreakoutTests {

    [TestFixture]
    public class BallTest {

        private const float DIFF = 10e-5f;

        [SetUp]
        public void InitializeTest() {
            Window.CreateOpenGLContext();
            
        }

        [Test]
        [TestCase(0, 0, CollisionDirection.CollisionDirDown, 0, 0)]
        [TestCase(1, 1, CollisionDirection.CollisionDirDown, 1, -1)]
        [TestCase(-1, -1, CollisionDirection.CollisionDirUp, -1, 1)]
        [TestCase(1, 0, CollisionDirection.CollisionDirLeft, -1, 0)]
        [TestCase(-1, 0, CollisionDirection.CollisionDirRight, 1, 0)]
        [TestCase(-58.3f, 45.1f, CollisionDirection.CollisionDirRight, 58.3f, 45.1f)]
        public void TestBounceStationary(float dirX, float dirY, CollisionDirection colDir, float expectedX, float expectedY) {
            Ball ball = new Ball(new DynamicShape(0,0,0,0), new NoImage());
            ball.GetShape().ChangeDirection(new Vec2F(dirX, dirY));
            //var dummyShape = new Shape().AsDynamicShape();

            //ball.Accept(dummyShape, new CollisionData() {CollisionDir = colDir});
            var newDir = ball.GetShape().Direction;

            Assert.True(Math.Abs(expectedX - newDir.X) < DIFF, $"dir.X = {newDir.X}");
            Assert.True(Math.Abs(expectedY - newDir.Y) < DIFF, $"dir.Y = {newDir.Y}");
        }

        [Test]
        [TestCase(0, 0, 10, 10, CollisionDirection.CollisionDirDown, 5, 0)]
        [TestCase(1, 1, 1, 1, CollisionDirection.CollisionDirDown, 1.5f, -1)]
        public void TestBounceDynamic(float dirX, float dirY, float dynX, float dynY, CollisionDirection colDir, float expectedX, float expectedY) {
            Ball ball = new Ball(new DynamicShape(0, 0, 0, 0), new NoImage());
            ball.GetShape().ChangeDirection(new Vec2F(dirX, dirY));
            var dynShape = new DynamicShape(0, 0, 0, 0, dynX, dynY);

            //ball.Accept(dynShape, new CollisionData() { CollisionDir = colDir });
            var newDir = ball.GetShape().Direction;

            Assert.True(Math.Abs(expectedX - newDir.X) < DIFF, $"dir.X = {newDir.X}");
            Assert.True(Math.Abs(expectedY - newDir.Y) < DIFF, $"dir.Y = {newDir.Y}");
        }

    }

}