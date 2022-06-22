using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout.Game.States;

namespace BreakoutTests.GameTest.States {

    [TestFixture]
    public class GameRunningTest {

        private GameRunning instance;

        /// <summary>Initialize statemachine and eventbus instances</summary>
        [SetUp]
        public void InitializeTest() {
            Window.CreateOpenGLContext();
            instance = GameRunning.GetInstance();
            instance.ResetState(); // neccessary for testing state og GamePaused, since GamePaused is a singleton
            instance.UpdateState();
            instance.RenderState();
        }

        /// <summary>
        /// </summary>
        [TestCase(KeyboardAction.KeyPress, KeyboardKey.Escape)]
        [TestCase(KeyboardAction.KeyPress, KeyboardKey.Left)]
        [TestCase(KeyboardAction.KeyPress, KeyboardKey.Right)]
        [TestCase(KeyboardAction.KeyRelease, KeyboardKey.Left)]
        [TestCase(KeyboardAction.KeyRelease, KeyboardKey.Right)]
        public void HandleKeyEventTest(KeyboardAction action, KeyboardKey key) {
            instance.HandleKeyEvent(action, key);
        }

    }

}