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

namespace GalagaTests {

    [TestFixture]
    public class TestSquadron {

        private float diff = 0.002f;


        [SetUp]
        public void InitializeTest() {

        }

        [Test]
        public void TestZigZag() { //TODO
        }

        [Test]
        public void TestVFormation() { //TODO
        }

        [Test]
        public void TestStraight() { //TODO

            float speed = 0.005f;
            int enemyCount = 5;
            ISquadron squadron = new Straight(enemyCount, speed);
            int MaxEnemies = squadron.MaxEnemies;
            float startOffset = (MaxEnemies - enemyCount) / 2.0f * 0.1f;

            var enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            var enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));


            squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);

            //for (int i = 1; i <= squadron.Enemies.CountEntities(); i++) {
            int i = 1;
            foreach (Enemy enemy in squadron.Enemies) {
                // float xPos = (float) i * 0.1f + startOffset;
                // float yPos = 0.8f + (i % 2) * 0.1f;
                float xPos = (float) i * 0.1f + startOffset;
                float yPos = 0.9f;
                var expectedPos = new Vec2F(xPos, yPos);
                i++;
                Assert.True(expectedPos >= enemy.Shape.Position && expectedPos <= , $"Expected {expectedPos} Actual pos {enemy.Shape.Position}");
            }

        }

    }
}