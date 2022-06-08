using NUnit.Framework;
using System;
using System.IO;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Game;
using Breakout.Entities;
using Breakout.Entities.Powerups;

namespace BreakoutTests {

    [TestFixture]
    public class PowerupTest {

        private GameEventBus eventBus;
        private Player player;

        private Ball ball;

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

        [OneTimeSetUp]
        public void Setup() {
            eventBus = GameBus.GetBus();
            eventBus.Flush();
            eventBus.ResetBreakProcessing();
            DIKUArcade.Timers.StaticTimer.ResumeTimer();
            PowerupContainer.GetPowerupContainer().Flush();
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

            eventBus.Subscribe(GameEventType.ControlEvent, player);
            eventBus.Subscribe(GameEventType.ControlEvent , ball);
        }


        /// <summary>
        /// Integration
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestPlayerSpeed() {
            double initSpeed = player.MaxSpeed;
            var doubleSpeed = new PlayerSpeed(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            doubleSpeed.PlayerCollision(null);
            eventBus.ProcessEventsSequentially();

            double activatedSpeed = initSpeed * 1.5;
            Assert.True(Math.Abs(activatedSpeed - player.MaxSpeed) < COMPARE_DIFF);

            System.Threading.Thread.Sleep(4000);
            eventBus.ProcessEventsSequentially();

            Assert.True(Math.Abs(initSpeed - player.MaxSpeed) < COMPARE_DIFF, $"{player.MaxSpeed}");
        }


        /// <summary>
        /// Black box
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestWideSize() {
            float initSize = player.Shape.Extent.X;
            var wide = new Wide(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            wide.PlayerCollision(null);
            eventBus.ProcessEventsSequentially();

            float activeSize = initSize * 1.5f;
            Assert.True(Math.Abs(activeSize - player.Shape.Extent.X) < COMPARE_DIFF);

            System.Threading.Thread.Sleep(4000);
            eventBus.ProcessEventsSequentially();

            Assert.True(Math.Abs(initSize - player.Shape.Extent.X) < COMPARE_DIFF);
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

            float expected = life + 1;
            Assert.AreEqual(expected, player.Life);
        }

        /// <summary>
        /// Integration
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestDoubleSpeedBall() {
            var shape = ball.Shape.AsDynamicShape();
            double initSpeed = shape.Direction.Length();
            var doubleSpeed = new DoubleSpeed(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            doubleSpeed.PlayerCollision(null);
            eventBus.ProcessEventsSequentially();

            double activeSpeed = 2 * initSpeed;
            Assert.True(Math.Abs(activeSpeed - shape.Direction.Length()) < COMPARE_DIFF);

            System.Threading.Thread.Sleep(4000);
            eventBus.ProcessEventsSequentially();

            Assert.True(Math.Abs(initSpeed - shape.Direction.Length()) < COMPARE_DIFF);
        }

        /// <summary>
        /// Integration
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestDoubleSize() {
            var initSize = ball.Shape.Extent.X;
            var doubleSpeed = new DoubleSize(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            doubleSpeed.PlayerCollision(null);
            eventBus.ProcessEventsSequentially();

            float activeSize = 2 * initSize;
            Assert.True(Math.Abs(activeSize - ball.Shape.Extent.X) < COMPARE_DIFF);

            System.Threading.Thread.Sleep(4000);
            eventBus.ProcessEventsSequentially();

            Assert.True(Math.Abs(initSize - ball.Shape.Extent.X) < COMPARE_DIFF);
        }

        [Test]
        public void TestHardBall() {
            bool initIsHard = ball.IsHard;
            var hardBall = new HardBall(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            hardBall.PlayerCollision(null);
            eventBus.ProcessEventsSequentially();

            Assert.True(initIsHard != ball.IsHard);

            System.Threading.Thread.Sleep(4000);
            eventBus.ProcessEventsSequentially();

            Assert.True(initIsHard == ball.IsHard);
        }

        [Test]
        public void TestSplit() {
            var split = new Split(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), new NoImage());
            BallContainer ballContainer = new BallContainer();
            eventBus.Subscribe(GameEventType.ControlEvent, ballContainer);
            int initBalls = ballContainer.CountBalls();

            split.PlayerCollision(null);
            eventBus.ProcessEventsSequentially();
            ballContainer.Update();

            int activeBalls = initBalls * 3;
            Assert.AreEqual(activeBalls, ballContainer.CountBalls());
        }

    }

}