using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Collision;

namespace Breakout.Items {

    public class Block : Entity, IItem, ICollidable {
        public int Health {
            get; set;
        } = 10;

        private int Threshold { get; set; } = 10;

        public Block(StationaryShape shape, IBaseImage image) : base(shape, image) {
            CollisionHandler.GetInstance().Subsribe(this);
        }

        /// <summary> Should be called when the block is hit, and decrements health </summary>
        /// <returns> Returns true if it is dead, and false otherwise </returns>
        public bool Hit() {
            Health--;
            if (Health < 0)
                return true;
            return false;
        }

        public Shape GetShape() {
            return base.Shape;
        }

        DynamicShape ICollidable.GetShape() {
            return Shape.AsDynamicShape();
        }

        void ICollidable.IsCollided(DynamicShape other) {
            Hit();
        }
    }
}