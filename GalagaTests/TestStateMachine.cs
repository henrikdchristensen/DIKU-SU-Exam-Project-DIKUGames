using NUnit.Framework;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using Galaga.GalagaStates;

namespace GalagaTests {

    [TestFixture]
    public class TestStateMachine {
        private StateMachine stateMachine;
        private GameEventBus eventBus;

        [SetUp]
        public void InitiateStateMachine() {
            Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.GameStateEvent });
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestEventGamePaused() {
            eventBus.RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                }
            );
            eventBus.ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }

        [Test]
        public void TestEventGameRunning() {
            eventBus.RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING"
                }
            );
            eventBus.ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
        }

        [Test]
        public void TestEventMainMenu() {
            eventBus.RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                }
            );
            eventBus.ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }
    }
}