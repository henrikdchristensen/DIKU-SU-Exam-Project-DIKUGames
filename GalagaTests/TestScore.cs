using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using Galaga;

namespace GalagaTests {

    [TestFixture]
    public class TestScore {
        private Score score;

        [SetUp]
        public void InitializeTest() {
            Window.CreateOpenGLContext();
            score = new Score(new Vec2F(0, 0), new Vec2F(0, 0));
        }

        [Test]
        public void TestAddPoints() {
            int oldPoints = score.Points;
            score.AddPoints();
            Assert.True(score.Points > oldPoints);
        }
    }
}