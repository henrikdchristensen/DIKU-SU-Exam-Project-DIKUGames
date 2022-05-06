using DIKUArcade.Entities;

namespace Breakout.Collision {

    interface ICollidable {

        public Shape GetShape();

        public void IsCollided(Shape other);
    }
}
