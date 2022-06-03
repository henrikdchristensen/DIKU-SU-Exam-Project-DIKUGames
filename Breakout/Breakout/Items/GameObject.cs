using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Levels;
using Breakout.Items.Powerups;
using Breakout.Collision;

namespace Breakout.Items {

    public abstract class GameObject : Entity {
        public bool IsDestroyable { get;  protected set; } = false;
        public GameObject(Shape shape, IBaseImage image) : base(shape, image) { }

        /// <summary>TODO</summary>
        /// <param name="level">TODO</param>
        public virtual void AtDeletion() { }

        /// <summary>TODO</summary>
        public virtual void Update() { }

        /// <summary>TODO</summary>
        /// <param name="other">TODO</param>
        /// <param name="data">TODO</param>
        public abstract void Accept(GameObject other, CollisionHandlerData data);

        /// <summary>TODO</summary>
        /// <param name="block">TODO</param>
        /// <param name="data">TODO</param>
        public virtual void BlockCollision(CollisionHandlerData data) { }

        /// <summary>TODO</summary>
        /// <param name="block">TODO</param>
        /// <param name="data">TODO</param>
        public virtual void BallCollision(CollisionHandlerData data) { }

        /// <summary>TODO</summary>
        /// <param name="wall">TODO</param>
        /// <param name="data">TODO</param>
        public virtual void WallCollision(CollisionHandlerData data) { }

        /// <summary>TODO</summary>
        /// <param name="player">TODO</param>
        /// <param name="data">TODO</param>
        public virtual void PlayerCollision(CollisionHandlerData data) { }

        /// <summary>TODO</summary>
        /// <param name="powerup">TODO</param>
        /// <param name="data">TODO</param>
        public virtual void PowerUpCollision(CollisionHandlerData data) { }

        /// <summary>TODO</summary>
        /// <param name="block">TODO</param>
        /// <param name="data">TODO</param>
        public virtual void UnbreakableCollision(CollisionHandlerData data) { }

    }
}
