using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;

namespace Breakout.Game.States {

    /// <summary>
    /// Represents the state, when the player has won/lost
    /// </summary>
    public class GameOver : IGameState, IGameEventProcessor {

        private GameEventBus eventBus;
        private string finalScore;
        private static GameOver instance = null;
        private Text[] menuButtons;
        private Text scoreText;

        private readonly Vec3F ACITVED_COLOR = new Vec3F(247f / 255f, 145f / 255f, 0);
        private readonly Vec3F DEACITVED_COLOR = new Vec3F(150f / 255f, 150f / 255f, 150f / 255f);
        private readonly Vec3F SCORE_COLOR = new Vec3F(247f / 255f, 247f / 255f, 27f / 255f);

        private Entity background;

        /// <summary> The active button of the menu, 0 = continue, 1 = main manu </summary>
        /// <remarks>Made public for testing purposes</remarks>
        public int activeMenuButton { get; private set; }

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
            background = new Entity(
                new StationaryShape(0, 0, 1, 1),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "BreakoutTitleScreen.png")));

            scoreText = new Text("Score: 0", new Vec2F(0.25f, 0.13f), new Vec2F(0.65f, 0.65f));
            scoreText.SetColor(SCORE_COLOR);

            menuButtons = new Text[] {
                new Text("Main Menu", new Vec2F(0.15f, -0.2f), new Vec2F(0.8f, 0.8f)),
                new Text("Quit", new Vec2F(0.35f, -0.4f), new Vec2F(0.8f, 0.8f))
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
            background.RenderEntity();
            
            scoreText.RenderText();
            for (int i = 0; i < menuButtons.Length; i++) {
                if (activeMenuButton == i)
                    menuButtons[i].SetColor(ACITVED_COLOR);
                else
                    menuButtons[i].SetColor(DEACITVED_COLOR);
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
        /// <param name="gameEvent">Processing events from the event bus</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch (gameEvent.Message) {
                    case "SCORE":
                        scoreText.SetText("Score: " + gameEvent.StringArg1);
                        break;
                }
            }
        }

    }

}