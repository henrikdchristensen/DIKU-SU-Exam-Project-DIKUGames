using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Items {

    public class Block : Entity, IItem {
        public int Health {
            get; set;
        }
        private int value {
            get; set;
        }

        public Block(StationaryShape shape, IBaseImage image, int health, int value) : base(shape, image) {
            this.Health = health;
            this.value = value;
        }

        /// <summary> Should be called when the block is hit, and decrements health </summary>
        /// <returns> Returns true if it is dead, and false otherwise </returns>
        public bool Hit() {
            Health--;
            if (Health < 0)
                return true;
            return false;
        }
    }
}