using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga{
    
    public class Enemy : Entity{
        private int hitpoints = 5;
        private const int THRESHOLD = 3;
        private IBaseImage enemyStridesRed;
        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enemyStridesRed)
        : base(shape, image) { 
            this.enemyStridesRed = enemyStridesRed;
        }

        public void Hit() {
            hitpoints--;
            if (hitpoints < 0) 
                DeleteEntity();
            else if (hitpoints < THRESHOLD) {
                Image = enemyStridesRed;
                //TODO: speed should increase
            }
            
        }
    }

}