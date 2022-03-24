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
            ISquadron squadron = new Straight(10, 3);
            squadron.CreateEnemies(new List<Image>(), new List<Image>());
            for (int i = 0; i < squadron.Enemies.CountEntities(); i++) {

            }
                
        }

    }
}