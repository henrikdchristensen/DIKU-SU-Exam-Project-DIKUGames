namespace Breakout.Levels {

    public interface ILevelLoader {

        /// <summary>Converts a txt file to an array of lines</summary>
        /// <param name="path">the path of the txt file</param>
        /// <returns>The array of lines representing the txt-file</returns>
        public string[] Load(string path);

    }
}
