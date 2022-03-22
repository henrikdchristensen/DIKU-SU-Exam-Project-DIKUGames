using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.IO;
using System;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Galaga;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            return MainMenu.instance;
        }

        public void InitializeGameState() {
            backGroundImage =
                new Entity(new DynamicShape(0, 0, 1, 1),
                new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            
            menuButtons = new Text[] {
                new Text("New Game", new Vec2F(0.15f, 0), new Vec2F(0.8f, 0.8f)),
                new Text("Quit", new Vec2F(0.35f, -0.3f), new Vec2F(0.8f, 0.8f))
            };
            activeMenuButton = 0;
        }

        public void ResetState() {

        }

        public void UpdateState() {

        }

        public void RenderState() {
            backGroundImage.RenderEntity();

            for (int i = 0; i < menuButtons.Length; i++) {
                if (activeMenuButton == i)
                    menuButtons[i].SetColor(new Vec3F(0, 1, 0));
                else
                    menuButtons[i].SetColor(new Vec3F(1, 1, 1));
                menuButtons[i].RenderText();
            }
                    
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    keyPressed(key);
                    break;
                case KeyboardAction.KeyRelease:
                    keyReleased(key);
                    break;
            }
        }

        private void keyPressed(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Up:
                    activeMenuButton = Math.Max(0, activeMenuButton - 1);
                    break;
                case KeyboardKey.Down:
                    activeMenuButton = Math.Min(menuButtons.Length - 1, activeMenuButton + 1);
                    break;
                case KeyboardKey.Enter:
                    if (activeMenuButton == 0)
                        changeState(GameStateType.GameRunning);
                    else
                        window.CloseWindow();
                    break;
            }
        }

        private void keyReleased(KeyboardKey key) {

        }

        private void changeState(GameStateType state) {
            var e = new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "CHANGE_STATE",
                StringArg1 = StateTransformer.TransformStateToString(state)
            };
            GalagaBus.GetBus().RegisterEvent(e);
        }
    }
}