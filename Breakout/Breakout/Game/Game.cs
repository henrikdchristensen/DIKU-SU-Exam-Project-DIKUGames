using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.GUI;

namespace Breakout.Game {

    public class Game : DIKUGame, IGameEventProcessor {

        private GameEventBus eventBus;
        private StateMachine stateMachine;

        /// <summary> Game are responsible for updating and rendering the game </summary>
        /// <param name = "windowArgs"> fundamental properties of the window. </param>
        /// <returns> A player instance </returns>
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            eventBus = GameBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent, GameEventType.WindowEvent, GameEventType.StatusEvent });
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            window.SetKeyEventHandler(KeyHandler);
            stateMachine = new StateMachine();
        }

        /// <summary> To render game entities. </summary>
        public override void Render() {
            //TODO: SHOULD GAME BE RESPONSIBLE FOR THIS
            stateMachine.ActiveState.RenderState();
        }

        /// <summary> To update game logic. </summary>
        public override void Update() {
            //TODO: SHOULD GAME BE RESPONSIBLE FOR THIS
            stateMachine.ActiveState.UpdateState();
            eventBus.ProcessEventsSequentially();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="key"></param>
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        }

        /// <summary> To receive events from the event bus. </summary>
        /// <param name = "gameEvent"> the game-event recieved </param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                    case "CLOSE_WINDOW":
                        window.CloseWindow();
                        break;
                    default:
                        break;
                }
            }
        }

    }

}