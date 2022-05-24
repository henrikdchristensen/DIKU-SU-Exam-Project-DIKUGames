using DIKUArcade.Entities;
using Breakout.Collision;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

namespace Breakout.Items {

    public class Wall : Entity, ICollidable {

        public Wall(StationaryShape shape) : base(shape, new NoImage()) {

        }

        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        public void Accept(ICollidable other, CollisionData data) {
            other.WallCollision(this, data);
        }

        public bool IsDestroyed() {
            return false;
        }
    }
}
