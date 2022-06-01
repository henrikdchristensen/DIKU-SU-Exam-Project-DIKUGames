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

        /// <summary>Get the one and only instance of the class</summary>
        /// <returns>Returns a instance of GameRunning</returns>
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        /// <summary>
        /// Initialize the game state setting up levels, score,
        /// player and collisionHandler
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
            eventBus.Subscribe(GameEventType.ControlEvent, player);
            eventBus.Subscribe(GameEventType.StatusEvent, this);
        }

        /// <summary>Call reset methods for levels, score and player</summary>
        public void ResetState() {
            levels.Reset();
            score.Reset();
            player.Reset();
        }

        /// <summary>Call update methods for collisionHandler, player and levels</summary>
        public void UpdateState() {
            collisionHandler.Update();
            player.Update();
            levels.ActiveLevel.Update();
        }

        /// <summary>Call render methods for levels, player and score</summary>
        public void RenderState() {
            levels.ActiveLevel.Render();
            player.Render();
            score.Render();
        }

        /// <summary>Handle KeyEventAction and call the corresponding method</summary>
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

        /// <summary>Handle the different KeyPress actions and trigger their events</summary>
        /// <param name="key"></param>
        private void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Escape:
                    GameBus.TriggerEvent(GameEventType.GameStateEvent, "CHANGE_STATE_RESET", StateTransformer.StateToString(GameStateType.GamePaused));
                    break;
                case KeyboardKey.Left:
                    GameBus.TriggerEvent(GameEventType.PlayerEvent, "LeftPressed");
                    break;
                case KeyboardKey.Right:
                    GameBus.TriggerEvent(GameEventType.PlayerEvent, "RightPressed");
                    break;
                case KeyboardKey.Space:
                    break;
            }
        }

        /// <summary>Handle KeyRelease actions and trigger their events</summary>
        /// <param name="key"></param>
        private void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    GameBus.TriggerEvent(GameEventType.PlayerEvent, "LeftReleased");
                    break;
                case KeyboardKey.Right:
                    GameBus.TriggerEvent(GameEventType.PlayerEvent, "RightReleased");
                    break;
                case KeyboardKey.Space:
                    levels.ActiveLevel.DeleteBlock();
                    break;
            }
        }

        /// <summary>Process events: Block destroyed</summary>
        /// <param name="gameEvent"></param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch (gameEvent.Message) {
                    case "BLOCK_DESTROYED":
                        score.AddPoints(gameEvent.IntArg1);
                        break;
                }
            }
        }

    }

}