using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga;

namespace GalagaTests {

    [TestFixture]
    public class TestEnemy {
        private Enemy enemy;

        [SetUp]
        public void InitializeTest() {
            enemy = new Enemy(new DynamicShape(0.0f, 0.0f, 0.0f, 0.0f), new NoImage(), new NoImage(), 0.0f);
        }

        [Test]
        public void TestHit() { // Is hitpoints decremented
            int oldHitpoints = enemy.Hitpoints;
            enemy.Hit();
            Assert.True(enemy.Hitpoints < oldHitpoints);
        }

        [Test]
        public void TestThreshold() { // Does state change when hitpoints hit threshhold
            IBaseImage oldImage = enemy.Image;
            float oldSpeed = enemy.Speed;
            enemy.Hit();
            enemy.Hit();
            enemy.Hit();
            Assert.AreNotEqual(oldImage, enemy.Image);
            Assert.AreNotEqual(oldSpeed, enemy.Speed);
        }

        [Test]
        public void TestDie() { // Does hit return true when dead (5 times hit)
            enemy.Hit();
            enemy.Hit();
            enemy.Hit();
            enemy.Hit();
            Assert.True(enemy.Hit());
        }
    }
}


