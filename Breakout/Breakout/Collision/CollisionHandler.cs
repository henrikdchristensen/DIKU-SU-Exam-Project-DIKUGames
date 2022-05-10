using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Collision;

namespace Breakout.Collision {

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

        private void RemoveDestroyed() {
            List<ICollidable> newList = new List<ICollidable>();
            foreach (ICollidable c in collidableList)
                if (!c.IsDestroyed())
                    newList.Add(c);
            collidableList = newList;
        }

        public void Update() {
            RemoveDestroyed();

            foreach (ICollidable col in collidableList) {
                foreach (ICollidable other in collidableList) {
                    if (col != other) {
                        CollisionData data = CollisionDetection.Aabb(col.GetShape(), other.GetShape());
                        if (data.Collision) {
                            col.AtCollision(other.GetShape(), data);
                            other.AtCollision   (col.GetShape(), data);
                        }
                    }
                }
            }
        }
    }
}