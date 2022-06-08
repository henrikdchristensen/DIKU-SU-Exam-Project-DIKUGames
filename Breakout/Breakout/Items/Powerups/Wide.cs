using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Items.Powerups {
    public class Wide : Powerup {
        public override PowerupType Type => PowerupType.Wide;
        private const float DURATION = 4;
        private const float SCALER = 1.5f;
        public Wide(DynamicShape shape, IBaseImage image) : base(shape, image, DURATION) { }

        /// <summary>
        /// Activating powerup via event
        /// </summary>
        public override void Activate() {
            Console.WriteLine("ACTIVATED PLAYER SPEED");
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Player.SCALE_WIDE_MSG, objArg: SCALER);
        }

        /// <summary>
        /// Deactivating powerup event
        /// </summary>
        public override void Deactivate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Player.SCALE_WIDE_MSG, objArg: 1f / SCALER);
        }
    }
}
