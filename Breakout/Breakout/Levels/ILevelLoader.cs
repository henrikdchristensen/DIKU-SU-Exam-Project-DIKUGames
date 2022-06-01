namespace Breakout.Levels {
    public interface ILevelLoader {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The corresponding level</returns>
        public string[] Load(string path);

    }
}
