﻿using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Items.Powerups {
    public class PlayerSpeed : Powerup {
        public override PowerupType Type => PowerupType.PlayerSpeed;
        private const float DURATION = 4;
        private const float SCALER = 1.5f;
        public PlayerSpeed(DynamicShape shape, IBaseImage image) : base(shape, image, DURATION) {}

        public override void Activate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Player.SCALE_SPEED_MSG, objArg: SCALER);
        }

        public override void Deactivate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Player.SCALE_SPEED_MSG, objArg: 1f / SCALER);
        }
    }
}