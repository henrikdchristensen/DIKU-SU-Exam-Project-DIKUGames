namespace Breakout.Game {

    /// <summary>Enum for different Game States</summary>
    public enum GameStateType {
        GameRunning,
        GamePaused,
        MainMenu
    }

    /// <summary>
    /// 
    /// </summary>
    public class StateTransformer {

        /// <summary>Converts a String to GameStateType</summary>
        /// <param name="state">A string containing the GameStateType</param>
        /// <returns>GameStateType</returns>
        /// <exception cref="ArgumentException"></exception>
        public static GameStateType StringToState(string state) {
            switch (state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>Converts a GameStateType to String</summary>
        /// <param name="state">A GameStateType which should be converted to string</param>
        /// <returns>String</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string StateToString(GameStateType state) {
            switch (state) {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
                default:
                    throw new ArgumentException();
            }
        }

    }

}