using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Levels;

namespace Breakout.Game.States {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;

        private GameEventBus eventBus;

        private Level currentLevel;

        private LevelLoader loader;

        private Player player;

        private GameRunning() { }

        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        public void InitializeGameState() {
            loader = new LevelLoader();
            currentLevel = loader.CreateLevel(Path.Combine("Assets", "Levels", "level3.txt"));

            player = new Player(
                new DynamicShape(new Vec2F(0.42f, 0.01f), new Vec2F(0.16f, 0.022f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));

            eventBus = GameBus.GetBus();
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
        }

        public void ResetState() {
        }

        public void UpdateState() {
        
        }

        public void RenderState() {
            currentLevel.Render();
            player.Render();
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }
        }

        private void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Escape:
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE_RESET",
                        StringArg1 = StateTransformer.TransformStateToString(GameStateType.GamePaused)
                    });
                    break;
                case KeyboardKey.Left:
                    break;
                case KeyboardKey.Right:
                    break;
                case KeyboardKey.Space:
                    break;
            }
        }
        private void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    break;
                case KeyboardKey.Right:
                    break;
            }
        }

    }
}