using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout.Game.States;

namespace BreakoutTests {

    [TestFixture]
    public class MainMenuTest {

        private MainMenu instance;

        /// <summary>Initialize statemachine and eventbus instances</summary>
        [SetUp]
        public void InitializeTest() {
            Window.CreateOpenGLContext();
            instance = MainMenu.GetInstance();
            instance.ResetState(); // neccessary for testing state og GamePaused, since GamePaused is a singleton
            instance.UpdateState();
            instance.RenderState();
        }

        /// <summary>
        /// </summary>
        [TestCase(KeyboardKey.Down, KeyboardKey.Enter)]
        [TestCase(KeyboardKey.Down, KeyboardKey.Up)]
        [TestCase(KeyboardKey.Enter, KeyboardKey.Enter)]
        public void HandleKeyEventTest(KeyboardKey key1, KeyboardKey key2) {
            instance.HandleKeyEvent(KeyboardAction.KeyPress, key1);
            instance.HandleKeyEvent(KeyboardAction.KeyPress, key2);
        }

    }

}