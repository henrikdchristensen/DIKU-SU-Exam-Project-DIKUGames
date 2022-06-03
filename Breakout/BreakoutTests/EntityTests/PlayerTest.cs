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
using Breakout.Items;
using DIKUArcade.Graphics;
using Breakout.Game;
using Breakout.Game.States;

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
            Assert.True(Math.Abs(player.Shape.AsDynamicShape().Direction.X) == expectedSpeed, TestLogger.OnFailedTestMessage<float>(Math.Abs(player.Shape.AsDynamicShape().Direction.X), expectedSpeed));
        }


        // *********** White box tests of the UpdateMovement() method 100% C0 and C1 (Branch) **************

        /// <summary>
        /// White box
        /// Testing move via eventbus and player.UpdateMovement() method
        /// </summary>
        [TestCase(10, "RightPressed", 0.635f)] // right move. Branch: 1.b 3.a, 3b,
        [TestCase(10, "LeftPressed", 0.365f)] // left move. Branch: 1.b 3.a, 3b,

        [TestCase(50, "RightPressed", 1.0f - 0.05f)] // right boundary test. Branch: 1.a 1.b 3.a, 3.b, 4.a
        [TestCase(50, "LeftPressed", 0.0f + 0.05f)] // left boundary test. Branch: 1.a 1.b 3.a, 3.b, 4.b
        [TestCase(4, "RightPressed", 0.5f + 0.045f)] // 1.b acceleration. 3.a

        public void TestMove(int iterations, string action, float expected) {

            registerPlayerEvent(action);
            eventBus.ProcessEventsSequentially();
            for (int i = 0; i < iterations; i++) {
                player.Update();
            }
            // float expected = 0.635f; // expected position on x-axis
            Assert.True(Math.Abs(player.GetPosition().X - expected) < COMPARE_DIFF, TestLogger.OnFailedTestMessage<float>(expected, player.GetPosition().X));
        }


        /// <summary>
        /// Part of the white box
        /// Testing move left and right via eventbus and player.Update() method
        /// Branches: 1.a
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
        /// Part of the whitebox test
        /// Testing acceleration to max speed and stop covers 2.a and 2.b which is not covered by the other tests
        /// Branches: 1, 2 3. and specifically 2.a and 2.b
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

            for (int i = 0; i < iterations + 5; i++) {
                player.Update();
                Console.WriteLine($"Got curr pos in iteration {i} pos: " + player.GetPosition().X);

            }
            float expectedPos = prevPos + 0.06f;//  (0.005+0.010+0.015 )*2;
            Assert.True(expectedPos - player.GetPosition().X < COMPARE_DIFF, TestLogger.OnFailedTestMessage<float>(expectedPos, player.GetPosition().X));
        }

        /// <summary>
        /// Integration test of gameover
        /// </summary>
        [Test]
        public void TestGameOver() {
            StateMachine stateMachine = new StateMachine();
            GameBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);

            GameBus.TriggerEvent(
                GameEventType.GameStateEvent, "CHANGE_STATE",
                StateTransformer.StateToString(GameStateType.GameRunning));
            GameBus.GetBus().ProcessEventsSequentially();

            var initState = stateMachine.ActiveState;
            Assert.True(initState is GameRunning);

            for (int i = 0; i < 3; i++) {
                GameBus.TriggerEvent(
                    GameEventType.PlayerEvent, Player.LOOSE_LIFE_MSG);
            }

            GameBus.GetBus().ProcessEventsSequentially();

            Assert.True(stateMachine.ActiveState is not GameRunning);
        }
    }
}