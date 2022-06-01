using NUnit.Framework;
using DIKUArcade.Math;
using Breakout;

namespace BreakoutTests {

    [TestFixture]
    public class ScoreTest {

        private Score score;

        /// <summary>Instantiate an Score instance.</summary>
        [SetUp]
        public void InitializeTest() {
            score = new Score(new Vec2F(0, 0), new Vec2F(0, 0));
        }

        /// <summary>Blacbox: Test adding points to score.</summary>
        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void TestAddPoints(int add) {
            int oldPoints = score.GetScore();
            score.AddPoints(add);
            if (add > 0) {
                Assert.True(score.GetScore() == oldPoints + add);
            } else {
                Assert.True(score.GetScore() == oldPoints);
            }
        }

        /// <summary>Test maximum possible points.</summary>
        [Test]
        public void TestMaximumPoints() {
            score.AddPoints(int.MaxValue);
            score.AddPoints(10);
            Assert.True(score.GetScore() == int.MaxValue);
        }

        /// <summary>Blackbox: Test resetting score.</summary>
        [Test]
        public void TestResetScore() {
            score.AddPoints();
            score.Reset();
            Assert.True(score.GetScore() == 0);
        }

    }

}