using DIKUArcade.GUI;

namespace Breakout.Game {

    class Program {
        /// <summary>
        /// Entry point of the program. Creates the window
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
            var game = new Game(windowArgs);
            game.Run();
        }

    }

}