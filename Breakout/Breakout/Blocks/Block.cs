using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Items {

    public class Block : Entity, IItem {
        public int Health { get; set; } = 10;

        private int Threshold { get; set; } = 10;

        public Block(StationaryShape shape, IBaseImage image) : base(shape, image) {

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

        public void IsCollided(Shape shape) {
            this.Hit();
        }

    }
}