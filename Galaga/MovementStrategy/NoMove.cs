using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    class NoMove : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) { }
        public void MoveEnemies(EntityContainer<Enemy> enemies) { }
    }
}