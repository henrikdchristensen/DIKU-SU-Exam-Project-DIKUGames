
namespace Breakout.Levels {

    /// <summary>
    /// 
    /// </summary>
    public interface ILoader {
        /// <summary>
        /// Creates a level from a level-file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The corresponding level</returns>
        public Level CreateLevel(string path);
    }
}