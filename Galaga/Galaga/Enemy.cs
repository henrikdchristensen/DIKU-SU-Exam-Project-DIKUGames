using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {

    public class Enemy : Entity {
        public int Hitpoints {get; private set;} = 5;

        private const int THRESHOLD = 3;

        private IBaseImage enemyStridesRed;

        public float Speed {
            get; private set;
        }

        public Vec2F InitialPos {
            get;
        }

        /// <summary> An enemy. </summary>
        /// <param name = "shape"> the shape of the enemy </param>
        /// <param name = "image"> the start-image of the enemy </param>
        /// <param name = "enemyStridesRed"> the image of the enemy, when hitpoints are below THRESHOLD </param>
        /// <param name = "initialPos"> the initial position </param>
        /// <param name = "speed"> the speed of the enemy </param>
        /// <returns> An enemy-instance</returns>
        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enemyStridesRed, float speed)
        : base(shape, image) {
            this.enemyStridesRed = enemyStridesRed;
            this.InitialPos = shape.Position.Copy();
            this.Speed = speed;
        }

        /// <summary> Should be called when the enemy is hit, and decrements hitpoints </summary>
        /// <returns> Returns true if it is dead, and false otherwise </returns>
        public bool Hit() {
            Hitpoints--;
            if (Hitpoints < 0)
                return true;
            else if (Hitpoints + 1 == THRESHOLD) {
                Image = enemyStridesRed;
                Speed *= 2;
            }
            return false;
        }
    }

}