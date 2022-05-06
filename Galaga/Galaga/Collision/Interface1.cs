using DIKUArcade.Entities;

namespace Galaga.Collision {

    interface ICollidable {

        public Shape GetShape();

        public void IsCollided(Shape other);
    }
}
