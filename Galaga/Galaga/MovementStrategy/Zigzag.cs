using DIKUArcade.Entities;
using System;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy {
    public class Zigzag : IMovementStrategy {
        private const float PERIOD = 0.045f;
        private const float AMPLITUDE = 0.05f;

        /// <summary> Move an enemy zig zag. </summary>
        /// <param name = "enemy"> The enemy to move </param>
        public void MoveEnemy(Enemy enemy) {
            Vec2F initialPos = enemy.InitialPos;
            float y_i = enemy.Shape.Position.Y - enemy.Speed;
            float x_i = initialPos.X + (float)(AMPLITUDE * Math.Sin((2.0f * Math.PI * (initialPos.Y-y_i)) / PERIOD));
            
            enemy.Shape.SetPosition(new Vec2F(x_i, y_i));
        }

        /// <summary> Move a list of enemies zig zag. </summary>
        /// <param name = "enemy"> The enemies to move </param>
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(MoveEnemy);
        }
    }
}