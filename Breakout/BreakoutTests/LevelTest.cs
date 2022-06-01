using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Utilities;
using Breakout.Levels;

namespace BreakoutTests {

    [TestFixture]
    public class LevelTest {

        private LevelParser parser = new LevelParser(new LevelLoader());
        private string levelFolder;

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup() {
            levelFolder = Path.Combine("..","Breakout", "Assets", "Levels");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestLevel1() {
            Console.WriteLine(levelFolder);
            Window.CreateOpenGLContext();
            Level level = parser.CreateLevel(Path.Combine(levelFolder, "level1.txt"));
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestLevel2() {
            Window.CreateOpenGLContext();
            Level level = parser.CreateLevel(Path.Combine(levelFolder, "level2.txt"));
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestLevel3() {
            Window.CreateOpenGLContext();
            Level level = parser.CreateLevel(Path.Combine(levelFolder, "level3.txt"));
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
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestInvalidPath() {
            Window.CreateOpenGLContext();
            try {
                Level level = parser.CreateLevel("INVALID_PATH");
            } catch (ArgumentException e) {
                //Correct exception is thrown
                var path = Path.Combine(FileIO.GetProjectPath(), "INVALID_PATH");
                Console.WriteLine(e.Message);
                if (e.Message == "File could not be found. Invalid path: " /*+ path*/)
                    Assert.Pass();
            }
            Assert.Fail();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestNoContent() {
            Window.CreateOpenGLContext();
            try {
                Level level = parser.CreateLevel(Path.Combine(levelFolder, "noContent.txt"));
            } catch (ArgumentException e) {
                //Correct exception is thrown
                Assert.Pass();
            }
            Assert.Fail();
        }
        
        [Test]
        public void TestValidator() {
            var file = Path.Combine(levelFolder, "level3-modified.txt");
            file = Path.Combine(FileIO.GetProjectPath(), file);

            string[] text = File.ReadAllText(file).Split(Environment.NewLine);

            LevelValidator.ValidateLevel(text);
            Assert.Fail();
        }

    }

}