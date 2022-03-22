using NUnit.Framework;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.GUI;

namespace GalagaTests {

    [TestFixture]
    public class ScoreTesting {
        private Score score;

        [SetUp]
        public void InitiateScore() {
            Window.CreateOpenGLContext();
            score = new Score(new Vec2F(0,0), new Vec2F(0,0));
        }

        [Test]
        public void TestAddPoints() {
            int oldPoints = score.Points;
            score.AddPoints();
            Assert.True(score.Points > oldPoints);
        }
    }
}