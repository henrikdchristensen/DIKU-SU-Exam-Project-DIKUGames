using DIKUArcade.Physics;
using DIKUArcade.Math;

namespace Breakout.Collision {

    public class CollisionHandlerData {

        /// <summary>
        /// The surface normal of the incident object, indicating
        /// from which direction a collision has occured.
        /// </summary>
        public CollisionDirection CollisionDir {
            get;
        }

        /// <summary>
        /// Direction of the object, the actor has collided with
        /// </summary>
        public Vec2F Direction {
            get;
        }

        public CollisionHandlerData(CollisionDirection collisionDir, Vec2F direction) {
            CollisionDir = collisionDir;
            Direction = direction;
        }

    }
}
