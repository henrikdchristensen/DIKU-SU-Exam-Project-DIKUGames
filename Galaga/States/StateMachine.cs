using DIKUArcade.Events;
using DIKUArcade.State; 

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameRunning.GetInstance();
            GamePaused.GetInstance();
            
        }
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance(); 
                    break;
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance(); 
                    break; 
            }
        }
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