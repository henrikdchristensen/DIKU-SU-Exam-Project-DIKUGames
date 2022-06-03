﻿using NUnit.Framework;
using Breakout.Items.Powerups;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Levels;
using System.Collections.Generic;
using Breakout.Game;
using DIKUArcade.Events;
using DIKUArcade.GUI;

namespace BreakoutTests.poweruptests {

    [TestFixture]
    public class PowerUpIntegrationTest {

        [Test]
        public void PowerUpSpawnTest() {
            Window.CreateOpenGLContext();
            GameBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.ControlEvent });
            Level level = new Level(new char[,] { }, new Dictionary<string, string>(), new Dictionary<string, string>());
            PowerupBlock block = new PowerupBlock(new StationaryShape(0, 0, 0, 0), new NoImage());
            int initItems = level.CountItems();
            GameBus.GetBus().Subscribe(GameEventType.ControlEvent, level);

            block.AtDeletion();
            GameBus.GetBus().ProcessEventsSequentially();

            int expected = initItems + 1;
            Assert.AreEqual(expected, level.CountItems());

        }

    }
}
