using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Collision;

namespace Breakout.Collision;
public class CollisionHandler {
    List<ICollidable> CollidableList;
    public void Update() {
        foreach (ICollidable dynamicCollidable in CollidableList) {
            foreach (ICollidable other in CollidableList) {
                if (dynamicCollidable != other) {
                    CollisionData data = CollisionDetection.Aabb(dynamicCollidable.GetShape(), other.GetShape());
                    if(data.Collision) {
                        dynamicCollidable.IsCollided(other.GetShape());
                    }
                }
            }
        }
    }
}