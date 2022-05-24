using DIKUArcade.Events;

namespace Breakout.Game {

    public static class GameBus {

        private static GameEventBus gameBus;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static GameEventBus GetBus() {
            return gameBus ?? (gameBus = new GameEventBus());
        }

    }

}

