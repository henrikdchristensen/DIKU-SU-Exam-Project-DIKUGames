using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Entities.Powerups {
    public class Split : Powerup {
        public override PowerupType Type => PowerupType.Split;
        public Split(DynamicShape shape, IBaseImage image) : base(shape, image) { }

        public override void Activate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                BallContainer.SPLIT_BALLS_MSG);
        }

        public override void Deactivate() {
        }
    }
}
