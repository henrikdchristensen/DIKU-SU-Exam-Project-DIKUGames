using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Items.Powerups {
    public class HardBall : Powerup {
        public override PowerupType Type => PowerupType.HardBall;
        private const float DURATION = 4;

        public HardBall(DynamicShape shape, IBaseImage image) : base(shape, image, DURATION) { }

        public override void Activate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SET_HARD_MSG, objArg: true);
        }

        public override void Deactivate() {
            GameBus.TriggerEvent(GameEventType.ControlEvent,
                Ball.SET_HARD_MSG, objArg: false);
        }
    }
}
