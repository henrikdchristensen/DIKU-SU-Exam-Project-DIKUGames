using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Items.Powerups {
    public class ExtraLife : Powerup {
        public override PowerupType Type => PowerupType.ExtraLife;
        public ExtraLife(DynamicShape shape, IBaseImage image) : base(shape, image) { }

        public override void Activate() {
            Console.WriteLine("ACTIVATED PLAYER SPEED");
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Player.ADD_LIFE_MSG);
        }

        public override void Deactivate() {}
    }
}
