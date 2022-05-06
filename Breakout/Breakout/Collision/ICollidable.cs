using DIKUArcade.Entities;

namespace Breakout.Collision {

    public interface ICollidable {

        public DynamicShape GetShape();

        public void IsCollided(DynamicShape other);
    }
}
