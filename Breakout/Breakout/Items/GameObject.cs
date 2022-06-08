using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Levels;
using Breakout.Items.Powerups;
using Breakout.Collision;

namespace Breakout.Items {

    /// <summary>
    /// All entities / objects that should appear in the game (not GUI), should inherit from this class
    /// </summary>
    /// <remarks>
    /// Implements IGameObjectVisitor, since all gameobjects can collide with each other
    /// </remarks>
    public abstract class GameObject : Entity, IGameObjectVisitor {
        public bool IsDestroyable { get;  protected set; } = false;
        public GameObject(Shape shape, IBaseImage image) : base(shape, image) { }

        /// <summary>Defines what happens when deleting the item</summary>
        public virtual void OnDeletion() { }

        /// <summary>Defines what happens when updating the item</summary>
        public virtual void Update() { }

        /// <summary>Accepting a visitor and calls their collision method on them</summary>
        /// <param name="other">The visitor that has collided with this entity</param>
        /// <param name="data">Data containing info about the collision</param>
        public abstract void Accept(IGameObjectVisitor other, CollisionHandlerData data);


        /// <summary>When colliding with a block</summary>
        /// <param name="data">Data containing info about the collision</param>
        public virtual void BlockCollision(CollisionHandlerData data) {
        }

        /// <summary>When colliding with a ball</summary>
        /// <param name="data">Data containing info about the collision</param>
        public virtual void BallCollision(CollisionHandlerData data) {
        }

        /// <summary>When colliding with a wall</summary>
        /// <param name="data">Data containing info about the collision</param>
        public virtual void WallCollision(CollisionHandlerData data) {
        }

        /// <summary>When colliding with a player</summary>
        /// <param name="data">Data containing info about the collision</param>
        public virtual void PlayerCollision(CollisionHandlerData data) {
        }

        /// <summary>When colliding with a powerup</summary>
        /// <param name="data">Data containing info about the collision</param>
        public virtual void PowerUpCollision(CollisionHandlerData data) {
        }

        /// <summary>When colliding with a unbreakable block</summary>
        /// <param name="data">Data containing info about the collision</param>
        public virtual void UnbreakableCollision(CollisionHandlerData data) {
        }


    }
}
