using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Items.Powerups {

    public class DoubleSize : Powerup {

        private const float SIZE_SACLE = 1.3f;
        public DoubleSize(DynamicShape shape, IBaseImage image) : base(shape, image) {
            duration = 5;
        }

        public override PowerupType TAG => PowerupType.DoubleSize;

        public override void Activate(GameObject obj) {
            obj.Shape.Extent.X *= SIZE_SACLE;
        }

        public override void Deactivate(GameObject obj) {
            obj.Shape.Extent.X /= SIZE_SACLE;
        }
    }
}
