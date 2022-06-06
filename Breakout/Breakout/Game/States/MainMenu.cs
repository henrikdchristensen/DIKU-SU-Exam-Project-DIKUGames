using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;

namespace Breakout.Game.States {

    public class MainMenu : IGameState {

        private static MainMenu instance = null;
        private Text[] menuButtons;
        private readonly Vec3F ACITVED_COLOR = new Vec3F(247f / 255f, 145f / 255f, 0);
        private readonly Vec3F DEACITVED_COLOR = new Vec3F(150f / 255f, 150f / 255f, 150f / 255f);
        public int activeMenuButton { get; private set; } // public get for testing purpose

        private Entity background;

        /// <summary>Get the one and only instance of the class</summary>
        /// <returns>Returns a instance of MainMenu</returns>
        public static MainMenu GetInstance() {
            if (instance == null) {
                instance = new MainMenu();
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
                new Text("New Game", new Vec2F(0.15f, -0.1f), new Vec2F(0.8f, 0.8f)),
                new Text("Quit", new Vec2F(0.35f, -0.35f), new Vec2F(0.8f, 0.8f))
            };

            activeMenuButton = 0;
        }

        /// <summary>No code</summary>
        public void ResetState() { }

        /// <summary>No code</summary>
        public void UpdateState() { }

        /// <summary>Set correct color to menu buttons</summary>
        public void RenderState() {
            background.RenderEntity();

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
        /// <param name="key">A key which could be Key-Up, Key-Down or Enter</param>
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
                        GameBus.TriggerEvent(GameEventType.GameStateEvent, "CHANGE_STATE_RESET",
                            StateTransformer.StateToString(GameStateType.GameRunning));
                    } else {
                        GameBus.TriggerEvent(GameEventType.WindowEvent, "CLOSE_WINDOW");
                    }
                    break;
            }
        }

    }

}