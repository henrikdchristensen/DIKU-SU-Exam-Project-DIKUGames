using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

namespace Breakout.Items {

    public class Wall : GameObject {

        /// <summary>Constructor of Wall</summary>
        /// <param name="shape">StationaryShape of the wall</param>
        public Wall(StationaryShape shape) : base(shape, new NoImage()) {

        }

        /// <summary>Gets the shape of the wall</summary>
        /// <returns>Shape of the Wall</returns>
        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        /// <summary>
        /// Accepts another GameObject if a collision has occured with another GameObject,
        /// and put the Wall's instance itself into the other one.
        /// (Visitor Pattern)
        /// </summary>
        /// <param name="other">The other GameObject</param>
        /// <param name="data">Collision data</param>
        public override void Accept(GameObject other, CollisionData data) {
            other.WallCollision(this, data);
        }

    }

}
