﻿using NUnit.Framework;
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
            Ball ball = new Ball(new Vec2F(0, 0), new Vec2F(0, 0), new NoImage());
            var shape = ball.Shape.AsDynamicShape();
            shape.ChangeDirection(new Vec2F(dirX, dirY));

            ball.BlockCollision(new CollisionHandlerData(colDir, new Vec2F(0,0)));
            var newDir = shape.Direction;

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
        [TestCase(0, 0, 10, 10, CollisionDirection.CollisionDirDown, 2.5f, 2.5f)]
        [TestCase(1, 1, 1, 1, CollisionDirection.CollisionDirDown, 1.25f, -0.75f)]
        public void TestBounceDynamic(float dirX, float dirY, float dynX, float dynY, CollisionDirection colDir, float expectedX, float expectedY) {
            Ball ball = new Ball(new Vec2F(0, 0), new Vec2F(0, 0), new NoImage());
            var shape = ball.Shape.AsDynamicShape();
            shape.ChangeDirection(new Vec2F(dirX, dirY));
            var dynShape = new DynamicShape(0, 0, 0, 0, dynX, dynY);
            var player = new Player(dynShape, new NoImage());

            ball.PlayerCollision(new CollisionHandlerData(colDir, dynShape.Direction));
            var newDir = shape.Direction;

            Assert.True(Math.Abs(expectedX - newDir.X) < DIFF, $"dir.X = {newDir.X}, {expectedX}");
            Assert.True(Math.Abs(expectedY - newDir.Y) < DIFF, $"dir.Y = {newDir.Y} {expectedY}");
        }

    }

}