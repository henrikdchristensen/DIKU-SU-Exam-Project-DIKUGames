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
            //zigzag = new Galaga.Squadron.Zigzag(9, 0.05f);
            //vformation = new VFormation(9, 0.05f);

            straightFormation.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            enemy = new Enemy()
            //straightFormation.Enemies;
            // zigzag.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            // vformation.CreateEnemies(enemyStridesBlue, enemyStridesRed);

            // squadronList = new List<ISquadron> {
            //    new Straight(9, 0.05f),
            //    new Galaga.Squadron.Zigzag(9, 0.05f),
            //    new VFormation(9, 0.05f)
            // };

        }

        [Test]
        public void TestNoMove() { //Nothing should happen at update, when noMove is used
            //squadronList[0].CreateEnemies(enemyStridesBlue, enemyStridesRed);


        }

        [Test]
        public void TestDown() { //Is enemy's position decremented with enemy.speed, when move strategy down is used
            for (int i = 0; i < 10; i++) {
                movementStrategyList[0].MoveEnemies(enemies);
            }
            foreach (Enemy e in enemies) {
                var expectedPos = e.InitialPos.Y - speed * 10;
                Assert.True( expectedPos >= expectedPos-diff && expectedPos <= expectedPos+diff, $"Expected pos: {expectedPos} New pos {e.Shape.Position.Y}");
            }
        }

        [Test]
        public void TestZigZag() { //Is enemy's position set correct when move strategy zisag is used
                                   //squadronList[0].CreateEnemies(enemyStridesBlue, enemyStridesRed);

            const float PERIOD = 0.045f;
            const float AMPLITUDE = 0.05f;
            float expected_y_i;
            float expected_x_i;

            for (int i = 0; i < 10; i++) {
                //var enemy = enemies.
                movementStrategyList[2].MoveEnemy(enemies);
                y_i = enemy[].Shape.Position.Y - enemy.Speed;
                x_i = initialPos.X + (float)(AMPLITUDE * Math.Sin((2.0f * Math.PI * (initialPos.Y-y_i)) / PERIOD));
            }

            foreach (Enemy enemy in enemies) {
                Assert.True( 
                expected_x_i >= expected_x_i-diff && expected_x_i <= expected_x_i+diff &&
                expected_y_i >= expected_y_i-diff && expected_y_i <= expected_y_i+diff
                , $"Expected pos: {expected_x_i} New pos {e.Shape.Position.X} AND Expected pos: {expected_y_i} New pos {e.Shape.Position.Y}");
            }

        }

    }
}
