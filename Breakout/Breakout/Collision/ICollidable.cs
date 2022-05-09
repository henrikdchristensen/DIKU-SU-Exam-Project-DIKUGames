using DIKUArcade.Entities;
using DIKUArcade.Physics;


namespace Breakout.Collision {

    public interface ICollidable {

        public DynamicShape GetShape();

        public void AtCollision(DynamicShape other, CollisionData data);

        public bool IsDestroyed();
    }
}
