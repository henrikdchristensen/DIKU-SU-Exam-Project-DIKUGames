﻿using DIKUArcade.Events;
using Breakout.Game;
using Breakout.Levels;

namespace Breakout.Items.Powerups {

    public class PowerupContainer : IGameEventProcessor {

        private static PowerupContainer instance = null;

        public static PowerupContainer GetPowerupContainer() {
            return instance ?? (instance = new PowerupContainer());
        }

        private PowerupContainer() {
            GameBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
            GameBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        }

        private List<PowerupType> active = new List<PowerupType>();


        /// <summary>
        /// Activate a powerup
        /// </summary>
        /// <param name="powerup">Powerup to be activated</param>
        private void activate(Powerup powerup) {
            if (!active.Contains(powerup.Type))
                powerup.Activate();
            if (powerup.Duration > 0)
                active.Add(powerup.Type);
                       
        }

        /// <summary>
        /// Deactivate a powerup
        /// </summary>
        /// <param name="powerup">Powerup that should be deactivated</param>
        private void deactivate(Powerup powerup) {
            if (active.Contains(powerup.Type)) {
                active.Remove(powerup.Type);
                if (!active.Contains(powerup.Type)) {
                    powerup.Deactivate();
                }
            }  
        }

        private void flush() {
            active = new List<PowerupType>();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.Message) {
                case Powerup.CAN_ACTIVATE_MSG:
                    activate((Powerup) gameEvent.ObjectArg1);
                    break;
                case Powerup.CAN_DEACTIVATE_MSG:
                    deactivate((Powerup) gameEvent.ObjectArg1);
                    break;
                case LevelContainer.NEXT_LEVEL_MSG:
                    flush();
                    break;
            }
        }
    }
}