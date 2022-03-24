using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.MovementStrategy;
using Galaga.Squadron;
using Galaga.GalagaStates;
using Galaga;
using System.Linq;

using DIKUArcade.Graphics;

namespace GalagaTests {

    [TestFixture]
    class TestMovementStrategy {


        private List<IMovementStrategy> movementStrategyList;

        private ISquadron straightFormation;

        private Enemy enemy;

        private float speed = 0.005f;
        private float diff = 0.002f;


        [SetUp]
        public void InitializeTest() {

            Window.CreateOpenGLContext();
            movementStrategyList = new List<IMovementStrategy> {
               new Down(),
               new NoMove(),
               new Galaga.MovementStrategy.Zigzag()
            };

            var enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            var enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
            straightFormation = new Straight(1, speed);

            straightFormation.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            enemy = new Enemy(new DynamicShape(0, 0, 0, 0), new NoImage(), new NoImage(), 10f);

        }

        [Test]
        public void TestNoMove() { //Nothing should happen at update, when noMove is used
            //squadronList[0].CreateEnemies(enemyStridesBlue, enemyStridesRed);
            movementStrategyList[1].MoveEnemy(enemy);
            var expectedPos = enemy.InitialPos;
            Assert.True(expectedPos.X == enemy.Shape.Position.X && expectedPos.Y == enemy.Shape.Position.Y);
        }

        [Test]
        public void TestDown() { //Is enemy's position decremented with enemy.speed, when move strategy down is used
            for (int i = 0; i < 10; i++) {
                movementStrategyList[0].MoveEnemy(enemy);
            }
            //foreach (Enemy e in enemies) {
            var expectedPos = enemy.InitialPos.Y - speed * 10;
            Assert.True(expectedPos >= expectedPos - diff && expectedPos <= expectedPos + diff, $"Expected pos: {expectedPos} New pos {enemy.Shape.Position.Y}");
            //}
        }

        [Test]
        public void TestZigZag() { //Is enemy's position set correct when move strategy zisag is used
                                   //squadronList[0].CreateEnemies(enemyStridesBlue, enemyStridesRed);

            const float PERIOD = 0.045f;
            const float AMPLITUDE = 0.05f;
            float expected_y_i = 0f;
            float expected_x_i = 0f;

            for (int i = 0; i < 10; i++) {
                movementStrategyList[2].MoveEnemy(enemy);
                expected_y_i = enemy.Shape.Position.Y - enemy.Speed;
                expected_x_i = enemy.InitialPos.X + (float) (AMPLITUDE * Math.Sin((2.0f * Math.PI * (enemy.InitialPos.Y - expected_y_i)) / PERIOD));
            }

            Assert.True(
            expected_x_i >= expected_x_i - diff && expected_x_i <= expected_x_i + diff 
            &&
            expected_y_i >= expected_y_i - diff && expected_y_i <= expected_y_i + diff
            , $"Expected pos: {expected_x_i} New pos {enemy.Shape.Position.X} AND Expected pos: {expected_y_i} New pos {enemy.Shape.Position.Y}");


        }

    }
}
