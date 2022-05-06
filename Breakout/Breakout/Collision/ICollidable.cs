using DIKUArcade.Entities;

namespace Breakout.Collision {

    interface ICollidable {

        public DynamicShape GetShape();

        public void IsCollided(Shape other);
    }
}
