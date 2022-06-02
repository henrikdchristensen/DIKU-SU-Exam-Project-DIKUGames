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

        /// <summary>
        /// Blackbox: Test adding points to score.
        /// Case:       Expected output:    Comment:
        /// add=-10     -10                 Negative numbers
        /// add=0       0                   Boundary
        /// add=1       1                   Small numbers
        /// add=10      10                  Big numbers
        /// </summary>
        [TestCase(-10, 0)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(10, 10)]
        public void TestAddPoints(int add, int expected) {
            score.AddPoints(add);
            Assert.True(score.GetScore() == expected, TestLogger.OnFailedTestMessage<int>(expected, score.GetScore()));
        }

        /// <summary>
        /// Blackbox: Test maximum points to score.
        /// Case:                   Expected output:    Comment:
        /// add=int.MaxValue+10     int.MaxValue        Score should maximize to int.MaxValue
        /// </summary>
        [Test]
        public void TestMaximumPoints() {
            score.AddPoints(int.MaxValue);
            score.AddPoints(10);
            Assert.True(score.GetScore() == int.MaxValue, TestLogger.OnFailedTestMessage<int>(int.MaxValue, score.GetScore()));
        }

        /// <summary>Blackbox: Test resetting score</summary>
        [Test]
        public void TestResetScore() {
            score.AddPoints();
            score.Reset();
            Assert.True(score.GetScore() == 0, TestLogger.OnFailedTestMessage<int>(0, score.GetScore()));
        }

    }

}