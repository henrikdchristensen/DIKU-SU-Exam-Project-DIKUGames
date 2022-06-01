using DIKUArcade.Events;
using Breakout.Game;


namespace Breakout.Levels {

    public class LevelContainer {

        // Singleton pattern
        private static LevelContainer instance = null;
        private List<Level> levelList;
        private LevelParser levelLoader;
        public Level ActiveLevel { get; private set; }
        private int levelCounter { get; set; } = 0;
        private LevelContainer() { }

        /// <summary>
        /// Singleton pattern used to make sure only one LevelContainer exists.
        /// </summary>
        /// <returns>Existing LevelContainer or new LevelContainer</returns>
        public static LevelContainer GetLevelContainer() {
            return LevelContainer.instance ?? (LevelContainer.instance = new LevelContainer());
        }

        /// <summary>TODO</summary>
        public void Reset() {
            levelCounter = 0;
            ActiveLevel?.Destroy();
            InitializeLevels();
        }

        /// <summary>
        /// Initializes levels. The levels are hardcoded in a list and active level is set to the first element
        /// </summary>
        private void InitializeLevels() {
            levelLoader = new LevelParser(new LevelLoader());
            // Initialize structure of levels
            levelList = new List<Level> {
                //levelLoader.CreateLevel(Path.Combine("Assets", "Levels", "level1.txt")),
                //levelLoader.CreateLevel(Path.Combine("Assets", "Levels", "level2.txt")),
                levelLoader.CreateLevel(Path.Combine("Assets", "Levels", "level3.txt")),
                levelLoader.CreateLevel(Path.Combine("Assets", "Levels", "level4.txt"))
            };
            // Set initial active level
            NextLevel();
        }


        /// <summary>
        /// Incrementing the level. If last level is passed then the game returns to main menu.
        /// </summary>
        public void NextLevel() {
            if (levelCounter <= levelList.Count - 1) {
                ActiveLevel = levelList[levelCounter++];
                ActiveLevel.Activate();
            } else {
                GameBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = StateTransformer.StateToString(GameStateType.MainMenu)
                });
            }
        }

        /// <summary>
        /// Can set an active level given a level file name 
        /// </summary>
        /// <param name="activeLevel">String with the name of level file</param>
        public void SetActiveLevel(string activeLevel) {
            ActiveLevel = levelLoader.CreateLevel(Path.Combine("Assets", "Levels", activeLevel));
        }

        /// <summary>
        /// Reset the LevelContainer.
        /// </summary>
        public void ResetLevelContainer() {
            InitializeLevels();
        }

    }
}
