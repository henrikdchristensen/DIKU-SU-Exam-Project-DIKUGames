using DIKUArcade.Entities;
using DIKUArcade.Physics;


namespace Breakout.Collision {

    public interface ICollidable {

        public DynamicShape GetShape();

        public void IsCollided(DynamicShape other, CollisionData data);

        public bool IsDestroyed();
    }
}
