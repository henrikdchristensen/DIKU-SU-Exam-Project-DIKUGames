using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using DIKUArcade.Events;
using Breakout.Game;
using Breakout.Levels;
using Breakout.Collision;

namespace Breakout.Items {

    public class Block : GameObject {

        public int StartHealt { get; protected set; } = 1;
        public int Health { get; protected set; }
        public int PointReward { get; protected set; } = 1;

        /// <summary>Constructor of Block: Setup that it is destroyable and has 1 life (normal block)</summary>
        /// <param name="shape">StationaryShape of the block</param>
        /// <param name="image">Image used for the block</param>
        public Block(StationaryShape shape, IBaseImage image) : base(shape, image) {
            IsDestroyable = true;
            Health = StartHealt;
        }

        /// <summary>Should be called when the block is hit, and decrements health</summary>
        public virtual void Hit() {
            Health--;
            if (Health <= 0)
                DeleteEntity();  
        }

        /// <summary>Deleting the block</summary>
        public override void OnDeletion() {
            DeleteEntity();
            GameBus.TriggerEvent(GameEventType.StatusEvent, "BLOCK_DESTROYED", intArg: PointReward);
        }

        /// <summary>
        /// Accpets another GameObject in case of collision and put the block instance
        /// itself into the other GameObject together with the collision data.
        /// (Visitor Pattern)
        /// </summary>
        /// <param name="other">The another GameObject</param>
        /// <param name="data">Collision data</param>
        public override void Accept(IGameObjectVisitor other, CollisionHandlerData data) {
            other.BlockCollision(data);
        }

        /// <summary>If collision with a ball has occured,then call Hit() and loose 1 life</summary>
        /// <param name="ball">Ball object</param>
        /// <param name="data">Collision data</param>
        public override void BallCollision(CollisionHandlerData data) {
            Hit();
        }

    }

}