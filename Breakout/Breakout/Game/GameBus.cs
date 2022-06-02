using DIKUArcade.Events;
using DIKUArcade.Timers;

namespace Breakout.Game {

    public static class GameBus {

        private static GameEventBus gameBus;

        /// <summary>Returns a new instance of GameEventBus</summary>
        /// <returns>A new instance of GameEventBus</returns>
        public static GameEventBus GetBus() {
            return gameBus ?? (gameBus = new GameEventBus());
        }

        /// <summary>Trigger event to game bus</summary>
        /// <param name="type">GameEventType</param>
        /// <param name="message">Message sent to GameEventBus</param>
        /// <param name="stringArg">StringArg sent to GameEventBus</param>
        public static void TriggerEvent(GameEventType type, string message = "", string stringArg = "", int intArg = 0, object objArg = null) {
            GetBus().RegisterEvent(new GameEvent {
                EventType = type,
                Message = message,
                StringArg1 = stringArg,
                IntArg1 = intArg,
                ObjectArg1 = objArg
            });
        }

        public static void TriggerTimedEvent(GameEventType type, float seconds, string message = "", string stringArg = "", int intArg = 0, object objArg = null) {
            GetBus().RegisterTimedEvent(new GameEvent {
                EventType = type,
                Message = message,
                StringArg1 = stringArg,
                IntArg1 = intArg,
                ObjectArg1 = objArg
            }, TimePeriod.NewSeconds(seconds));
        }

    }

}

