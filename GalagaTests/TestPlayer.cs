using NUnit.Framework;
using Galaga.GalagaStates;
using Galaga;

using DIKUArcade.GUI;
using DIKUArcade.Events;

using System.Security.Principal;
using System.Collections.Generic;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Galaga.MovementStrategy;
using System;




namespace GalagaTests {

    [TestFixture]
    public class PlayerTest {
        //private StateMachine stateMachine;
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
            
            eventBus = GalagaBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
        }

        [Test]
        public void TestMoveRight() {
            float prevPos = player.GetPosition().X;
            registerPlayerEvent("RightPressed");
            for (int i = 0; i < 10; i++) {
                player.Move();
                Console.WriteLine(player.GetPosition().X);
            }
            registerPlayerEvent("RightReleased");
            Assert.True( player.GetPosition().X == prevPos+player.GetMovementSpeed()*10, $"oldPos {player.GetPosition().X} newPos {prevPos+player.GetMovementSpeed()*10}" );
        }

        [Test]
        public void TestMoveLeft() {

        }
    }
}

/*
Here you should:
(1) Initialize a GalagaBus with proper GameEventTypes
(2) Instantiate the StateMachine
(3) Subscribe the GalagaBus to proper GameEventTypes
and GameEventProcessors
*/