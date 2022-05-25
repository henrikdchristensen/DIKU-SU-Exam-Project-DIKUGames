using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Items.Powerups {

    public class DoubleSpeed : Powerup {

        private const float SPEED_SCALE = 2f;
        public DoubleSpeed(DynamicShape shape, IBaseImage image) : base(shape, image) {
            duration = 5;
        }

        public override PowerupType TAG => PowerupType.DoubleSpeed;

        public override void Activate(GameObject obj) {
            obj.Shape.AsDynamicShape().Direction *= SPEED_SCALE;
        }

        public override void Deactivate(GameObject obj) {
            Console.WriteLine("DEACTIVATED");
            obj.Shape.AsDynamicShape().Direction /= SPEED_SCALE;
        }
    }
}
