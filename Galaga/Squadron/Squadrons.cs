using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;
using System.IO;


namespace Galaga.Squadron {
    class VFormation : ISquadron {

        public EntityContainer<Enemy> Enemies { get; } = new EntityContainer<Enemy>();

        private float speed {
            get; set;
        }

        public int MaxEnemies { get; } = 9;

        private int enemyCount { get; set; }

	    /// <summary> A V formation </summary>
        /// <param name = "count"> the number of enemies in the formation </param>
        /// <param name = "speed"> the speed of the enemies </param>
        /// <returns> a VFormation-instance </returns>
        public VFormation(int count, float speed) {
            this.speed = speed;
            this.enemyCount = count > MaxEnemies ? MaxEnemies : count;
        }

        /// <summary> Creates the enemies </summary>
        /// <param name = "enemyStride"> an image stride of the enemy </param>
        /// <param name = "alternativeEnemyStride"> an alternative image stride of the enemy </param>
        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            float startOffset = (MaxEnemies - enemyCount) / 2.0f * 0.1f;
            for (int i = 1; i <= enemyCount; i++) {
                float normIter = (float) i * 0.1f;
                float xPos = (float) i * 0.1f + startOffset;
                float yPos = i > enemyCount / 2 ? normIter + (0.9f - enemyCount * 0.1f) : 1 - normIter;
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
        private float speed {
            get; set;
        }

        public int MaxEnemies { get; } = 9;

        private int enemyCount { get; set; }

	    /// <summary> A Zig Zag formation </summary>
        /// <param name = "count"> the number of enemies in the formation </param>
        /// <param name = "speed"> the speed of the enemies </param>
        /// <returns> a Zigzag-instance </returns>
        public Zigzag(int count, float speed) {
            this.speed = speed;
            this.enemyCount = count > MaxEnemies ? MaxEnemies : count;
        }

        /// <summary> Creates the enemies </summary>
        /// <param name = "enemyStride"> an image stride of the enemy </param>
        /// <param name = "alternativeEnemyStride"> an alternative image stride of the enemy </param>
        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            float startOffset = (MaxEnemies - enemyCount) / 2.0f * 0.1f;
            for (int i = 1; i <= enemyCount; i++) {
                float xPos = (float) i * 0.1f + startOffset;
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

        private float speed {
            get; set;
        }

        public int MaxEnemies { get; } = 9;

        private int enemyCount { get; set; }

        /// <summary> A straight formation </summary>
        /// <param name = "count"> the number of enemies in the formation </param>
        /// <param name = "speed"> the speed of the enemies </param>
        /// <returns> a Straight-instance </returns>
        public Straight(int count, float speed) {
            this.speed = speed;
            enemyCount = count > MaxEnemies ? MaxEnemies : count;
        }

        /// <summary> Creates the enemies </summary>
        /// <param name = "enemyStride"> an image stride of the enemy </param>
        /// <param name = "alternativeEnemyStride"> an alternative image stride of the enemy </param>
        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            float startOffset = (MaxEnemies - enemyCount) / 2.0f * 0.1f;
            for (int i = 1; i <= enemyCount; i++) {
                float xPos = (float) i * 0.1f + startOffset;
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