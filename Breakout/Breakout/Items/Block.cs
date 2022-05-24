using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Collision;
using DIKUArcade.Physics;
using Breakout.Game;
using DIKUArcade.Events;
using Breakout.Levels;

namespace Breakout.Items {

    public class Block : Item, ICollidable {


        public int StartHealt { get; protected set; } = 1;

        public int Health { get; protected set; }

        public int value { get; protected set; } = 1;

        public Block(StationaryShape shape, IBaseImage image) : base(shape, image) {
            Health = StartHealt;
        }

        /// <summary> Should be called when the block is hit, and decrements health </summary>
        /// <returns> Returns true if it is dead, and false otherwise </returns>
        virtual public void Hit() {
            Health--;
            if (Health <= 0)
                DeleteEntity();
        }

        public override void Die(Level level) {
            GameBus.GetBus().RegisterEvent(new GameEvent {
                Message = "BLOCK_DESTROYED",
                EventType = GameEventType.StatusEvent,
                IntArg1 = value
            });
        }

        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        public void Accept(ICollidable other, CollisionData data) {
            other.BlockCollision(this, data);
        }

        public void BallCollision(Ball ball, CollisionData data) {
            Console.WriteLine("BLOCK HIT");
            Hit();
        }

        public bool IsDestroyed() {
            return IsDeleted();
        }
    }
}