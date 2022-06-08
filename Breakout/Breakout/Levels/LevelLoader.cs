using DIKUArcade.Utilities;

namespace Breakout.Levels {

    public class LevelLoader : ILevelLoader {

        /// <summary>Converts a txt file to an array of lines</summary>
        /// <param name="path">the path of the txt file</param>
        /// <returns>The array of lines representing the txt-file</returns>
        /// <exception cref="ArgumentException">Uf the file does not exist</exception>
        public string[] Load(string path) {
            var file = Path.Combine(FileIO.GetProjectPath(), path);

            if (!File.Exists(file))
                throw new ArgumentException("File could not be found. Invalid path: " + file);

            string text = File.ReadAllText(file);
            return text.Split(Environment.NewLine);
        }

    }

}
