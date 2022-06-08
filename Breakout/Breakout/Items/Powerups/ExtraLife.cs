using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Items.Powerups {
    public class ExtraLife : Powerup {
        public override PowerupType Type => PowerupType.ExtraLife;
        public ExtraLife(DynamicShape shape, IBaseImage image) : base(shape, image) { }

        /// <summary>
        /// Sends out event to activate extralife
        /// </summary>
        public override void Activate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Player.ADD_LIFE_MSG);
        }


        /// <summary>
        /// Empty function body as it cannot not be deactivated
        /// </summary>
        public override void Deactivate() {}
    }
}
