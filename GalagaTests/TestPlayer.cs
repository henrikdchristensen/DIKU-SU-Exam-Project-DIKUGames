using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;

namespace GalagaTests {

    [TestFixture]
    public class TestPlayer {
        private GameEventBus eventBus;
        private Player player;

        private void registerPlayerEvent(string message) {
            var e = new GameEvent {
                Message = message,
                EventType = GameEventType.PlayerEvent
            };
            eventBus.RegisterEvent(e);
        }

        [SetUp]
        public void InitializeTest() {
            Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..", "Galaga", "Assets", "Images", "Player.png")));
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            eventBus.RegisterEvent(new GameEvent {
                Message = "HEJ",
                EventType = GameEventType.PlayerEvent
            });
        }

        [Test]
        public void TestMoveRight() {
            float prevPos = player.GetPosition().X;
            registerPlayerEvent("RightPressed");
            eventBus.ProcessEventsSequentially();
            for (int i = 0; i < 10; i++) {
                player.Move();
                Console.WriteLine(player.GetPosition().X);
            }
            registerPlayerEvent("RightReleased");
            Assert.True(player.GetPosition().X == prevPos + player.GetMovementSpeed() * 10, $"oldPos {player.GetPosition().X} newPos {prevPos + player.GetMovementSpeed() * 10}");
        }

        [Test]
        public void TestMoveLeft() { // TODO

        }

        [Test]
        public void TestMoveBoth() { // TODO

        }
    }
}