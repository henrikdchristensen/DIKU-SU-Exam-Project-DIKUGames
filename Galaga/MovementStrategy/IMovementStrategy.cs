using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    /// <summary> Interface for an enemy's movement strategy </summary>
    public interface IMovementStrategy {
        void MoveEnemy(Enemy enemy);
        void MoveEnemies(EntityContainer<Enemy> enemies);
    }
}