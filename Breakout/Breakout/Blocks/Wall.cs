using DIKUArcade.Entities;
using Breakout.Collision;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    class Wall : Entity, ICollidable {

        public Wall(StationaryShape shape, float rotation) : base(shape, new NoImage()) {
            shape.SetRotation(rotation);
        }

        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        public void IsCollided(DynamicShape other) {

        }
    }
}
