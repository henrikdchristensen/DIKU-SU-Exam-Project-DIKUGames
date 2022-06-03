﻿using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Items.Powerups {
    public class DoubleSize : Powerup {
        public override PowerupType Type => PowerupType.DoubleSize;
        private const float DURATION = 4;
        private const float SCALER = 2f;
        public DoubleSize(DynamicShape shape, IBaseImage image) : base(shape, image, DURATION) { }

        public override void Activate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SCALE_SIZE_MSG, objArg: SCALER);
        }

        public override void Deactivate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SCALE_SIZE_MSG, objArg: 1f / SCALER);
        }
    }
}