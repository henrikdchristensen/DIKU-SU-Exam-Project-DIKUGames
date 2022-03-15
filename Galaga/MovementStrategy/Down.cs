using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    class Down : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.MoveY(-enemy.Speed);
        }
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(MoveEnemy);
        }
    }
}