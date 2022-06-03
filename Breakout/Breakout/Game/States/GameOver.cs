using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.Game.States {

    public class GameOver : IGameState, IGameEventProcessor {

        private GameEventBus eventBus;
        private string finalScore;
        private static GameOver instance = null;
        private Text[] menuButtons;
        private Text scoreText;
        public int activeMenuButton { get; private set; } // public get for testing purpose

        /// <summary>Get the one and only instance of the class</summary>
        /// <returns>Returns a instance of GamePaused</returns>
        public static GameOver GetInstance() {
            if (instance == null) {
                instance = new GameOver();
                instance.InitializeGameState();
            }
            return instance;
        }

        /// <summary>Initialize the game state by setting correct menu items</summary>
        public void InitializeGameState() {
            scoreText = new Text("Score: ", new Vec2F(0.015f, 0.2f), new Vec2F(0.8f, 0.8f));
            menuButtons = new Text[] {
                new Text("Main Menu", new Vec2F(0.15f, 0), new Vec2F(0.8f, 0.8f)),
                new Text("Quit", new Vec2F(0.35f, -0.3f), new Vec2F(0.8f, 0.8f))
            };
            activeMenuButton = 0;

            eventBus = GameBus.GetBus();
            eventBus.Subscribe(GameEventType.StatusEvent, this);
        }

        /// <summary>Reset the active menu button to the first one</summary>
        public void ResetState() {
            activeMenuButton = 0;
        }

        /// <summary>No code (needs to be implemented - interface)</summary>
        public void UpdateState() { }

        /// <summary>Set correct color to menu buttons</summary>
        public void RenderState() {
            scoreText.SetText("Score: " + finalScore);
            scoreText.SetColor(new Vec3F(1, 1, 1));
            scoreText.RenderText();
            for (int i = 0; i < menuButtons.Length; i++) {
                if (activeMenuButton == i)
                    menuButtons[i].SetColor(new Vec3F(0, 1, 0));
                else
                    menuButtons[i].SetColor(new Vec3F(1, 1, 1));
                menuButtons[i].RenderText();
            }
        }

        /// <summary>Handle key event (KeyPress)</summary>
        /// <param name="action">KeyboardAction</param>
        /// <param name="key">KeyboardKey</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    keyPressed(key);
                    break;
            }
        }

        /// <summary>Handle different KeyPressed action</summary>
        /// <param name="key">A key could be Key-Up, Key-Down or Enter</param>
        private void keyPressed(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Up:
                    activeMenuButton = Math.Max(0, activeMenuButton - 1);
                    break;
                case KeyboardKey.Down:
                    activeMenuButton = Math.Min(menuButtons.Length - 1, activeMenuButton + 1);
                    break;
                case KeyboardKey.Enter:
                    if (activeMenuButton == 0) {
                        GameBus.TriggerEvent(GameEventType.GameStateEvent, "CHANGE_STATE",
                            StateTransformer.StateToString(GameStateType.MainMenu));
                    } else {
                        GameBus.TriggerEvent(GameEventType.WindowEvent, "CLOSE_WINDOW");
                    }
                    break;
            }
        }

        /// <summary>Process events: Game over</summary>
        /// <param name="gameEvent">TODO</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch (gameEvent.Message) {
                    case "SCORE":
                        finalScore = gameEvent.StringArg1;
                        break;
                }
            }
        }

    }

}