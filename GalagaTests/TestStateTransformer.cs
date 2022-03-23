using NUnit.Framework;
using Galaga;

namespace GalagaTests {

    [TestFixture]
    public class TestStateTransformer {

        [Test]
        public void TestTransformStringToState() {
            Assert.True(StateTransformer.TransformStringToState("GAME_RUNNING") == GameStateType.GameRunning);
            Assert.True(StateTransformer.TransformStringToState("GAME_PAUSED") == GameStateType.GamePaused);
            Assert.True(StateTransformer.TransformStringToState("MAIN_MENU") == GameStateType.MainMenu);
        }

        [Test]
        public void TestStateToString() {
            Assert.True(StateTransformer.TransformStateToString(GameStateType.GameRunning) == "GAME_RUNNING");
            Assert.True(StateTransformer.TransformStateToString(GameStateType.GamePaused) == "GAME_PAUSED");
            Assert.True(StateTransformer.TransformStateToString(GameStateType.MainMenu) == "MAIN_MENU");
        }
    }
}

