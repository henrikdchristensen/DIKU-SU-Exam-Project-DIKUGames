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

namespace Galaga
{
    public class Game : DIKUGame, IGameEventProcessor
    {
        private Player player;
        private GameEventBus eventBus;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;

        public Game(WindowArgs windowArgs) : base(windowArgs){
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });
            window.SetKeyEventHandler(KeyHandler);
            
            eventBus.Subscribe(GameEventType.InputEvent, this);  
            eventBus.Subscribe(GameEventType.PlayerEvent, player);          
            

            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            var enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            
            for (int i = 0; i < numEnemies; i++){
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, images),
                    new ImageStride(80, enemyStridesRed)));
            }
            

            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));


            enemyExplosions = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
                

        }

        public void AddExplosion(Vec2F position, Vec2F extent) {
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent), 
                EXPLOSION_LENGTH_MS, 
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides));            
        }

        private void IterateShots(){
            playerShots.Iterate(shot =>{
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1){
                    shot.DeleteEntity();
                } else{
                    enemies.Iterate(enemy =>
                    {
                        CollisionData data = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);
                        if (data.Collision) {
                            shot.DeleteEntity();
                            enemy.Hit();
                            AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                        }
                    });
                }
            });
        }

        public override void Render(){
            player.Render();
            enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
        }

        public override void Update(){
          eventBus.ProcessEventsSequentially();
          player.Move();
          IterateShots();
        }
        
        private void KeyHandler(KeyboardAction action, KeyboardKey key){
            switch (action){
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }
        }

        public void KeyPress(KeyboardKey key) {
            switch (key){
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
            switch (key){
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