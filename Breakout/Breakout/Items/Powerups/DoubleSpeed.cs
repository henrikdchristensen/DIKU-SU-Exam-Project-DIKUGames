using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

/// <summary>
/// Makes double speed for ball
/// </summary>
namespace Breakout.Items.Powerups {
    public class DoubleSpeed : Powerup {

        /// <summary>
        /// Returns the type as enum
        /// </summary>
        public override PowerupType Type => PowerupType.DoubleSpeed;
        private const float DURATION = 4;
        private const float SCALER = 2f;
        public DoubleSpeed(DynamicShape shape, IBaseImage image) : base(shape, image, DURATION) { }

        /// <summary>
        /// Sends event to activate powerup
        /// </summary>
        public override void Activate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SCALE_SPEED_MSG, objArg: SCALER);
        }

        /// <summary>
        /// Sends event to deactivate powerup
        /// </summary>
        public override void Deactivate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SCALE_SPEED_MSG, objArg: 1f / SCALER);
        }
    }
}
