using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

namespace Breakout.Items {

    public class Wall : GameObject {

        /// <summary>TODO</summary>
        /// <param name="shape">TODO</param>
        public Wall(StationaryShape shape) : base(shape, new NoImage()) {

        }

        /// <summary>TODO</summary>
        /// <returns>TODO</returns>
        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        /// <summary>TODO</summary>
        /// <param name="other">TODO</param>
        /// <param name="data">TODO</param>
        public override void Accept(GameObject other, CollisionData data) {
            other.WallCollision(this, data);
        }

    }

}
