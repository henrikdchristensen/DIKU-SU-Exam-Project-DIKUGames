using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.GUI;

namespace Breakout.Game {

    public class Game : DIKUGame, IGameEventProcessor {

        private GameEventBus eventBus;
        private StateMachine stateMachine;

        /// <summary>Game are responsible for updating and rendering the game</summary>
        /// <param name="windowArgs">fundamental properties of the window</param>
        /// <returns>A player instance</returns>
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            eventBus = GameBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.PlayerEvent, GameEventType.GameStateEvent, GameEventType.WindowEvent, GameEventType.StatusEvent, GameEventType.ControlEvent });
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            window.SetKeyEventHandler(KeyHandler);
            stateMachine = new StateMachine();
        }

        /// <summary>Call RenderState for the active state</summary>
        public override void Render() {
            stateMachine.ActiveState.RenderState();
        }

        /// <summary>Call UpdateState for the active state and process events sequentially</summary>
        public override void Update() {
            stateMachine.ActiveState.UpdateState();
            eventBus.ProcessEventsSequentially();
        }

        /// <summary>Call HandleKeyEvent for the active state</summary>
        /// <param name="action"></param>
        /// <param name="key"></param>
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        }

        /// <summary>Process the GameEvents: CLOSE_WINDOW</summary>
        /// <param name="gameEvent">The game event recieved</param>
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