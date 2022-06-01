using DIKUArcade.Utilities;
using Breakout.Utility;

namespace Breakout.Levels {

    public static class LevelValidator {

        private const string MAP =  "Map";
        private const string LEGEND = "Legend";
        private const char LEGEND_SEPERATOR = ')';
        private const string META = "Meta";
        private const char META_SEPERATOR = ':';
        private const char START = ':';
        private const char END = '/';
        private const int HEIGHT = 25;
        private const int WIDTH = 12;
        private const char EMPTY_TILE = '-';

        private static readonly string FILE_PATH =
            Path.Combine(FileIO.GetProjectPath(), "..", "Breakout", "Assets", "Images");
        private static readonly string[] META_BLOCK_ATTR =
            { "Unbreakable", "PowerUp", "Hardened" };
        private static List<char> blockChars;

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="level">TODO</param>
        /// <returns>TODO</returns>
        public static bool ValidateLevel(string[] level) {
            // Validate that all group names exist (no duplicates)
            if (!validateGroups(level))
                return false;

            // All groups exist, and each group can be split into a array
            blockChars = new List<char>();
            string[] legend = SubarrayExtractor.Extract(level, $"{LEGEND + START}", $"{LEGEND + END}");
            string[] map = SubarrayExtractor.Extract(level, $"{MAP + START}", $"{MAP + END}");
            string[] meta = SubarrayExtractor.Extract(level, $"{META + START}", $"{META + END}");

            return validateLegend(legend) &&
                   validateMap(map) &&
                   validateMeta(meta);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="level">TODO</param>
        /// <returns>TODO</returns>
        private static bool validateGroups(string[] level) {
            string[] groupNames = { $"{MAP}{START}", $"{MAP}{END}",
                                     $"{LEGEND}{START}", $"{LEGEND}{END}",
                                     $"{META}{START}", $"{META}{END}"};
            foreach (string name in groupNames) {
                if (Array.FindAll(level, e => e.Contains(name)).Length != 1)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="legendLines">TODO</param>
        /// <returns>TODO</returns>
        private static bool validateLegend(string[] legendLines) {
            foreach (string line in legendLines) {
                string[] split = line.Split(LEGEND_SEPERATOR + " ");
                if (!validateLegendPair(split))
                    return false;
                blockChars.Add(split[0][0]);
            }
            return true;   
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="pair">TODO</param>
        /// <returns>TODO</returns>
        private static bool validateLegendPair(string[] pair) {
            return pair.Length == 2 &&
                   pair[0].Length == 1 || //first entry should be a char
                   !blockChars.Contains(pair[0][0]) || //the block-char should not have been visited before
                   File.Exists(Path.Combine(FILE_PATH, pair[1])); //does the image exist
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="metaLines">TODO</param>
        /// <returns>TODO</returns>
        private static bool validateMeta(string[] metaLines) {
            List<string> visited = new List<string>();
            foreach (string line in metaLines) {
                string[] split = line.Split(META_SEPERATOR + " ");
                if (!validateMetaPair(split, visited))
                    return false;
                visited.Add(split[0]);
            }
            return true;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="pair">TODO</param>
        /// <param name="visited">TODO</param>
        /// <returns>TODO</returns>
        private static bool validateMetaPair(string[] pair, List<string> visited) {
            return pair.Length == 2 && //expected length
                   !visited.Contains(pair[0]) &&
                   !(META_BLOCK_ATTR.Contains(pair[0]) && !blockChars.Contains(pair[1][0]));
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="mapLines">TODO</param>
        /// <returns>TODO</returns>
        private static bool validateMap(string[] mapLines) {
            if (mapLines.Length != HEIGHT)
                return false;
            foreach (string line in mapLines) {
                if (line.Length != WIDTH)
                    return false;
                foreach (char ch in line) {
                    if (ch != EMPTY_TILE && !blockChars.Contains(ch))
                        return false;
                }
            }
            return true;
        }

    }
}
