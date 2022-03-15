using DIKUArcade.Entities;
using System;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy {
    class Zigzag : IMovementStrategy {
        private const float PERIOD = 0.045f;
        private const float AMPLITUDE = 0.05f;

        public void MoveEnemy(Enemy enemy) {
            Vec2F initialPos = enemy.InitialPos;
            float y_i = enemy.Shape.Position.Y - enemy.Speed;
            float x_i = initialPos.X + (float)(AMPLITUDE * Math.Sin((2.0f * Math.PI * (initialPos.Y-y_i)) / PERIOD));
            
            enemy.Shape.SetPosition(new Vec2F(x_i, y_i));
        }
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(MoveEnemy);
        }
    }
}