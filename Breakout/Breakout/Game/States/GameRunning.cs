using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Levels;
using Breakout.Collision;
using Breakout.Items;

namespace Breakout.Game.States {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;

        private GameEventBus eventBus;

        private Level currentLevel;

        private LevelLoader loader;

        private Player player;

        private CollisionHandler collisionHandler;

        private Ball ball;

        private GameRunning() {
        }

        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        public void InitializeGameState() {
            loader = new LevelLoader();
            currentLevel = loader.CreateLevel(Path.Combine("Assets", "Levels", "level1.txt"));
            collisionHandler = new CollisionHandler();

            player = new Player(
                new DynamicShape(new Vec2F(0.42f, 0.01f), new Vec2F(0.16f, 0.022f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));

            ball = new Ball(
                new DynamicShape(0.5f, 0.5f, 0.02f, 0.02f, -0.01f, 0.01f),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            collisionHandler = CollisionHandler.GetInstance();

            collisionHandler.Subsribe(player);
            collisionHandler.Subsribe(new Wall(new StationaryShape(1, 0, 0.1f, 1)));
            collisionHandler.Subsribe(new Wall(new StationaryShape(-0.1f, 0, 0.1f, 1)));
            collisionHandler.Subsribe(new Wall(new StationaryShape(0, 1, 1, 0.1f)));

            eventBus = GameBus.GetBus();
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
        }

        public void ResetState() {
        }

        public void UpdateState() {
            collisionHandler.Update();
            player.Move();
            ball.Move();
            currentLevel.Update();
        }

        public void RenderState() {
            collisionHandler.Update();
            currentLevel.Render();
            player.Render();
            ball.Render();
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
                    registerPlayerEvent("LeftPressed");
                    break;
                case KeyboardKey.Right:
                    registerPlayerEvent("RightPressed");
                    break;
                case KeyboardKey.Space:
                    break;
            }
        }
        private void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    registerPlayerEvent("LeftReleased");
                    break;
                case KeyboardKey.Right:
                    registerPlayerEvent("RightReleased");
                    break;
            }
        }

        private void registerPlayerEvent(string message) {
            var e = new GameEvent {
                Message = message,
                EventType = GameEventType.PlayerEvent
            };
            eventBus.RegisterEvent(e);
        }
    }
}