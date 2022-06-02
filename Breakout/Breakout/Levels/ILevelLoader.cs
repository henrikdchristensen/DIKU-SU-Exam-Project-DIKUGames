namespace Breakout.Levels {

    public interface ILevelLoader {

        /// <summary>TODO</summary>
        /// <param name="path">TODO</param>
        /// <returns>The corresponding level</returns>
        public string[] Load(string path);

    }
}
