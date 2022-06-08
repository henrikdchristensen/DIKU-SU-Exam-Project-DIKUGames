using NUnit.Framework;
using Breakout.Entities.Powerups;

namespace BreakoutTests.poweruptests {

    [TestFixture]
    public class PowerupTypeTransformer {

        [Test]
        public void TestTransformStringToState() {
            Assert.True(PowerupTransformer.StringToState("PLAYER_SPEED") == PowerupType.PlayerSpeed);
            Assert.True(PowerupTransformer.StringToState("EXTRA_LIFE") == PowerupType.ExtraLife);
            Assert.True(PowerupTransformer.StringToState("WIDE") == PowerupType.Wide);
            Assert.True(PowerupTransformer.StringToState("HARD_BALL") == PowerupType.HardBall);
            Assert.True(PowerupTransformer.StringToState("DOUBLE_SPEED") == PowerupType.DoubleSpeed);
            Assert.True(PowerupTransformer.StringToState("DOUBLE_SIZE") == PowerupType.DoubleSize);
        }

        [Test]
        public void TestStateToString() {
            Assert.True(PowerupTransformer.StateToString(PowerupType.PlayerSpeed) == "PLAYER_SPEED");
            Assert.True(PowerupTransformer.StateToString(PowerupType.ExtraLife) == "EXTRA_LIFE");
            Assert.True(PowerupTransformer.StateToString(PowerupType.Wide) == "WIDE");
            Assert.True(PowerupTransformer.StateToString(PowerupType.HardBall) == "HARD_BALL");
            Assert.True(PowerupTransformer.StateToString(PowerupType.DoubleSpeed) == "DOUBLE_SPEED");
            Assert.True(PowerupTransformer.StateToString(PowerupType.DoubleSize) == "DOUBLE_SIZE");
        }
    }
}
