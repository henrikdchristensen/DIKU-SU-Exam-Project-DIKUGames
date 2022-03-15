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
//using Galaga.Squadron;

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {
        private Player  player;
        private GameEventBus eventBus;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage; 
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private IMovementStrategy movementStrategyDown;
        private Score scoreboard;
        private float movementSpeed = 0.0003f;
        private const float DELTA_SPEED = 0.0005f;
        private List<Image> enemyStridesBlue;
        private List<Image> enemyStridesRed; 

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });
            window.SetKeyEventHandler(KeyHandler);

            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, player);


            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));

            //const int numEnemies = 9;
            createSquadron();

            //movementStrategyDown = new Down();
            movementStrategyDown = new Zigzag();

            //TODO: sizes need to be changed properly
            scoreboard = new Score(new Vec2F(0.1f, 0.5f), new Vec2F(0.5f, 0.5f));


            playerShots = new EntityContainer<PlayerShot>(10);
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));


            enemyExplosions = new AnimationContainer(8);
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));

        }

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

        public override void Render() {
            player.Render();
            enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
            scoreboard.RenderScore();
        }

        public override void Update() {
            eventBus.ProcessEventsSequentially();
            player.Move();
            movementStrategyDown.MoveEnemies(enemies);
            IterateShots();
            handleSquadron();
            handleGameOver();
        }

        private void handleGameOver() {
            foreach (Enemy e in enemies) {
                if (e.Shape.Position.Y <= 0.6) {
                    deleteAll();
                    return;
                }
            }
        }

        private void deleteAll() {
            //player = null;
            enemies = null;
            //playerShots = null;
            // foreach (Entity e in enemies) {
            //     e.DeleteEntity();
            // }
            return;
        }

        private void handleSquadron() {
            if (enemies.CountEntities() == 0) {
                movementSpeed += DELTA_SPEED;
                createSquadron();
            }
        }

        private void createSquadron() {
            Squadron.ISquadron squadron = new Squadron.VFormation(1, movementSpeed);
            squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            enemies = squadron.Enemies;
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

        public void KeyPress(KeyboardKey key) {
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
        public void KeyRelease(KeyboardKey key) {
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