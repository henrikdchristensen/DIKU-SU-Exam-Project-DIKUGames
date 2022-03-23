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

namespace GalagaTests {

    [TestFixture]
    public class TestMovementStrategy {

        [SetUp]
        public void InitializeTest() {
        }

        [Test]
        public void TestNoMove() { //Nothing should happen at update, when noMove is used

        }

        [Test]
        public void TestDown() { //Is enemy's position decremented with enemy.speed, when move strategy down is used

        }

        [Test]
        public void TestZigZag() { //Is enemy's position set correct when move strategy zisag is used

        }


    }
}