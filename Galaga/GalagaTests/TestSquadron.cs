using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
using Galaga.Squadron;
using System;

namespace GalagaTests {

    [TestFixture]
    public class TestSquadron {

        private const float COMPARE_DIFF = 10e-5f;

        [SetUp]
        public void InitializeTest() {
           Window.CreateOpenGLContext();
        }

        [Test]
        public void TestZigZag() { //TODO
            List<Vec2F> expected = new List<Vec2F> { new Vec2F(0.3f, 0.9f), new Vec2F(0.4f, 0.8f), new Vec2F(0.5f, 0.9f), new Vec2F(0.6f, 0.8f), new Vec2F(0.7f, 0.9f) };

            int enemyCount = 5;
            float speed = 0.005f;
            ISquadron squadron = new Zigzag(enemyCount, speed);
            float startOffset = (squadron.MaxEnemies - enemyCount) / 2.0f * 0.1f;

            var enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            var enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));


            squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);

            int i = 0;
            foreach (Enemy enemy in squadron.Enemies) {
                Vec2F expectedPos = expected[i++];
                Vec2F diff = expectedPos - enemy.Shape.Position;
                Assert.True(Math.Abs(diff.X) < COMPARE_DIFF && Math.Abs(diff.Y) < COMPARE_DIFF);
            }
        }

        [Test]
        public void TestVFormation() { //TODO
            List<Vec2F> expected = new List<Vec2F> { new Vec2F(0.3f, 0.9f), new Vec2F(0.4f, 0.8f), new Vec2F(0.5f, 0.7f), new Vec2F(0.6f, 0.8f), new Vec2F(0.7f, 0.9f) };

            int enemyCount = 5;
            float speed = 0.005f;
            ISquadron squadron = new VFormation(enemyCount, speed);

            var enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            var enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));

            squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            
            int i = 0;
            foreach (Enemy enemy in squadron.Enemies) {
                Vec2F expectedPos = expected[i++];
                Vec2F diff = expectedPos - enemy.Shape.Position;
                Assert.True(Math.Abs(diff.X) < COMPARE_DIFF && Math.Abs(diff.Y) < COMPARE_DIFF, $"{diff}");
            }
        }

        [Test]
        public void TestStraight() { //TODO

            float speed = 0.005f;
            int enemyCount = 5;
            ISquadron squadron = new Straight(enemyCount, speed);
            float startOffset = (squadron.MaxEnemies - enemyCount) / 2.0f * 0.1f;

            var enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            var enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));


            squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);

            int i = 1;
            foreach (Enemy enemy in squadron.Enemies) {
                float xPos = (float) i++ * 0.1f + startOffset;
                float yPos = 0.9f;
                Vec2F expectedPos = new Vec2F(xPos, yPos);
                Vec2F diff = expectedPos - enemy.Shape.Position;
                Assert.True(Math.Abs(diff.X) < COMPARE_DIFF && Math.Abs(diff.Y) < COMPARE_DIFF);
            }

        }

    }
}