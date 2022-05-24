using Breakout.Input;

namespace Breakout.Levels {
    public class LevelLoader : ILoader {
        
        public LevelLoader() { }


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
        public Level CreateLevel(string file) {
            file = FilePath.GetAbsolutePath(file);

            if (!File.Exists(file))
                throw new ArgumentException("File could not be found. Invalid path: " + file); 

            string text = File.ReadAllText(file);
            if (!isTextValid(text))
                throw new ArgumentException("file does not have correct format");

            var map = linesTo2DCharArr(extractData(text, "Map"));
            var meta = linesToDict(extractData(text, "Meta"), ':');
            var legend = linesToDict(extractData(text, "Legend"), ')');
            return new Level(map, meta, legend);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private string[] extractData(string text, string keyword) {
            int start = text.IndexOf(keyword) + keyword.Length + 1;
            int end = text.LastIndexOf(keyword);
            string content = text.Substring(start, end - start).Trim();
            return content.Split(Environment.NewLine);
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

            try {
             for (int i = 0; i < lines.Length; i++)
                 for (int j = 0; j < lines[i].Length; j++)
                     ch[i, j] = lines[i][j];
            } catch (Exception) {

                Console.WriteLine("Out of bounds proceeeding");
            }

            return ch;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool isTextValid(string text) {
            return text.Contains("Map:") && text.Contains("Map/") ||
                   text.Contains("Meta:") && text.Contains("Meta/") ||
                   text.Contains("Legend:") && text.Contains("Legend/");
        }

    }

}