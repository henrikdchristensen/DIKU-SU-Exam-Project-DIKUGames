using Breakout.Levels;
using NUnit.Framework;
using System.Collections.Generic;

namespace BreakoutTests {

    public class Tests {
        private ILoader loader = new LevelLoader();

        [SetUp]
        public void Setup() {
        }

        [Test]
        public void TestLevel1() {
            Level? level = loader.CreateLevel(@"..\..\..\..\Breakout\Assets\Levels\level1.txt");
            char[,] map = {{
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', '-'}, {
                    '-', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', '-'}, {
                    '-', '0', '0', '0', '-', '-', '-', '-', '0', '0', '0', '-'}, {
                    '-', '0', '0', '0', '-', '%', '%', '-', '0', '0', '0', '-'}, {
                    '-', '0', '0', '0', '-', '1', '1', '-', '0', '0', '0', '-'}, {
                    '-', '0', '0', '0', '-', '%', '%', '-', '0', '0', '0', '-'}, {
                    '-', '0', '0', '0', '-', '-', '-', '-', '0', '0', '0', '-'}, {
                    '-', '%', '%', '%', '%', '%', '%', '%', '%', '%', '%', '-'}, {
                    '-', '%', '%', '%', '%', '%', '%', '%', '%', '%', '%', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}};

            Dictionary<string, string> meta = new Dictionary<string, string>(){{
                    "Name", "LEVEL 1"}, {
                    "Time", "300"}, {
                    "Hardened", "#"}, {
                    "PowerUp", "2"}};
            Dictionary<string, string> legend = new Dictionary<string, string>(){{
                    "%", "blue-block.png"}, {
                    "0", "grey-block.png"}, {
                    "1", "orange-block.png"}, {
                    "a", "purple-block.png"}};

            Level expected = new Level(map, meta, legend);
            Assert.AreEqual(expected, level);
        }

        [Test]
        public void TestLevel2() {
            Level? level = loader.CreateLevel(@"..\..\..\..\Breakout\Assets\Levels\level2.txt");
            char[,] map = {{
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    'h', 'h', 'h', 'h', 'h', 'h', 'h', 'h', 'h', 'h', 'h', 'h'}, {
                    'h', 'h', 'i', 'h', 'h', 'h', 'h', 'h', 'h', 'i', 'h', 'h'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    'j', 'j', 'j', 'j', 'j', 'i', 'j', 'j', 'j', 'j', 'j', 'j'}, {
                    'j', 'j', 'i', 'j', 'j', 'j', 'i', 'j', 'j', 'i', 'j', 'j'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'}, {
                    'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}};

            Dictionary<string, string> meta = new Dictionary<string, string>(){{
                    "Name", "LEVEL 2"}, {
                    "Time", "180"}, {
                    "PowerUp", "i"}};
            Dictionary<string, string> legend = new Dictionary<string, string>(){{
                    "h", "green-block.png"}, {
                    "i", "teal-block.png"}, {
                    "j", "blue-block.png"}, {
                    "k", "brown-block.png"}};

            Level expected = new Level(map, meta, legend);
            Assert.AreEqual(expected, level);
        }

        [Test]
        public void TestLevel3() {
            Level? level = loader.CreateLevel(@"..\..\..\..\Breakout\Assets\Levels\level3.txt");
            char[,] map = {{
                    'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '#', '-', '-', '-', '-', '-', '-', '-', '-', '#', '-'}, {
                    '-', '-', '#', '-', '-', '-', '-', '-', '-', '#', '-', '-'}, {
                    '-', '-', '-', '#', '-', '-', '-', '-', '#', '-', '-', '-'}, {
                    '-', '-', '-', '-', '#', '-', '-', '#', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', 'Y', '-', '-', '-', '-', 'Y', '-', '-', '-'}, {
                    '0', '0', '0', 'Y', '-', '-', '-', '-', 'Y', '0', '0', '0'}, {
                    '0', '0', '0', 'Y', '-', '-', '-', '-', 'Y', '0', '0', '0'}, {
                    '0', '0', '0', 'Y', '-', '-', '-', '-', 'Y', '0', '0', '0'}, {
                    '0', '0', '0', 'Y', '-', '#', '#', '-', 'Y', '0', '0', '0'}, {
                    '0', '0', '0', 'Y', '-', '-', '-', '-', 'Y', '0', '0', '0'}, {
                    'Y', 'Y', 'Y', 'Y', 'w', 'w', 'w', 'w', 'Y', 'Y', 'Y', 'Y'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}, {
                    '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'}};

            Dictionary<string, string> meta = new Dictionary<string, string>(){{
                    "Name", "LEVEL 3"}, {
                    "Time", "180"}, {
                    "PowerUp", "#"}, {
                    "Unbreakable", "Y"}};
            Dictionary<string, string> legend = new Dictionary<string, string>(){{
                    "0", "orange-block.png"}, {
                    "w", "darkgreen-block.png"}, {
                    "#", "green-block.png"}, {
                    "Y", "brown-block.png"}, {
                    "b", "yellow-block.png"}};

            Level expected = new Level(map, meta, legend);
            Assert.AreEqual(expected, level);
            //Assert.Fail(level?.ToString());
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