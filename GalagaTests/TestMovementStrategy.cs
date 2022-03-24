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

using DIKUArcade.Graphics;

namespace GalagaTests {

    [TestFixture]
    class TestMovementStrategy {

        // props
        private List<ISquadron> squadronList;

        private List<IMovementStrategy> movementStrategyList;

        private List<Image> enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("", "", "Assets", "Images", "BlueMonster.png"));

        private List<Image> enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));

        private ISquadron straightFormation;
        private ISquadron zigzag;
        private ISquadron vformation;

        private EntityContainer<Enemy> enemies;

        //private GameRunning gameRunning = new();

        private float speed;


        [SetUp]
        public void InitializeTest() {

            //Window.CreateOpenGLContext();
            //movementStrategyList = new List<IMovementStrategy> {
            //    new Down(),
            //    new NoMove(),
            //    new Galaga.MovementStrategy.Zigzag()
            //};


            //speed = gameRunning.GetSpeed();
            //straightFormation = new Straight(1, speed);
            ////zigzag = new Galaga.Squadron.Zigzag(9, 0.05f);
            ////vformation = new VFormation(9, 0.05f);

            //straightFormation.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            //enemies = straightFormation.Enemies;
            //zigzag.CreateEnemies(enemyStridesBlue, enemyStridesRed);
            //vformation.CreateEnemies(enemyStridesBlue, enemyStridesRed);

            // enemies = squadron.Enemies;

            // enemies = squadron.Enemies;
            //squadronList = new List<ISquadron> {
            //    new Straight(9, 0.05f),
            //    new Galaga.Squadron.Zigzag(9, 0.05f f),
            //    new VFormation(9, 0.05f)
            //};

        }

        [Test]
        public void TestNoMove() { //Nothing should happen at update, when noMove is used
            //squadronList[0].CreateEnemies(enemyStridesBlue, enemyStridesRed);


        }

        [Test]
        public void TestDown() { //Is enemy's position decremented with enemy.speed, when move strategy down is used

            //for (int i = 0; i < 10; i++) {
            //    movementStrategyList[0].MoveEnemies(enemies);
            //}

            //foreach (Enemy e in enemies) {
            //    Assert.That(e.InitialPos.Y * 10 == e.Shape.Position.Y, $"Old pos: {e.InitialPos.Y * 10} New pos {e.Shape.Position.Y}");
            //}

        }

        [Test]
        public void TestZigZag() { //Is enemy's position set correct when move strategy zisag is used
                                   //squadronList[0].CreateEnemies(enemyStridesBlue, enemyStridesRed);


        }

    }
}
