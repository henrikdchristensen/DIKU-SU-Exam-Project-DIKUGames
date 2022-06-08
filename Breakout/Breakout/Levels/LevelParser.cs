using Breakout.Utility;

namespace Breakout.Levels {

    public class LevelParser {

        private const string MAP                = "Map";
        private const string LEGEND             = "Legend";
        private const char LEGEND_SEPERATOR     = ')';
        private const string META               = "Meta";
        private const char META_SEPERATOR       = ':';
        private const char START                = ':';
        private const char END                  = '/';

        private ILevelLoader loader;

        /// <summary>
        /// Creates a level parser-instance
        /// </summary>
        /// <param name="loader">A level loader, to load files</param>
        public LevelParser(ILevelLoader loader) {
            this.loader = loader;
        }


        /// <summary>
        /// Creates a level from a level-file
        /// require:
        ///     the file with the given path, must have the correct level format.
        /// ensure:
        ///     it returns a level accordance to preperties specified in the file
        /// </summary>
        /// <param name="path">The path of the file</param>
        /// <returns>The corresponding level</returns>
        /// <exception cref="ArgumentException">If the file is not correctly formated</exception>
        public Level CreateLevel(string path) {
            string[] level;
            try {
                level = loader.Load(path);
            } catch (ArgumentException e) {
                throw;
            }

            if (!LevelValidator.ValidateLevel(level))
                throw new ArgumentException("Invalid format of given file: " + path);

            //Generate datastructeres for the level
            var map = linesTo2DCharArr(SubarrayExtractor.Extract(level, $"{MAP + START}", $"{MAP + END}"));
            var meta = linesToDict(
                SubarrayExtractor.Extract(level, $"{META + START}", $"{META + END}"),
                META_SEPERATOR);
            var legend = linesToDict(
                SubarrayExtractor.Extract(level, $"{LEGEND + START}", $"{LEGEND + END}"),
                LEGEND_SEPERATOR);

            return new Level(map, meta, legend);
        }

        /// <summary>Conversts an array of lines to a dictionary</summary>
        /// <param name="lines">The lines that should be converted</param>
        /// <param name="delimiter">The delimitor, that seperates the key-value pair in the line</param>
        /// <returns>Return the dictionary</returns>
        private Dictionary<string, string> linesToDict(string[] lines, char delimiter) {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string line in lines) {
                string[] pair = line.Split(delimiter + " ", 2);
                dict.Add(pair[0], pair[1]);
            }
            return dict;
        }

        /// <summary>Conversts an array of lines to a 2d-char-array. </summary>
        /// <param name="lines">The lines that should be converted</param>
        /// <returns>returns the char array</returns>
        private char[,] linesTo2DCharArr(string[] lines) {
            char[,] ch = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < lines[i].Length; j++)
                    ch[i, j] = lines[i][j];
            return ch;
        }

    }

}