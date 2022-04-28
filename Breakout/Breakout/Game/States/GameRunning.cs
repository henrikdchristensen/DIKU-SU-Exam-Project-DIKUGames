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
using System;

namespace Breakout.Game.States {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;

        private GameEventBus eventBus;

        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }

        public void InitializeGameState() {
            

        }

        public void ResetState() {
           
        }

        public void UpdateState() {
            
        }

        public void RenderState() {
            
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
                    break;
                case KeyboardKey.Right:
                    break;
                case KeyboardKey.Space:
                    break;
            }
        }
        private void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    break;
                case KeyboardKey.Right:
                    break;
            }
        }

    }
}