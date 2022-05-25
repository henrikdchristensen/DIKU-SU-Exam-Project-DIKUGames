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
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void registerPlayerEvent(string message) {
            var e = new GameEvent {
                Message = message,
                EventType = GameEventType.PlayerEvent
            };
            eventBus.RegisterEvent(e);
        }

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void InitializeTest() {
            Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "Player.png")));
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestMoveRight() {
            float prevPos = player.GetPosition().X;
            registerPlayerEvent("RightPressed");
            eventBus.ProcessEventsSequentially();
            for (int i = 0; i < 10; i++) {
                player.Update();
            }
            registerPlayerEvent("RightReleased");
            float expected = 0.684f;
            Assert.True(Math.Abs(player.GetPosition().X - expected) < COMPARE_DIFF, "" + player.GetPosition().X);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestMoveLeft() {
            float prevPos = player.GetPosition().X;
            registerPlayerEvent("LeftPressed");
            eventBus.ProcessEventsSequentially();
            for (int i = 0; i < 10; i++) {
                player.Update();
            }
            registerPlayerEvent("RightReleased");
            float expected = 0.316f;
            Assert.True(Math.Abs(player.GetPosition().X - expected) < COMPARE_DIFF, "" + player.GetPosition().X);
        }

        /// <summary>
        /// 
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

    }

}