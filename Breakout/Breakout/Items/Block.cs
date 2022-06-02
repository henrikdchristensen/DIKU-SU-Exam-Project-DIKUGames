using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using DIKUArcade.Events;
using Breakout.Game;
using Breakout.Levels;

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

        /// <summary>
        /// If the block has been marked as delete,
        /// then delete it and trigger a block destroyed event
        /// </summary>
        /// <param name="level">A level object</param>
        public override void AtDeletion(Level level) {
            DeleteEntity();
            GameBus.TriggerEvent(GameEventType.StatusEvent, "BLOCK_DESTROYED", "", PointReward);
        }

        /// <summary>
        /// Accpets another GameObject in case of collision and put the block instance
        /// itself into the other GameObject together with the collision data.
        /// (Visitor Pattern)
        /// </summary>
        /// <param name="other">The another GameObject</param>
        /// <param name="data">Collision data</param>
        public override void Accept(GameObject other, CollisionData data) {
            other.BlockCollision(this, data);
        }

        /// <summary>If collision with a ball has occured,then call Hit() and loose 1 life</summary>
        /// <param name="ball">Ball object</param>
        /// <param name="data">Collision data</param>
        public override void BallCollision(Ball ball, CollisionData data) {
            Hit();
        }

    }

}