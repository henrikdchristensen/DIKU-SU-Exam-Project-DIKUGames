using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.Game {

    public class StateMachine : IGameEventProcessor {

        public IGameState ActiveState { get; private set; }

        /// <summary>
        /// Constructor for StateMachine: Instantiate the state instances and subscribes for GameStateEvents
        /// </summary>
        public StateMachine() {
            GameBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);

            // Instantiate state-objects to avoid lagging at game transition
            ActiveState = States.MainMenu.GetInstance();
            States.GameRunning.GetInstance();
            States.GamePaused.GetInstance();
        }

        /// <summary>Switch to a new active state</summary>
        /// <param name="stateType">The state which should be shifted to</param>
        private void switchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.MainMenu:
                    ActiveState = States.MainMenu.GetInstance();
                    break;
                case GameStateType.GameRunning:
                    ActiveState = States.GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = States.GamePaused.GetInstance();
                    break;
            }
        }

        /// <summary>Process events: Change state and Reset state</summary>
        /// <param name="gameEvent">The GameEvent which should be proccesed</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                GameStateType state = StateTransformer.StringToState(gameEvent.StringArg1);
                switch (gameEvent.Message) {
                    case "CHANGE_STATE":
                        switchState(state);
                        break;
                    case "CHANGE_STATE_RESET":
                        switchState(state);
                        ActiveState.ResetState();
                        break;
                }
            }
        }

    }

}