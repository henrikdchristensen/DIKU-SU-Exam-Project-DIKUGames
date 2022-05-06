using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Collision;

namespace Breakout.Collision;
public class CollisionHandler {

    private static CollisionHandler instance = null;
    public static CollisionHandler GetInstance() {
        if (instance == null) {
            instance = new CollisionHandler();
        }
        return instance;
    }

    private List<ICollidable> collidableList = new List<ICollidable>();

    public void Subsribe(ICollidable obj) {
        collidableList.Add(obj);
    }

    public void Update() {
        foreach (ICollidable dynamicCollidable in collidableList) {
            foreach (ICollidable other in collidableList) {
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