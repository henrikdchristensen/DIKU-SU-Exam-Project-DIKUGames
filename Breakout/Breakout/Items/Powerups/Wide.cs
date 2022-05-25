using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Items.Powerups {

    public class Wide : Powerup {

        private const float SIZE_SACLE = 1.3f;
        public Wide(DynamicShape shape, IBaseImage image) : base(shape, image) {
            duration = 5;
        }

        public override PowerupType TAG => PowerupType.Wide;

        public override void Activate(GameObject obj) {
            obj.Shape.Extent.X *= SIZE_SACLE;
        }

        public override void Deactivate(GameObject obj) {
            obj.Shape.Extent.X /= SIZE_SACLE;
        }
    }
}
