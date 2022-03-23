using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    public class NoMove : IMovementStrategy {

        /// <summary> Move an enemy. </summary>
        /// <remarks> no implementation since the enemy should not move </remarks>
        /// <param name = "enemy"> the enemy to move </param>
        public void MoveEnemy(Enemy enemy) { }

        /// <summary> Move an enemy. </summary>
        /// <remarks> No implementation since the enemies should not move </remarks>
        /// <param name = "EntityContainer"> the list of enemies to move </param>
        public void MoveEnemies(EntityContainer<Enemy> enemies) { }
    }
}