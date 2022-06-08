namespace Breakout.Levels {

    public interface ILevelLoader {

        /// <summary>
        /// Loads a file and deserialize the content into an array of lines
        /// </summary>
        /// <param name="path">The path of the file</param>
        /// <returns>Returns an array of lines in witch it will get validated by the levelValidator</returns>
        /// <exception cref="ArgumentException">Uf the file does not exist</exception>
        public string[] Load(string path);

    }
}
