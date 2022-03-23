using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.Security.Principal;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Galaga.MovementStrategy;
using Galaga.GalagaStates;
using System;

namespace Galaga.GalagaStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;

        private Player player;

        private EntityContainer<Enemy> enemies;

        private EntityContainer<PlayerShot> playerShots;

        private IBaseImage playerShotImage;

        private AnimationContainer enemyExplosions;

        private List<Image> explosionStrides;

        private const int EXPLOSION_LENGTH_MS = 500;

        private IMovementStrategy movementStrategy;

        private List<MovementStrategy.IMovementStrategy> movementStrategyList;

        private Score scoreboard;

        private float movementSpeed = 0.0003f;

        private const float DELTA_SPEED = 0.0005f;

        private List<Image> enemyStridesBlue;

        private List<Image> enemyStridesRed;

        private GameEventBus eventBus;

        private Boolean isGameOver { get; set; } = false;


        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        public void InitializeGameState() {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            eventBus = GalagaBus.GetBus();
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("", "", "Assets", "Images", "BlueMonster.png"));
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));

            movementStrategyList = new List<MovementStrategy.IMovementStrategy> {
                new MovementStrategy.Down(),
                new MovementStrategy.NoMove(),
                new MovementStrategy.Zigzag()
            };
            createSquadron();

            scoreboard = new Score(new Vec2F(0.1f, 0.5f), new Vec2F(0.5f, 0.5f));

            playerShots = new EntityContainer<PlayerShot>(10);
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));


            enemyExplosions = new AnimationContainer(8);
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));

        }

        public void ResetState() {
            scoreboard.Reset();
            createSquadron();
        }

        public void UpdateState() {
            player.Move();
            movementStrategy.MoveEnemies(enemies);
            IterateShots();
            handleSquadron();
        }

        public void RenderState() {
            if (!isGameOver) {
                player.Render();
                enemies.RenderEntities();
                playerShots.RenderEntities();
                enemyExplosions.RenderAnimations();
                checkGameOver();
            }
            scoreboard.RenderScore();
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

        /// <summary> Add an explosion on the screen </summary>
        /// <param name = "position"> the position of the explosion </param>
        /// <param name = "extent"> extent (size) of the explosion </param>
        private void AddExplosion(Vec2F position, Vec2F extent) {
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent),
                EXPLOSION_LENGTH_MS,
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides));
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1) {
                    shot.DeleteEntity();
                } else {
                    enemies.Iterate(enemy => {
                        CollisionData data = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);
                        if (data.Collision) {
                            shot.DeleteEntity();
                            if (enemy.Hit()) {
                                //Is dead
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                                enemy.DeleteEntity();
                                scoreboard.AddPoints();
                            }
                        }
                    });
                }
            });
        }

        private void checkGameOver() {
            foreach (Enemy e in enemies) {
                if (e.Shape.Position.Y <= 0.0) {
                    isGameOver = true;
                }
            }
        }

        private void handleSquadron() {
            if (enemies.CountEntities() == 0) {
                movementSpeed += DELTA_SPEED;
                createSquadron();
            }
        }
        private void createSquadron() {
            var rnd = new Random();

            int enemyCount = rnd.Next(5);
            List<Squadron.ISquadron> squadronList = new List<Squadron.ISquadron> {
                new Squadron.Straight(enemyCount, movementSpeed),
                new Squadron.Zigzag(enemyCount, movementSpeed),
                new Squadron.VFormation(enemyCount, movementSpeed)
            };

            int squadronNum = rnd.Next(squadronList.Count);
            Squadron.ISquadron squadron = squadronList[squadronNum];
            squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            enemies = squadron.Enemies;

            int movementNum = rnd.Next(movementStrategyList.Count);
            movementStrategy = movementStrategyList[movementNum];
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
                    playerShots.AddEntity(new PlayerShot(player.GetPosition(), playerShotImage));
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