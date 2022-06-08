using NUnit.Framework;
using System;
using DIKUArcade.GUI;
using DIKUArcade.Physics;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout;
using Breakout.Items;
using Breakout.Collision;

namespace BreakoutTests {

    [TestFixture]
    public class BallTest {

        private const float DIFF = 10e-5f;

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void InitializeTest() {
            Window.CreateOpenGLContext();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirX"></param>
        /// <param name="dirY"></param>
        /// <param name="colDir"></param>
        /// <param name="expectedX"></param>
        /// <param name="expectedY"></param>
        [Test]
        [TestCase(0, 0, CollisionDirection.CollisionDirDown, 0, 0)]
        [TestCase(1, 1, CollisionDirection.CollisionDirDown, 1, -1)]
        [TestCase(-1, -1, CollisionDirection.CollisionDirUp, -1, 1)]
        [TestCase(1, 0, CollisionDirection.CollisionDirLeft, -1, 0)]
        [TestCase(-1, 0, CollisionDirection.CollisionDirRight, 1, 0)]
        [TestCase(-58.3f, 45.1f, CollisionDirection.CollisionDirRight, 58.3f, 45.1f)]
        public void TestBounceStationary(float dirX, float dirY, CollisionDirection colDir, float expectedX, float expectedY) {
            //Arrange all objects needed: ball and set direction
            Ball ball = new Ball(new Vec2F(0, 0), new Vec2F(0, 0), new NoImage());
            var shape = ball.Shape.AsDynamicShape();
            shape.ChangeDirection(new Vec2F(dirX, dirY));

            //Act
            ball.BlockCollision(new CollisionHandlerData(colDir, new Vec2F(0,0)));
            var newDir = shape.Direction;

            //Assert
            Assert.True(Math.Abs(expectedX - newDir.X) < DIFF, $"dir.X = {newDir.X}, {expectedX}");
            Assert.True(Math.Abs(expectedY - newDir.Y) < DIFF, $"dir.Y = {newDir.Y}, {expectedY}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirX"></param>
        /// <param name="dirY"></param>
        /// <param name="dynX"></param>
        /// <param name="dynY"></param>
        /// <param name="colDir"></param>
        /// <param name="expectedX"></param>
        /// <param name="expectedY"></param>
        [Test]
        [TestCase(1, 1, 10, 10, CollisionDirection.CollisionDirDown, 1.2998674f, 0.557086f)]
        [TestCase(1, 1, 1, 1, CollisionDirection.CollisionDirDown, 1.2126782f, -0.7276069f)]
        public void TestBounceDynamic(float dirX, float dirY, float dynX, float dynY, CollisionDirection colDir, float expectedX, float expectedY) {
            //Arrange all objects needed: ball and set direction
            Ball ball = new Ball(new Vec2F(0, 0), new Vec2F(0, 0), new NoImage());
            var shape = ball.Shape.AsDynamicShape();
            shape.ChangeDirection(new Vec2F(dirX, dirY));

            //Act
            ball.PlayerCollision(new CollisionHandlerData(colDir, new Vec2F(dynX, dynY)));
            var newDir = shape.Direction;

            //Assert
            Assert.True(Math.Abs(expectedX - newDir.X) < DIFF, $"dir.X = {newDir.X}, {expectedX}");
            Assert.True(Math.Abs(expectedY - newDir.Y) < DIFF, $"dir.Y = {newDir.Y} {expectedY}");
        }

        [Test]
        public void TestUnbreakableCollision() {
            Ball ball = new Ball(new Vec2F(0, 0), new Vec2F(0, 0), new NoImage());
            Unbreakable block = new Unbreakable(new StationaryShape(0, 0, 0, 0), new NoImage());
            var shape = ball.Shape.AsDynamicShape();
            shape.ChangeDirection(new Vec2F(1, 1));
            CollisionHandlerData dummy = new CollisionHandlerData(CollisionDirection.CollisionDirDown, new Vec2F(0, 0));
            var expected = new Vec2F(1, -1);

            block.Accept(ball, dummy);
            var newDir = shape.Direction;

            Assert.True(Math.Abs(expected.X - newDir.X) < DIFF, $"dir.X = {newDir.X}, {expected.X}");
            Assert.True(Math.Abs(expected.Y - newDir.Y) < DIFF, $"dir.Y = {newDir.Y} {expected.Y}");
        }

        [Test]
        public void TestWallCollision() {
            Ball ball = new Ball(new Vec2F(0, 0), new Vec2F(0, 0), new NoImage());
            Wall wall = new Wall(new StationaryShape(0, 0, 0, 0));
            var shape = ball.Shape.AsDynamicShape();
            shape.ChangeDirection(new Vec2F(1, 1));
            CollisionHandlerData dummy = new CollisionHandlerData(CollisionDirection.CollisionDirDown, new Vec2F(0, 0));
            var expected = new Vec2F(1, -1);

            wall.Accept(ball, dummy);
            var newDir = shape.Direction;

            Assert.True(Math.Abs(expected.X - newDir.X) < DIFF, $"dir.X = {newDir.X}, {expected.X}");
            Assert.True(Math.Abs(expected.Y - newDir.Y) < DIFF, $"dir.Y = {newDir.Y} {expected.Y}");
        }

    }

}