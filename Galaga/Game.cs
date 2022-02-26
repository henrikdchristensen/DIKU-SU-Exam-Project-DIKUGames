using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.GUI;
using DIKUArcade;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class Game : DIKUGame, IGameEventProcessor
    {
        private Player player;
        private GameEventBus eventBus;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        
        public Game(WindowArgs windowArgs) : base(windowArgs){
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++){
                enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, images)));
            }
            player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);
        }

        private void IterateShots(){
            playerShots.Iterate(shot =>{
                // TODO: move the shot's shape
                if ( true ){
                }
                // TODO: delete shot
                else{
                    enemies.Iterate(enemy =>
                    {
                        // TODO: if collision btw shot and enemy -> delete both
                    });
                }
            });
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key){
            //Console.WriteLine($"TestKeyEvents.KeyHandler({action}, {key})");
            switch (action){
                case KeyboardAction.KeyRelease:
                    switch (key){
                        case KeyboardKey.Escape:
                            window.CloseWindow();
                            break;
                        case KeyboardKey.Left:
                            player.SetMoveLeft(false);
                            break;
                        case KeyboardKey.Right:
                            player.SetMoveRight(false);
                            break;
                    }
                    break;
                case KeyboardAction.KeyPress:
                    switch (key){
                        case KeyboardKey.Left:
                            player.SetMoveLeft(true);
                            break;
                        case KeyboardKey.Right:
                            player.SetMoveRight(true);
                            break;
                    }
                    break;
            }
        }

        public override void Render(){
            player.Render();
        }

        public override void Update(){
          eventBus.ProcessEventsSequentially();
          player.Move();
        }

        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.WindowEvent){
                switch (gameEvent.Message){
                    // TODO: Implement
                    default:
                        break;
                }
            }
        }

    }

}