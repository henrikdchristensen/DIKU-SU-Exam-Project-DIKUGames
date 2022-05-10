using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using Breakout.Score;

namespace BreakoutTests {

    [TestFixture]
    public class ScoreTest {
        private Score score;

        [SetUp]
        public void InitializeTest() {
            Window.CreateOpenGLContext();
            score = new Score(new Vec2F(0, 0), new Vec2F(0, 0));
        }

        [Test]
        public void TestAddPoints() {
            int oldPoints = score.GetScore();
            score.AddPoints();
            Assert.True(score.GetScore() > oldPoints);
        }

        [Test]
        public void TestResetScore() {
            score.AddPoints();
            score.Reset();
            Assert.True(score.GetScore() == 0);
        }

    }
}