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

        public LevelParser(ILevelLoader loader) {
            this.loader = loader;
        }


        /// <summary>
        /// Creates a level from a level-txt-file
        /// require:
        ///     the file parameter is an existing file
        ///     the file has to have a correct setup
        ///
        /// ensure:
        ///     it returns a level accordance to preperties specified in the file
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The corresponding level</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        private Dictionary<string, string> linesToDict(string[] lines, char delimiter) {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string line in lines) {
                string[] pair = line.Split(delimiter + " ", 2);
                dict.Add(pair[0], pair[1]);
            }
            return dict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private char[,] linesTo2DCharArr(string[] lines) {
            char[,] ch = new char[lines.Length, lines[0].Length];

             for (int i = 0; i < lines.Length; i++)
                 for (int j = 0; j < lines[i].Length; j++)
                     ch[i, j] = lines[i][j];
            return ch;
        }

    }

}