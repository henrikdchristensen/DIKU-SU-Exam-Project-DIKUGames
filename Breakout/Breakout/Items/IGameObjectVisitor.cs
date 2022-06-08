using Breakout.Collision;

namespace Breakout.Entities {

    /// <summary>
    /// All entities that can collide with a gameobject should implement this
    /// </summary>
    public interface IGameObjectVisitor {

        /// <summary>When colliding with a block</summary>
        /// <param name="data">Data containing info about the collision</param>
        public virtual void BlockCollision(CollisionHandlerData data) {
        }


        /// <summary>When colliding with a ball</summary>
        /// <param name="data">Data containing info about the collision</param>
        public void BallCollision(CollisionHandlerData data) {
        }


        /// <summary>When colliding with a wall</summary>
        /// <param name="data">Data containing info about the collision</param>
        public void WallCollision(CollisionHandlerData data) {
        }

        /// <summary>When colliding with a player</summary>
        /// <param name="data">Data containing info about the collision</param>
        public void PlayerCollision(CollisionHandlerData data) {
        }

        /// <summary>When colliding with a powerup</summary>
        /// <param name="data">Data containing info about the collision</param>
        public void PowerUpCollision(CollisionHandlerData data) {
        }


        /// <summary>When colliding with an unbreakable</summary>
        /// <param name="data">Data containing info about the collision</param>
        public void UnbreakableCollision(CollisionHandlerData data) {
        }
    }
}
