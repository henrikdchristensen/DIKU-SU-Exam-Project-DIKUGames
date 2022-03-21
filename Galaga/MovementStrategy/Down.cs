using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    class Down : IMovementStrategy {

	    /// <summary> Move an enemy downwards. </summary>
        /// <param name = "enemy"> the enemy to move </param>
        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.MoveY(-enemy.Speed);
        }

        /// <summary> Move a list of enemies downwards. </summary>
        /// <param name = "enemies"> the  list of enemies to move </param>
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(MoveEnemy);
        }
    }
}