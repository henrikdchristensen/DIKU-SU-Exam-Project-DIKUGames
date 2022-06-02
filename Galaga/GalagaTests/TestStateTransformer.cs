using NUnit.Framework;
using Galaga;

namespace GalagaTests {

    [TestFixture]
    public class TestStateTransformer {

        [Test]
        public void TestTransformStringToState() {
            Assert.True(StateTransformer.StringToState("GAME_RUNNING") == GameStateType.GameRunning);
            Assert.True(StateTransformer.StringToState("GAME_PAUSED") == GameStateType.GamePaused);
            Assert.True(StateTransformer.StringToState("MAIN_MENU") == GameStateType.MainMenu);
        }

        [Test]
        public void TestStateToString() {
            Assert.True(StateTransformer.StateToString(GameStateType.GameRunning) == "GAME_RUNNING");
            Assert.True(StateTransformer.StateToString(GameStateType.GamePaused) == "GAME_PAUSED");
            Assert.True(StateTransformer.StateToString(GameStateType.MainMenu) == "MAIN_MENU");
        }
    }
}

