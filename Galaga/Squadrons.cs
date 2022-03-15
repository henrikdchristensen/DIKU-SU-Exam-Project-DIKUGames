using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;
using System.IO;


namespace Galaga.Squadron {
    class VFormation : ISquadron {

        public EntityContainer<Enemy> Enemies { get; } = new();

        private float speed { get; set; }

        public int MaxEnemies { get; } = 9;

        public VFormation(int max, float speed) {
            this.speed = speed;
            MaxEnemies = max > MaxEnemies ? MaxEnemies : max;
        }

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            for (int i = 1; i <= MaxEnemies; i++) {
                float xPos = (float) i * 0.1f;
                float yPos = i > MaxEnemies / 2 ? xPos + (0.9f - MaxEnemies * 0.1f) : 1 - xPos;
                var pos = new Vec2F(xPos, yPos);
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(pos, new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(80, alternativeEnemyStride),
                    pos,
                    speed
                ));
            }
        }
    }

    class Zigzag : ISquadron {
        public EntityContainer<Enemy> Enemies { get; } = new();

        //private const float SPEED = 0.006f;
        private float speed { get; set; }

        public int MaxEnemies { get; } = 9;

        public Zigzag(int max, float speed) {
            this.speed = speed;
            MaxEnemies = max > MaxEnemies ? MaxEnemies : max;
        }

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            for (int i = 1; i <= MaxEnemies; i++) {
                float xPos = (float) i * 0.1f;
                float yPos = 0.8f + (i % 2) * 0.1f;
                var pos = new Vec2F(xPos, yPos);

                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(xPos, yPos), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(80, alternativeEnemyStride),
                    pos,
                    speed
                ));
            }
        }
    }

    class Straight : ISquadron {
        public EntityContainer<Enemy> Enemies { get; } = new();

        private float speed { get; set; }

        public int MaxEnemies { get; } = 9;

        public Straight(int max, float speed) {
            this.speed = speed;
            MaxEnemies = max > MaxEnemies ? MaxEnemies : max;
        }

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            for (int i = 1; i <= MaxEnemies; i++) {
                float xPos = (float) i * 0.1f;
                float yPos = 0.9f;
                var pos = new Vec2F(xPos, yPos);

                Enemies.AddEntity(new Enemy(
                    new DynamicShape(pos, new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(80, alternativeEnemyStride),
                    pos,
                    speed
                ));
            }
        }
    }

}