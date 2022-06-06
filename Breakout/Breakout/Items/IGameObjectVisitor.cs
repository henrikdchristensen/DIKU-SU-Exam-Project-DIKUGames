using Breakout.Collision;

namespace Breakout.Items {

    /// <summary>
    /// All entities that can collide with a gameobject should implement this
    /// </summary>
    public interface IGameObjectVisitor {
        /// <summary>TODO</summary>
        /// <param name="block">TODO</param>
        /// <param name="data">TODO</param>
        public virtual void BlockCollision(CollisionHandlerData data) {
        }

        /// <summary>TODO</summary>
        /// <param name="block">TODO</param>
        /// <param name="data">TODO</param>
        public void BallCollision(CollisionHandlerData data) {
        }

        /// <summary>TODO</summary>
        /// <param name="wall">TODO</param>
        /// <param name="data">TODO</param>
        public void WallCollision(CollisionHandlerData data) {
        }

        /// <summary>TODO</summary>
        /// <param name="player">TODO</param>
        /// <param name="data">TODO</param>
        public void PlayerCollision(CollisionHandlerData data) {
        }

        /// <summary>TODO</summary>
        /// <param name="powerup">TODO</param>
        /// <param name="data">TODO</param>
        public void PowerUpCollision(CollisionHandlerData data) {
        }

        /// <summary>TODO</summary>
        /// <param name="block">TODO</param>
        /// <param name="data">TODO</param>
        public void UnbreakableCollision(CollisionHandlerData data) {
        }
    }
}
