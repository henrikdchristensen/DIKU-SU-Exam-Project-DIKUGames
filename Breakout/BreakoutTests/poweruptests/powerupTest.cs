using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Items.Powerups;
using Breakout.Items;


namespace BreakoutTests {

    [TestFixture]
    public class PowerupTest {

        private GameEventBus eventBus;
        private Player player;

        private Ball ball;


        private PowerupContainer powerupContainer = PowerupContainer.GetPowerupContainer();

        private const float COMPARE_DIFF = 10e-6f;

        /// <summary>
        /// Registering an event in the eventbus
        /// </summary>
        /// <param name="message">message for the eventbus</param>
        private void registerEvent(string message) {
            var e = new GameEvent {
                Message = message,
                EventType = GameEventType.PlayerEvent
            };
            eventBus.RegisterEvent(e);
        }

        /// <summary>
        /// Arranging tests
        /// </summary>
        [SetUp]
        public void InitializeTest() {
            Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "Player.png")));
            ball = new Ball(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f), new NoImage());
            
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            eventBus.Subscribe(GameEventType.ControlEvent , ball);

        }


        /// <summary>
        /// Integration
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestDoubleSpeedPlayer() {
            double speed = player.MaxSpeed;
            var doubleSpeed = new DoubleSpeed(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            doubleSpeed.PlayerCollision(null);
            eventBus.ProcessEventsSequentially();

            double expected = speed * 2;
            Assert.AreEqual(expected, player.MaxSpeed);
        }


        /// <summary>
        /// Black box
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestWideSize() {
            float size = player.Shape.Extent.X;
            var wide = new Wide(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            wide.PlayerCollision(null);
            eventBus.ProcessEventsSequentially();
            float expected = size * 2;
            Assert.AreEqual(expected, player.Shape.Extent.X);
        }


        /// <summary>
        /// Black box
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestExtraLife() {
            int life = player.Life;
            var extraLife = new ExtraLife(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            extraLife.PlayerCollision(null);
            eventBus.ProcessEventsSequentially();
            float expected = life+1;
            Assert.AreEqual(expected, player.Life);
        }




       /// <summary>
        /// Integration
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestDoubleSpeedBall() {
            float speed = ball.Shape.AsDynamicShape().Direction.X;
            var doubleSpeed = new DoubleSpeed(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            doubleSpeed.BallCollision(null);
            eventBus.ProcessEventsSequentially();

            float expected = 2*speed;
            Assert.AreEqual(expected, ball.Shape.AsDynamicShape().Direction.X);
        }


       /// <summary>
        /// Integration
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestDoubleSize() {
            var size = ball.Shape.Extent.X;
            var doubleSpeed = new DoubleSpeed(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            doubleSpeed.BallCollision(null);
            eventBus.ProcessEventsSequentially();

            float expected = 2 * size;
            Assert.AreEqual(expected, player.MaxSpeed);
        }

    }

}