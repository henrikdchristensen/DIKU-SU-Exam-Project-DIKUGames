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

        /// <summary>Initialize statemachine and eventbus instances</summary>
        [SetUp]
        public void InitiateStateMachine() {
            Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.GameStateEvent });
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        }

        /// <summary>Integration test: Test that starting state is Main Menu</summary>
        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>(), TestLogger.OnFailedTestMessage(Is.InstanceOf<MainMenu>().ToString(), stateMachine.ActiveState.ToString()));
        }

        /// <summary>
        /// Black- and Whitebox (C0, C1): Test that switching between possible game states
        /// Case:           Expected output:            Comment:
        /// MAIN_MENU       ActiveState=MainMenu        Change state to MainMenu
        /// GAME_RUNNING    ActiveState=GameRunning     Change state to GameRunning
        /// GAME_PAUSED     ActiveState=GamePaused      Change state to GamePaused
        /// </summary>
        [TestCase("MAIN_MENU", GameStateType.MainMenu)]
        [TestCase("GAME_RUNNING", GameStateType.GameRunning)]
        [TestCase("GAME_PAUSED", GameStateType.GamePaused)]
        public void SwitchStateTest(string stringArg1, GameStateType expectedType) {
            eventBus.RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = stringArg1
                }
            );
            eventBus.ProcessEventsSequentially();
            switch (expectedType) {
                case GameStateType.MainMenu:
                    Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>(), TestLogger.OnFailedTestMessage(Is.InstanceOf<MainMenu>().ToString(), stateMachine.ActiveState.ToString()));
                    break;
                case GameStateType.GameRunning:
                    Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>(), TestLogger.OnFailedTestMessage(Is.InstanceOf<GameRunning>().ToString(), stateMachine.ActiveState.ToString()));
                    break;
                case GameStateType.GamePaused:
                    Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>(), TestLogger.OnFailedTestMessage(Is.InstanceOf<GamePaused>().ToString(), stateMachine.ActiveState.ToString()));
                    break;
            }
        }

    }

}