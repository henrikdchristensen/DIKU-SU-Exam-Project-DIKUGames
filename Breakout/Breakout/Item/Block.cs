using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Collision;
using DIKUArcade.Physics;

namespace Breakout.Items {

    public class Block : Entity, ICollidable {


        public int StartHealt { get; set; } = 1;

        public int Health { get; set; }

        public Block(StationaryShape shape, IBaseImage image) : base(shape, image) {
            CollisionHandler.GetInstance().Subsribe(this);
            Health = StartHealt;
        }

        /// <summary> Should be called when the block is hit, and decrements health </summary>
        /// <returns> Returns true if it is dead, and false otherwise </returns>
        virtual public bool Hit() {
            Health--;
            if (Health <= 0) {
                DeleteEntity();
                return true;
            }
            return false;
        }


        public Shape GetShape() {
            return base.Shape;
        }

        DynamicShape ICollidable.GetShape() {
            return Shape.AsDynamicShape();
        }

        void ICollidable.IsCollided(DynamicShape other, CollisionData data) {
            Hit();
        }

        public bool IsDestroyed() {
            return IsDeleted();
        }
    }
}