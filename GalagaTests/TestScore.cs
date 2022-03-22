using NUnit.Framework;
using Galaga;
using DIKUArcade.Math;

namespace GalagaTests {

    [TestFixture]
    public class ScoreTesting {
        private Score score;

        [SetUp]
        public void InitiateScore() {
            score = new Score(new Vec2F(), new Vec2F()); //TODO
        }

        [Test]
        public void TestAddPoints() { //TODO
            score.AddPoints();
        }

        [Test]
        public void TestRenderScore() { //TODO
            score.RenderScore();
        }

    }
}