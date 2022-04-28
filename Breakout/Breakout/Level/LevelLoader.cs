using System.IO;

namespace Breakout.Levels {
    public class LevelLoader : ILoader {
        
        public LevelLoader() { }


        /// <summary>
        ///
        /// require:
        ///     the file parameter is an existing file
        ///     the file has to have a correct setup
        ///
        /// ensure:
        ///     it returns a level accordance to preperties specified in the file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Level CreateLevel(string file) {
            if (!File.Exists(file))
                throw new ArgumentException("file could not be found"); 

            string text = File.ReadAllText(file);
            if (!text.Contains("Map:") || !text.Contains("Map/") ||
                    !text.Contains("Map:") || !text.Contains("Map/") ||
                    !text.Contains("Map:") || !text.Contains("Map/"))
                throw new ArgumentException("file does not have correct format");

            var map = linesToCharArr(extractData(text, "Map"));
            var meta = linesToDict(extractData(text, "Meta"), ':');
            var legend = linesToDict(extractData(text, "Legend"), ')');
            return new Level(map, meta, legend);
        }


        private string[] extractData(string text, string keyword) {
            int start = text.IndexOf(keyword) + keyword.Length + 1;
            int end = text.LastIndexOf(keyword);
            string content = text.Substring(start, end - start).Trim();
            return content.Split("\r\n");
        }

        private Dictionary<string, string> linesToDict(string[] lines, char delimiter) {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string line in lines) {
                string[] pair = line.Split(delimiter + " ", 2);
                dict.Add(pair[0], pair[1]);
            }
            return dict;
        }

        private char[,] linesToCharArr(string[] lines) {
            char[,] ch = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++) 
                for (int j = 0; j < lines[i].Length; j++)
                    ch[i, j] = lines[i][j];
            return ch;
        }

    }
}