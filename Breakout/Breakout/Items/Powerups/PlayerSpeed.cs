﻿using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Entities.Powerups {
    public class PlayerSpeed : Powerup {

        /// <summary>
        /// Returns the type as enum
        /// </summary>
        public override PowerupType Type => PowerupType.PlayerSpeed;
        private const float DURATION = 4;
        private const float SCALER = 1.5f;
        public PlayerSpeed(DynamicShape shape, IBaseImage image) : base(shape, image, DURATION) {}
        
        /// <summary>
        /// Sends out event to activate powerup
        /// </summary>
        public override void Activate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Player.SCALE_SPEED_MSG, objArg: SCALER);
        }

        /// <summary>
        /// Sends out event to deactivate powerup
        /// </summary>
        public override void Deactivate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Player.SCALE_SPEED_MSG, objArg: 1f / SCALER);
        }
    }
}
