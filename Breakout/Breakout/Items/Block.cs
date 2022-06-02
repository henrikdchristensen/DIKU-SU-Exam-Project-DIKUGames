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
        public int value { get; protected set; } = 1;

        /// <summary>TODO</summary>
        /// <param name="shape">TODO</param>
        /// <param name="image">TODO</param>
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

        /// <summary>TODO</summary>
        /// <param name="level">TODO</param>
        public override void AtDeletion() {
            DeleteEntity();
            GameBus.TriggerEvent(GameEventType.StatusEvent, "BLOCK_DESTROYED", intArg: value);
        }

        /// <summary>TODO</summary>
        /// <param name="other">TODO</param>
        /// <param name="data">TODO</param>
        public override void Accept(GameObject other, CollisionData data) {
            other.BlockCollision(this, data);
        }

        /// <summary>TODO</summary>
        /// <param name="ball">TODO</param>
        /// <param name="data">TODO</param>
        public override void BallCollision(Ball ball, CollisionData data) {
            Hit();
        }

    }

}