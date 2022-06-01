using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.Game.States {
    public class GamePaused : IGameState {

        private static GamePaused instance = null;
        private Text[] menuButtons;
        private int activeMenuButton;

        /// <summary>Get the one and only instance of the class</summary>
        /// <returns>Returns a instance of GamePaused</returns>
        public static GamePaused GetInstance() {
            if (GamePaused.instance == null) {
                GamePaused.instance = new GamePaused();
                GamePaused.instance.InitializeGameState();
            }
            return GamePaused.instance;
        }

        /// <summary>Initialize the game state by setting correct menu items</summary>
        public void InitializeGameState() {
            menuButtons = new Text[] {
                new Text("Continue", new Vec2F(0.2f, 0), new Vec2F(0.8f, 0.8f)),
                new Text("Main menu", new Vec2F(0.15f, -0.3f), new Vec2F(0.8f, 0.8f))
            };
            activeMenuButton = 0;
        }

        /// <summary>Reset the active menu button to the first one</summary>
        public void ResetState() {
            activeMenuButton = 0;
        }

        /// <summary>No code</summary>
        public void UpdateState() {

        }

        /// <summary>Set correct color to menu buttons</summary>
        public void RenderState() {
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
                    KeyPressed(key);
                    break;
            }
        }

        /// <summary>Handle different KeyPressed action</summary>
        /// <param name="key">A key could be Key-Up, Key-Down or Enter</param>
        private void KeyPressed(KeyboardKey key) {
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