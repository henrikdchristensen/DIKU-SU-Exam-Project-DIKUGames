using NUnit.Framework;
using DIKUArcade.GUI;
using Breakout.Game;

namespace BreakoutTests.GameTest {

    [TestFixture]
    public class Test {

        private Game game;

        [SetUp]
        public void InitiateGame() {
            game = new Game(new WindowArgs() { Title = "Breakout v0.1" });
        }

        [Test]
        public void TestGame() {
            game.Render();
            game.Update();
            game.Run();
        }
    }
}