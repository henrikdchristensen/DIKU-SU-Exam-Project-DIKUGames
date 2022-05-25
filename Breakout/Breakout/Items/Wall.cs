using DIKUArcade.Entities;
using Breakout.Collision;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

namespace Breakout.Items {

    public class Wall : GameObject {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        public Wall(StationaryShape shape) : base(shape, new NoImage()) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="data"></param>
        public override void Accept(GameObject other, CollisionData data) {
            other.WallCollision(this, data);
        }

    }

}
