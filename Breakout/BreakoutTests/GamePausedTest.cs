using NUnit.Framework;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using Breakout.Game;
using Breakout.Game.States;

namespace BreakoutTests {

    [TestFixture]
    public class GamePausedTest {

        private GamePaused instance;

        /// <summary>Initialize statemachine and eventbus instances</summary>
        [SetUp]
        public void InitializeTest() {
            instance = GamePaused.GetInstance();
            instance.ResetState(); // neccessary for testing state og GamePaused, since GamePaused is a singleton
        }

        /// <summary>
        /// Black- and Whitebox (C0, C1): Test each branch and statement in HandleKeyEvents
        /// Case:           Expected output:        Comment:
        /// Up, Up          activeMenuButton=0      Boundary test (min)
        /// Down, Up        activeMenuButton=0      Back to starting point
        /// Down, Down      activeMenuButton=1      Boundary test (max)
        /// Escape, Enter   activeMenuButton=0      ChangeState to GameRunning
        /// Down, Enter     activeMenuButton=1      ChangeState to MainMenu
        /// </summary>
        [TestCase(KeyboardKey.Up, KeyboardKey.Up, 0)]
        [TestCase(KeyboardKey.Down, KeyboardKey.Up, 0)]
        [TestCase(KeyboardKey.Down, KeyboardKey.Down, 1)]
        [TestCase(KeyboardKey.Escape, KeyboardKey.Enter, 0)]
        [TestCase(KeyboardKey.Down, KeyboardKey.Enter, 1)]
        [TestCase(KeyboardKey.Enter, KeyboardKey.Enter, 0)]
        public void HandleKeyEventTest(KeyboardKey key1, KeyboardKey key2, int expected) {
            instance.HandleKeyEvent(KeyboardAction.KeyPress, key1);
            instance.HandleKeyEvent(KeyboardAction.KeyPress, key2);
            Assert.AreEqual(expected, instance.activeMenuButton);
        }

    }

}