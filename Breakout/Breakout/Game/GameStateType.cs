namespace Breakout.Game {

    /// <summary>
    /// 
    /// </summary>
    public enum GameStateType {
        GameRunning,
        GamePaused,
        MainMenu
    }

    /// <summary>
    /// 
    /// </summary>
    public class StateTransformer {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static GameStateType TransformStringToState(string state) {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string TransformStateToString(GameStateType state) {
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