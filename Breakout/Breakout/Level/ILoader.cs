
namespace Breakout.Level {
    public interface ILoader {
        public Level? CreateLevel(string path);
    }
}