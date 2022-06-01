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

        /// <summary>
        /// Trigger event to game bus.
        /// </summary>
        /// <param name="type">GameEventType</param>
        /// <param name="message">Message sent to GameEventBus</param>
        /// <param name="stringArg">StringArg sent to GameEventBus</param>
        public static void TriggerEvent(GameEventType type, string message = "", string stringArg = "", int intArg = 0) {
            GetBus().RegisterEvent(new GameEvent {
                EventType = type,
                Message = message,
                StringArg1 = stringArg,
                IntArg1 = intArg
            });
        }

    }

}

