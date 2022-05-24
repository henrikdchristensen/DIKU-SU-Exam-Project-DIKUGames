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

    public class GameRunning : IGameState, IGameEventProcessor {

        private static GameRunning instance = null;
        private GameEventBus eventBus;
        private Score score;
        private Level currentLevel;
        private LevelLoader loader;
        private LevelContainer levels;
        private Player player;
        private CollisionHandler collisionHandler;
        private GameRunning() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeGameState() {
            levels = LevelContainer.GetLevelContainer();
            
            score = new Score(new Vec2F(0.1f, 0.5f), new Vec2F(0.5f, 0.5f));
            player = new Player(
                new DynamicShape(new Vec2F(0.42f, 0.01f), new Vec2F(0.16f, 0.022f)),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));

            collisionHandler = CollisionHandler.GetInstance();

            collisionHandler.Subsribe(player);
            collisionHandler.Subsribe(new Wall(new StationaryShape(1, 0, 0.1f, 1)));
            collisionHandler.Subsribe(new Wall(new StationaryShape(-0.1f, 0, 0.1f, 1)));
            collisionHandler.Subsribe(new Wall(new StationaryShape(0, 1, 1, 0.1f)));

            eventBus = GameBus.GetBus();
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            eventBus.Subscribe(GameEventType.StatusEvent, this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetState() {
            levels.Reset();
            score.Reset();
            player.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateState() {
            collisionHandler.Update();
            player.Move();
            levels.ActiveLevel.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RenderState() {
            levels.ActiveLevel.Render();
            player.Render();
            score.Render();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="key"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        private void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    registerPlayerEvent("LeftReleased");
                    break;
                case KeyboardKey.Right:
                    registerPlayerEvent("RightReleased");
                    break;
                case KeyboardKey.Space:
                    levels.ActiveLevel.DeleteBlock();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void registerPlayerEvent(string message) {
            var e = new GameEvent {
                Message = message,
                EventType = GameEventType.PlayerEvent
            };
            eventBus.RegisterEvent(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameEvent"></param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                Console.WriteLine(gameEvent.Message);
                switch (gameEvent.Message) {
                    case "BLOCK_DESTROYED":
                        score.AddPoints(gameEvent.IntArg1);
                        break;
                }
            }
        }

    }

}