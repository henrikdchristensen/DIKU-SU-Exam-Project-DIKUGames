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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static GamePaused GetInstance() {
            if (GamePaused.instance == null) {
                GamePaused.instance = new GamePaused();
                GamePaused.instance.InitializeGameState();
            }
            return GamePaused.instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeGameState() {
            menuButtons = new Text[] {
                new Text("Continue", new Vec2F(0.2f, 0), new Vec2F(0.8f, 0.8f)),
                new Text("Main menu", new Vec2F(0.15f, -0.3f), new Vec2F(0.8f, 0.8f))
            };
            activeMenuButton = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetState() {
            activeMenuButton = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateState() {

        }

        /// <summary>
        /// 
        /// </summary>
        public void RenderState() {
            for (int i = 0; i < menuButtons.Length; i++) {
                if (activeMenuButton == i)
                    menuButtons[i].SetColor(new Vec3F(0, 1, 0));
                else
                    menuButtons[i].SetColor(new Vec3F(1, 1, 1));
                menuButtons[i].RenderText();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="key"></param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    keyPressed(key);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
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
                        GameBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = StateTransformer.TransformStateToString(GameStateType.GameRunning)
                        });
                    } else {
                        GameBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = StateTransformer.TransformStateToString(GameStateType.MainMenu)
                        });
                    }
                    break;
            }
        }

    }
}