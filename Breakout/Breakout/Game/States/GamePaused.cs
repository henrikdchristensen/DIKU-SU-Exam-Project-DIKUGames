using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using DIKUArcade.Entities;

namespace Breakout.Game.States {
    /// <summary>
    /// Represents the state, when the game is paused
    /// </summary>
    public class GamePaused : IGameState {

        private static GamePaused instance = null;
        private Text[] menuButtons;

        /// <summary> The active button of the menu, 0 = continue, 1 = main manu </summary>
        /// <remarks>Made public for testing purposes</remarks>
        public int ActiveMenuButton { get; private set; } // public get for testing purpose
        private readonly Vec3F ACITVED_COLOR = new Vec3F(247f / 255f, 145f / 255f, 0);
        private readonly Vec3F DEACITVED_COLOR = new Vec3F(150f / 255f, 150f / 255f, 150f / 255f);

        private Entity background;

        /// <summary>Get the one and only instance of the class</summary>
        /// <returns>Returns a instance of GamePaused</returns>
        public static GamePaused GetInstance() {
            if (instance == null) {
                instance = new GamePaused();
                instance.InitializeGameState();
            }
            return instance;
        }

        /// <summary>Initialize the game state by setting correct menu items</summary>
        public void InitializeGameState() {
            background = new Entity(
                new StationaryShape(0, 0, 1, 1),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "BreakoutTitleScreen.png")));

            menuButtons = new Text[] {
                new Text("Continue", new Vec2F(0.2f, -0.1f), new Vec2F(0.8f, 0.8f)),
                new Text("Main menu", new Vec2F(0.15f, -0.35f), new Vec2F(0.8f, 0.8f))
            };
            ActiveMenuButton = 0;
        }

        /// <summary>Reset the active menu button to the first one</summary>
        public void ResetState() {
            ActiveMenuButton = 0;
            StaticTimer.PauseTimer();
        }

        /// <summary>No code (needs to be implemented - interface)</summary>
        public void UpdateState() { }

        /// <summary>Set correct color to menu buttons</summary>
        public void RenderState() {
            background.RenderEntity();
            for (int i = 0; i < menuButtons.Length; i++) {
                if (ActiveMenuButton == i)
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
                    ActiveMenuButton = Math.Max(0, ActiveMenuButton - 1);
                    break;
                case KeyboardKey.Down:
                    ActiveMenuButton = Math.Min(menuButtons.Length - 1, ActiveMenuButton + 1);
                    break;
                case KeyboardKey.Enter:
                    if (ActiveMenuButton == 0) {
                        GameBus.TriggerEvent(GameEventType.GameStateEvent, "CHANGE_STATE",
                            StateTransformer.StateToString(GameStateType.GameRunning));
                    } else {
                        GameBus.TriggerEvent(GameEventType.GameStateEvent, "CHANGE_STATE",
                            StateTransformer.StateToString(GameStateType.MainMenu));
                    }
                    break;
            }
        }

    }

}