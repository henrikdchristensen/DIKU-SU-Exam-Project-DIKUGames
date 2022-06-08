using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Entities.Powerups {
    public class DoubleSpeed : Powerup {
        public override PowerupType Type => PowerupType.DoubleSpeed;
        private const float DURATION = 4;
        private const float SCALER = 2f;
        public DoubleSpeed(DynamicShape shape, IBaseImage image) : base(shape, image, DURATION) { }

        public override void Activate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SCALE_SPEED_MSG, objArg: SCALER);
        }

        public override void Deactivate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SCALE_SPEED_MSG, objArg: 1f / SCALER);
        }
    }
}
