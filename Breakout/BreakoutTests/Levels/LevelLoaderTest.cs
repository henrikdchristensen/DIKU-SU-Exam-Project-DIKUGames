using NUnit.Framework;
using System.IO;
using Breakout.Levels;

namespace BreakoutTests.Levels {

    [TestFixture]
    public class LevelLoaderTest {

        private ILevelLoader loader;
        private string levelFolder;

        [SetUp]
        public void Setup() {
            loader = new LevelLoader();
            levelFolder = Path.Combine("..", "Breakout", "Assets", "Levels");
        }

        [Test]
        public void TestLevelLoader() {
            string path = Path.Combine(levelFolder, "levelLoaderTest1.txt");
            string[] expected = new string[] {
                "Map:",
                "------------",
                "Map/",
                "Meta:",
                "Name: LEVEL 1",
                "Meta/",
                "Legend:",
                "%) blue-block.png",
                "Legend/"
            };

            string[] actual = loader.Load(path);

            Assert.AreEqual(expected, actual);
        }

    }

}