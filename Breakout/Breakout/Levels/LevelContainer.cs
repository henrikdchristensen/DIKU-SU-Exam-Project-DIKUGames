using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Levels {
    /// <summary>
    /// Creates all levels inside of the level-folder
    /// </summary>
    public class LevelContainer : IGameEventProcessor {

        public const string NEXT_LEVEL_MSG = "NEXT_LEVEL";

        // Singleton pattern
        private static LevelContainer instance = null;
        private List<Level> levelList = new List<Level>();
        private LevelParser levelLoader;

        /// <summary> The active level </summary>
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

        /// <summary>Resets all levels</summary>
        public void Reset() {
            levelCounter = 0;
            ActiveLevel?.Deactivate();
            initializeLevels();
        }

        /// <summary>
        /// Initializes levels. The levels are hardcoded in a list and active level is set to the first element
        /// </summary>
        private void initializeLevels() {
            levelLoader = new LevelParser(new LevelLoader());
            // Initialize structure of levels
            createLevels(new List<string> {
                Path.Combine("Assets", "Levels", "level1.txt"),
                Path.Combine("Assets", "Levels", "level2.txt"),
                Path.Combine("Assets", "Levels", "level3.txt"),
                Path.Combine("Assets", "Levels", "level4.txt")
            });

            // Set initial active level
            NextLevel();
        }

        private void createLevels(List<string> levelPaths) {
            foreach (string path in levelPaths) {
                try {
                    Level level = levelLoader.CreateLevel(path);
                    levelList.Add(level);
                } catch (ArgumentException e) {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Incrementing the level. If last level is passed then the game returns to main menu.
        /// </summary>
        public void NextLevel() {
            if (levelCounter > 0)
                ActiveLevel.Deactivate();
            if (levelCounter <= levelList.Count - 1) {
                ActiveLevel = levelList[levelCounter++];
                ActiveLevel.Activate();
            } else {
                GameBus.TriggerEvent(GameEventType.StatusEvent, Player.PLAYER_DEAD_MSG);
            }
        }

        /// <summary>
        /// Process game events. Should react on next level events
        /// </summary>
        /// <param name="gameEvent">The game event to process</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch (gameEvent.Message) {
                    case NEXT_LEVEL_MSG:
                        NextLevel();
                        break;
                }
            }

        }
    }
}
