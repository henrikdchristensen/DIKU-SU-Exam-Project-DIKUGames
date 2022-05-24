using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Collision;

namespace Breakout.Collision {

    public class CollisionHandler {

        private static CollisionHandler instance = null;

        /// <summary>
        /// Gets a new instance of the CollisionHandler class
        /// </summary>
        /// <returns>Return a CollisionHandler instance</returns>
        public static CollisionHandler GetInstance() {
            if (instance == null) {
                instance = new CollisionHandler();
            }
            return instance;
        }

        private List<ICollidable> collidableList = new List<ICollidable>();

        /// <summary>
        /// Add an object to the list of collidables
        /// </summary>
        /// <param name="obj"></param>
        public void Subsribe(ICollidable obj) {
            collidableList.Add(obj);
        }

        /// <summary>
        /// Removed destroyed items from collidable list
        /// </summary>
        private void RemoveDestroyed() {
            List<ICollidable> newList = new List<ICollidable>();
            foreach (ICollidable c in collidableList)
                if (!c.IsDestroyed())
                    newList.Add(c);
            collidableList = newList;
        }

        /// <summary>
        /// Update list of collision items
        /// </summary>
        public void Update() {
            RemoveDestroyed();

            foreach (ICollidable col in collidableList) {
                foreach (ICollidable other in collidableList) {
                    if (col != other) {
                        CollisionData data = CollisionDetection.Aabb(col.GetShape(), other.GetShape());
                        if (data.Collision) {
                            col.Accept(other, data);
                            other.Accept   (col, data);
                        }
                    }
                }
            }
        }
    }
}