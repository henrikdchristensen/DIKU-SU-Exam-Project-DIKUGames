using DIKUArcade.Physics;
using Breakout.Items;

namespace Breakout.Collision {

    public class CollisionHandler {

        private List<GameObject> collidableList = new List<GameObject>();
        private static CollisionHandler instance = null;

        /// <summary>Gets a new instance</summary>
        /// <returns>Returns a CollisionHandler instance</returns>
        public static CollisionHandler GetInstance() {
            if (instance == null) {
                instance = new CollisionHandler();
            }
            return instance;
        }

        /// <summary>Add an object to the list of collidables</summary>
        /// <param name="obj">An GameObject for be added to list</param>
        public void Subsribe(GameObject obj) {
            collidableList.Add(obj);
        }

        /// <summary>Removed destroyed items from collidable list</summary>
        private void removeDestroyed() {
            List<GameObject> newList = new List<GameObject>();
            foreach (GameObject c in collidableList)
                if (!c.IsDeleted())
                    newList.Add(c);
            collidableList = newList;
        }

        /// <summary>Update list of collision items</summary>
        public void Update() {
            removeDestroyed();
            foreach (GameObject col in collidableList) {
                foreach (GameObject other in collidableList) {
                    if (col != other) {
                        CollisionData data = CollisionDetection.Aabb(col.Shape.AsDynamicShape(), other.Shape);
                        if (data.Collision) {
                            col.Accept(other, data);
                            other.Accept(col, data);
                        }
                    }
                }
            }
        }

    }

}