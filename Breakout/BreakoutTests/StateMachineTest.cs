using NUnit.Framework;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using Breakout.Game;
using Breakout.Game.States;

namespace BreakoutTests {

    [TestFixture]
    public class StateMachineTest {

        private StateMachine stateMachine;
        private GameEventBus eventBus;

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void InitiateStateMachine() {
            Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.GameStateEvent });
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        }


        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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