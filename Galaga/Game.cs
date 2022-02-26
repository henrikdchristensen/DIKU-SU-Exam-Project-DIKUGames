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
        
        public Game(WindowArgs windowArgs) : base(windowArgs){
            player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);
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