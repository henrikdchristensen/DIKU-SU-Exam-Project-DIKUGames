using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.Game {

    public class StateMachine : IGameEventProcessor {

        /// <summary>
        /// 
        /// </summary>
        public IGameState ActiveState {
            get; private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public StateMachine() {
            GameBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GameBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            //Instantiate state-objects to avoid lagging at game transition
            ActiveState = States.MainMenu.GetInstance();
            States.GameRunning.GetInstance();
            States.GamePaused.GetInstance();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateType"></param>
        private void SwitchState(GameStateType stateType) {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameEvent"></param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                GameStateType state = StateTransformer.TransformStringToState(gameEvent.StringArg1);
                switch (gameEvent.Message) {
                    case "CHANGE_STATE":
                        SwitchState(state);
                        break;
                    case "CHANGE_STATE_RESET":
                        SwitchState(state);
                        ActiveState.ResetState();
                        break;
                }
            } else if (gameEvent.EventType == GameEventType.InputEvent) {

            }
        }

    }

}