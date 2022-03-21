using System.Security.Principal;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.GUI;
using DIKUArcade;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Galaga.MovementStrategy;
using System;

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {
        private Player player;

        private GameEventBus eventBus;

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

        /// <summary> Game are responsible for updating and rendering the game </summary>
        /// <param name = "windowArgs"> fundamental properties of the window. </param>
        /// <returns> A player instance </returns>
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            eventBus = GalagaBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });
            window.SetKeyEventHandler(KeyHandler);

            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, player);


            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
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

        /// <summary> Add an explosion on the screen </summary>
        /// <param name = "position"> the position of the explosion </param>
        /// <param name = "extent"> extent (size) of the explosion </param>
        public void AddExplosion(Vec2F position, Vec2F extent) {
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

        /// <summary> To render game entities. </summary>
        public override void Render() {
            player.Render();
            enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
            scoreboard.RenderScore();
            handleGameOver();
        }

        /// <summary> To update game logic. </summary>
        public override void Update() {
            eventBus.ProcessEventsSequentially();
            player.Move();
            movementStrategy.MoveEnemies(enemies);
            IterateShots();
            handleSquadron();
        }

        private void handleGameOver() {
            foreach (Enemy e in enemies) {
                if (e.Shape.Position.Y <= 0.0) {
                    window.Clear();
                    scoreboard.RenderScore();
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

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
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
                    window.CloseWindow();
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

        /// <summary> To receive events from the event bus. </summary>
        /// <param name = "gameEvent"> the game-event recieved </param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                    // TODO: Implement
                    default:
                        break;
                }
            }
        }

    }

}