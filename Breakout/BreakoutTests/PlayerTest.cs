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

namespace BreakoutTests {

    [TestFixture]
    public class PlayerTest {

        private GameEventBus eventBus;
        private Player player;
        private const float COMPARE_DIFF = 10e-6f;

        /// <summary>
        /// Registering an event in the eventbus
        /// </summary>
        /// <param name="message">message for the eventbus</param>
        private void registerPlayerEvent(string message) {
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
                new NoImage());
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
        }


        /// <summary>
        /// Black box 
        /// Testing move left and right via eventbus and player.Update() method
        /// </summary>
        [Test]
        public void TestMoveBoth() {
            float prevPos = player.GetPosition().X;
            registerPlayerEvent("LeftPressed");
            registerPlayerEvent("RightPressed");
            eventBus.ProcessEventsSequentially();
            for (int i = 0; i < 10; i++) {
                player.Update();
            }
            Assert.True(player.GetPosition().X == prevPos);
        }


        /// <summary>
        /// Black box
        /// Testing that max speed is reached after acceleration
        /// </summary>
        [Test]
        public void TestMaxSpeed() {
            registerPlayerEvent("RightPressed");
            eventBus.ProcessEventsSequentially();
            int iterations = 4;
            for (int i = 0; i < iterations; i++) {
                player.Update();
            }
            float expectedSpeed = 0.015f;
            Assert.True( Math.Abs(player.Shape.AsDynamicShape().Direction.X) == expectedSpeed , TestLogger.OnFailedTestMessage<float>(Math.Abs(player.Shape.AsDynamicShape().Direction.X), expectedSpeed) );
        }


        /// <summary>
        /// Black box
        /// Testing if the deaccelating and stopping correctly 
        /// </summary>
        [Test]
        public void TestDeaccelerateAndStop() {
            float prevPos = player.GetPosition().X;
            int iterations = 4;

            registerPlayerEvent("RightPressed");
            eventBus.ProcessEventsSequentially();

            for (int i = 0; i < iterations; i++) {
                player.Update();
            }

            registerPlayerEvent("RightReleased");
            eventBus.ProcessEventsSequentially();

            for (int i = 0; i < iterations+5; i++) {
                player.Update();
                Console.WriteLine($"Got curr pos in iteration {i} pos: " + player.GetPosition().X);

            }
            float expectedPos = prevPos + 0.06f;//  (0.005+0.010+0.015 )*2;

            Assert.True(expectedPos - player.GetPosition().X < COMPARE_DIFF, TestLogger.OnFailedTestMessage<float>(expectedPos, player.GetPosition().X));
        }


        /// <summary>
        /// Black box
        /// Testing acceleration
        /// </summary>
        [Test]
        public void TestAcceleration() {
            float prevPos = player.GetPosition().X;
            registerPlayerEvent("RightPressed");
            eventBus.ProcessEventsSequentially();
            for (int i = 0; i < 4; i++) {
                player.Update();
                Console.WriteLine($"Got curr pos in iteration {i} pos: " + player.GetPosition().X);
            }
            float expectedPos = prevPos + 0.045f; // (0.005+0.010+0.015 + (0.005)*3
            Assert.True(expectedPos - player.GetPosition().X < COMPARE_DIFF, TestLogger.OnFailedTestMessage<float>(expectedPos, player.GetPosition().X));
        
        }



        /// <summary>
        /// White box
        /// Testing move via eventbus and player.Update method
        /// </summary>
        [TestCase(10, "RightPressed", 0.635f, 0)] // right move
        [TestCase(10, "LeftPressed", 0.365f, 1)] // left move
        [TestCase(50, "RightPressed", 1.0f - 0.05f)] // right boundary test
        [TestCase(50, "LeftPressed", 0.0f+0.05f)] // left boundary test
        [TestCase(4, "RightPressed", 0.5f+0.045f)] // testing acceleration

        public void TestMove(int iterations, string action, float expected) {

            registerPlayerEvent(action);
            eventBus.ProcessEventsSequentially();
            for (int i = 0; i < iterations; i++) {
                player.Update();
            }
            // float expected = 0.635f; // expected position on x-axis
            Assert.True(Math.Abs(player.GetPosition().X - expected) < COMPARE_DIFF, TestLogger.OnFailedTestMessage<float>(expected, player.GetPosition().X));
        }

    }

}