using System;
using NUnit.Framework;
using Breakout.Level;

namespace BreakoutTests {

    public class Tests {
        private ILoader loader = new LevelLoader();

        [SetUp]
        public void Setup() {
        }

        [Test]
        public void TestLevel1() {
            Level? level = loader.CreateLevel(@"..\Breakout\Assets\Levels\level1.txt");
            Assert.Fail(level.ToString());
        }
    

        [Test]
        public void TestInvalidPath() {
            try {
                Level? level = loader.CreateLevel("INVALID_PATH");
                Assert.IsTrue(level == null);
            } catch {
                Assert.Fail();
            }
            
        }

        [Test]
        public void TestNoContent() {
            try {
                Level? level = loader.CreateLevel(@"..\Breakout\Assets\Levels\noContent.txt");
                Assert.IsTrue(level == null);
            } catch {
                Assert.Fail();
            }
        }
    }

}