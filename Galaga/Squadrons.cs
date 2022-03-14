using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;
using System.IO;


namespace Galaga.Squadron {
    class Squadron1 : ISquadron {

        public EntityContainer<Enemy> Enemies { get; } = new();

        public int MaxEnemies { get; } = 9;

        public Squadron1(int max) {
            MaxEnemies = max > MaxEnemies ? MaxEnemies : max;
        }

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            for (int i = 1; i <= MaxEnemies; i++) {
                float xPos = (float) i * 0.1f;
                float yPos = i > MaxEnemies / 2 ? xPos + (0.9f - MaxEnemies * 0.1f) : 1 - xPos;
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(xPos, yPos), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(80, alternativeEnemyStride)));
            }
        }
    }

    class Squadron2 : ISquadron {
        public EntityContainer<Enemy> Enemies { get; } = new();

        public int MaxEnemies { get; } = 9;

        public Squadron2(int max) {
            MaxEnemies = max > MaxEnemies ? MaxEnemies : max;
        }

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            for (int i = 1; i <= MaxEnemies; i++) {
                float xPos = (float) i * 0.1f;
                float yPos = 0.8f + (i % 2) * 0.1f;
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(xPos, yPos), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(80, alternativeEnemyStride)));
            }
        }
    }

    class Squadron3 : ISquadron {
        public EntityContainer<Enemy> Enemies { get; } = new();

        public int MaxEnemies { get; } = 9;

        public Squadron3(int max) {
            MaxEnemies = max > MaxEnemies ? MaxEnemies : max;
        }

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            for (int i = 1; i <= MaxEnemies; i++) {
                float xPos = (float) i * 0.1f;
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(xPos, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(80, alternativeEnemyStride)));
            }
        }
    }

}