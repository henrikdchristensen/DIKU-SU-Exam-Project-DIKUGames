using DIKUArcade.Utilities;

namespace Breakout.Levels {

    public class LevelLoader : ILevelLoader {

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="path">TODO</param>
        /// <returns>TODO</returns>
        /// <exception cref="ArgumentException"></exception>
        public string[] Load(string path) {
            var file = Path.Combine(FileIO.GetProjectPath(), path);

            if (!File.Exists(file))
                throw new ArgumentException("File could not be found. Invalid path: " + file);

            string text = File.ReadAllText(file);
            return text.Split(Environment.NewLine);
        }

    }

}
