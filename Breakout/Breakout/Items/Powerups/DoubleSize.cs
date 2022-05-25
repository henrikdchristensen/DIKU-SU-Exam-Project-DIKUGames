using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Items.Powerups {

    public class DoubleSize : Powerup {

        private const float SIZE_SACLE = 1.3f;
        public DoubleSize(DynamicShape shape, IBaseImage image) : base(shape, image) {
            duration = 5;
        }

        public override void Apply(GameObject obj) {
            var shape = obj.Shape.AsDynamicShape();
            if (!obj.Active.Contains(this)) {          
                shape.Extent.X *= SIZE_SACLE;
                obj.Active.Add(this);
            }
            else {
                obj.Active.Remove(this);
                shape.Extent.X /= SIZE_SACLE;
            }


        }
    }
}
