using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Utilities;
using Breakout.Levels;

namespace BreakoutTests {

    [TestFixture]
    public class LevelValidatorTest {


        private string[] correctMap = new string[] {
            "Map:",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------",
            "Map/"
        };
        private string[] correctMeta = new string[] {
            "Meta:",
            "Meta/"
        };
        private string[] correctLegend = new string[] {
            "Legend:",
            "Legend/"
        };

        [SetUp]
        public void Setup() {}

        //Following test are Blackbox 
        [Test]
        public void TestSimpleLevel() {
            string[] level = CombineArrays(correctMap, correctMeta, correctLegend);
            bool actual = LevelValidator.ValidateLevel(level);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [TestCase(new string[]{
            "Map:", "------------", "Map/"}
        , false)] //Height too small (1)
        [TestCase(new string[]{
            "Map:",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "Map/"}
        , false)] //Height too small (24)
        [TestCase(new string[]{
            "Map:",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "Map/"}
        , false)] //Height too large (27)
        [TestCase(new string[]{
            "Map:",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "-----------",
            "Map/"}
        , false)] //width not consistent
        [TestCase(new string[]{
            "Map:",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------",
            "Map/"}
        , true)] //correct
        public void TestMapSize(string[] map, bool expected) {
            string[] level = CombineArrays(map, correctMeta, correctLegend);
            bool actual = LevelValidator.ValidateLevel(level);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(new string[]{
            "Meta:",
            "Name: LEVEL 1",
            "Name: LEVEL 2",
            "Meta/"}
        , false)] //same attribute
        [TestCase(new string[]{
            "Meta:",
            "Meta"}
        , false)] //incorrect end keyword
        [TestCase(new string[]{
            "Meta:",
            "Name LEVEL 1",
            "Meta/"}
        , false)] //no seperator
        [TestCase(new string[]{
            "Meta:",
            "PowerUp: #",
            "Meta/"}
        , false)] //'#' does not exist in legend
        [TestCase(new string[]{
            "Meta:",
            "Time: 100",
            "Name: LEVEL 1",
            "Meta/"}
        , true)] //correct
        public void TestMeta(string[] meta, bool expected) {
            string[] level = CombineArrays(correctMap, meta, correctLegend);
            bool actual = LevelValidator.ValidateLevel(level);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(new string[]{
            "Legend:",
            "1) orange-block.png",
            "Legend/"}
        , true)] //correct image
        [TestCase(new string[]{
            "Legend:",
            "1) orange-block.ng",
            "Legend/"}
        , false)] //incorrect image
        [TestCase(new string[]{
            "Legend:",
            "1) orange-block.png",
            "1) orange-block.¨png",
            "Legend/"}
        , false)] //duplicate
        [TestCase(new string[]{
            "Legend",
            "1) orange-block.png",
            "Legend/"}
        , false)] //incorrect start keyword
        [TestCase(new string[]{
            "Legend:",
            "1 orange-block.png",
            "Legend/"}
        , false)] //incorrect seperator
        public void TestLegend(string[] legend, bool expected) {
            string[] level = CombineArrays(correctMap, correctMeta, legend);
            bool actual = LevelValidator.ValidateLevel(level);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(new string[]{
            "Map:",
            "-----111----", "------------", "------------",
            "------------", "------------", "------------",
            "-----111----", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------",
            "Map/",
            "Meta:",
            "Time: 100",
            "Name: LEVEL 1",
            "Meta/",
            "Legend:",
            "1) orange-block.png",
            "Legend/"}
        , true)] //Correct level
        [TestCase(new string[]{
            "Map:",
            "-----111----", "------------", "------------",
            "------------", "------------", "------------",
            "-----111----", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "-------##---", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------",
            "Map/",
            "Meta:",
            "Time: 100",
            "Name: LEVEL 1",
            "Meta/",
            "Legend:",
            "1) orange-block.png",
            "Legend/"}
        , false)] //'#' tile does not exist in legend
        [TestCase(new string[]{
            "Map:",
            "-----111----", "------------", "------------",
            "------------", "------------", "------------",
            "-----111----", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------",
            "Map/",
            "Meta:",
            "Time: 100",
            "Name: LEVEL 1",
            "PowerUp: V",
            "Meta/",
            "Legend:",
            "1) orange-block.png",
            "Legend/"}
        , false)] //powerup tile, 'V', does not exist in legend
        [TestCase(new string[]{
            "Map:",
            "-----111----", "------------", "------------",
            "------------", "------------", "------------",
            "-----111----", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "-V----------", "------------",
            "------------", "1-----------", "------------",
            "----------i-", "------------", "------------",
            "----------i-", "------------", "------------",
            "------------",
            "Map/",
            "Meta:",
            "Time: 100",
            "Name: LEVEL 1",
            "PowerUp: V",
            "Unbreakable: i",
            "Meta/",
            "Legend:",
            "1) orange-block.png",
            "V) green-block.png",
            "i) red-block.png",
            "Legend/"}
        , true)] //correct, complex level
        public void TestAll(string[] level, bool expected) {
            bool actual = LevelValidator.ValidateLevel(level);
            Assert.AreEqual(expected, actual);
        }


        //Following test are Whitebox, and ensures 100% C_1 coverage
        //*.a = branch condition is true
        //*.b = branch condition is false
        [TestCase(new string[]{
            "Map:",
            "Map/",
            "Meta:",
            "Meta/",
            "Legend:",
            "L"}
        , false)] //Testing Branch 1.a
        [TestCase(new string[]{
            "Map:",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------", "------------", "------------",
            "------------",
            "Map/",
            "Meta:",
            "Meta/",
            "Legend:",
            "Legend/"}
        , true)] //Branch 1.b -> Branch 2, Testing Branch 2.a
        [TestCase(new string[]{
            "Map:",
            "Map/",
            "Meta:",
            "Meta/",
            "Legend:",
            "Legend/"}
        , false)] //Testing Branch 2.b
        public void TestAllWhiteBox(string[] level, bool expected) {
            bool actual = LevelValidator.ValidateLevel(level);
            Assert.AreEqual(expected, actual);
        }

        private string[] CombineArrays(string[] map, string[] meta, string[] legend) {
            List<string> level = new List<string>();
            level.AddRange(map);
            level.AddRange(meta);
            level.AddRange(legend);
            return level.ToArray();
        }



    }
}