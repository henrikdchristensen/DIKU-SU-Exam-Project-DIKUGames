using NUnit.Framework;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Levels;
using Breakout.Game;
using Breakout.Entities.Powerups;
using System;

namespace BreakoutTests.poweruptests {

    [TestFixture]
    public class PowerUpIntegrationTest {

        [OneTimeSetUp]
        public void Setup() {}

        [Test]
        public void PowerUpSpawnTest() {
            Window.CreateOpenGLContext();
            Level level = new Level(new char[,] { }, new Dictionary<string, string>(), new Dictionary<string, string>());
            PowerupBlock block = new PowerupBlock(new StationaryShape(0, 0, 0, 0), new NoImage());
            int initItems = level.CountItems();
            GameBus.GetBus().Subscribe(GameEventType.ControlEvent, level);

            block.OnDeletion();
            GameBus.GetBus().ProcessEventsSequentially();

            int expected = initItems + 1;
            Assert.AreEqual(expected, level.CountItems());

        }

    }
}
