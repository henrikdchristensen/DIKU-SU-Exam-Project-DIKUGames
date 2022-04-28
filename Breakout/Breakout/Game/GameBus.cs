using DIKUArcade.Events;

namespace Breakout.Game {
    public static class GameBus {
        private static GameEventBus gameBus;
        public static GameEventBus GetBus() {
            return gameBus ?? (gameBus = new GameEventBus());
        }
    }
}

