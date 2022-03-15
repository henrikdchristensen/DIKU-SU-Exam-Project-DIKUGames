using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga{
    
    public class Enemy : Entity{
        private int hitpoints = 5;
        private const int THRESHOLD = 3;
        private IBaseImage enemyStridesRed;

        public float Speed {get; private set;}

        public Vec2F InitialPos;
        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enemyStridesRed, Vec2F initialPos, float speed )
        : base(shape, image) { 
            this.enemyStridesRed = enemyStridesRed;
            this.InitialPos = initialPos;
            this.Speed = speed;
        }

        //Should be called when the enemy is hit. Returns true if it is dead, and false otherwise
        public bool Hit() {
            hitpoints--;
            if (hitpoints < 0) 
                return true;
            else if (hitpoints + 1 == THRESHOLD) {
                Image = enemyStridesRed;
                Speed *= 2;
            }
            return false;
        }
    }

}