using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Items.Powerups {
    /// <summary>
    /// When activated the ball should be hardened - should not collide with destroyable blocks
    /// </summary>
    public class HardBall : Powerup {

        /// <summary>
        /// Returns the type as enum
        /// </summary>
        public override PowerupType Type => PowerupType.HardBall;
        private const float DURATION = 4;

        public HardBall(DynamicShape shape, IBaseImage image) : base(shape, image, DURATION) { }

        /// <summary>
        /// Sends out event to activate powerup
        /// </summary>
        public override void Activate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SET_HARD_MSG, objArg: true);
        }


        /// <summary>
        /// Sends out event to deactivate powerup
        /// </summary>
        public override void Deactivate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SET_HARD_MSG, objArg: false);
        }
    }
}
