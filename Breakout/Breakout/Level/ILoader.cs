
namespace Breakout.Levels {
    public interface ILoader {
        public Level? CreateLevel(string path);
    }
}