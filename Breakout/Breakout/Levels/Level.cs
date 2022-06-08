using System.Diagnostics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;
using Breakout.Entities;
using Breakout.Collision;
using Breakout.Entities.Powerups;
using DIKUArcade.Timers;

namespace Breakout.Levels {
    /// <summary>
    /// A level of the game
    /// </summary>
    public class Level : IGameEventProcessor {

        public const string ADD_GAMEOBJECT_MSG = "ADD_GAMOBJECT_MSG";

        private Text display;
        private long timeToEnd = -1;
        private readonly Vec3F COLOR = new Vec3F(247f / 255f, 145f / 255f, 0);
        public char[,] Map { get; }
        public Dictionary<string, string> Meta { get; }
        public Dictionary<string, string> Legend { get; }
        private EntityContainer<GameObject> items = new EntityContainer<GameObject>();
        private BallContainer ballContainer;
        private Vec2F blockSize;

        /// <summary>Creates a level-instance</summary>
        /// <param name="map">The level map represented as a 2d-char-array</param>
        /// <param name="meta">All meta-data represented as a dictionary</param> 
        /// <param name="legend">All legend-data represend as a dictionary</param>
        public Level(char[,] map, Dictionary<string, string> meta, Dictionary<string, string> legend) {
            Map = map;
            Meta = meta;
            Legend = legend;
            
            blockSize = new Vec2F(1f / Map.GetLength(1), 1f / Map.GetLength(0));

            generateBlocks();
        }

        /// <summary>Add a gameobject to the level, which wil be rendered and updated each frame</summary>
        /// <param name="obj">The gamobject to be added</param>
        public void AddGameObject(GameObject obj) {
            items.AddEntity(obj);
            CollisionHandler.GetInstance().Subsribe(obj);
        }



        /// <summary>This method should be call when the level needs to be activated</summary>
        public void Activate() {
            items.Iterate(CollisionHandler.GetInstance().Subsribe);
            ballContainer = new BallContainer();
            GameBus.GetBus().Subscribe(GameEventType.ControlEvent, ballContainer);
            GameBus.GetBus().Subscribe(GameEventType.ControlEvent, this);

            //Start timer if field is set in metadata
            string time = MetaTransformer.StateToString(MetaType.Time);
            if (Meta.ContainsKey(time)) {
                long startTime = StaticTimer.GetElapsedMilliseconds();
                //Converts seconds to milliseconds
                timeToEnd = int.Parse(Meta[time]) * 1000 + startTime; 
                if (timeToEnd > 0) {
                    display = new Text($"{timeToEnd}", new Vec2F(0.75f, 0.5f), new Vec2F(0.6f, 0.5f));
                    display.SetColor(COLOR);
                }
            
            }
        }

        /// <summary>Update the level. This includes updating all gameobjects: ball, blocks etc</summary>
        public void Update() {
            //Delete entities that have been marked for deletion
            items = DeleteMarkedEntity(items);

            foreach (GameObject item in items)
                item.Update();

            ballContainer.Update();

            if (hasWon()) {
                nextLevel();
            } else if (hasTimeExceeded()) {
                gameOver();
            }
        }

        /// <summary>Deletes all gameobjects marked for deletion</summary>
        /// <param name="entities">The list of gameobjects, that should be looped through</param>
        /// <returns>The new list wihout gameobjects marked for deletion</returns>
        private EntityContainer<GameObject> DeleteMarkedEntity(EntityContainer<GameObject> entities)  {
            var newList = new EntityContainer<GameObject>();
            foreach (GameObject obj in entities) {
                if (!obj.IsDeleted()) {
                    newList.AddEntity(obj);
                }
                else {
                    obj.OnDeletion();
                }
            }
            return newList;
        }

        /// <summary>Renders the level</summary>
        public void Render() {
            items.RenderEntities();
            ballContainer.Render();
            renderTime();
        }

        /// <summary>Renders the time</summary>
        private void renderTime() {
            long time = (timeToEnd - StaticTimer.GetElapsedMilliseconds()) / 1000;
            display.SetText(time.ToString());
            display.RenderText();
        }

        /// <summary>check if the player has won</summary>
        /// <returns>returns true if the player has won and otherwise false</returns>
        private bool hasWon() {
            foreach (GameObject item in items) {
                if (item.IsDestroyable && !item.IsDeleted()) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>generates all block</summary>
        private void generateBlocks() {
            for (int i = 0; i < Map.GetLength(0); i++) {
                for (int j = 0; j < Map.GetLength(1); j++) {
                    string c = Map[i, j] + "";
                    if (Legend.ContainsKey(c)) {
                        Block block = chooseBlock(c,
                            new StationaryShape(j * blockSize.X, 1 - (i * blockSize.Y + blockSize.Y), blockSize.X, blockSize.Y));
                        items.AddEntity(block);
                    }
                }
            }
        }

        /// <summary>Deativated the level. Marks all gameobjects for deletion.</summary>
        public void Deactivate() {
            GameBus.GetBus().Unsubscribe(GameEventType.ControlEvent, this);
            ballContainer.Destroy();
            foreach (GameObject item in items)
                item.DeleteEntity();
        }

        /// <summary>TODO: SHOULD BE DELETED</summary>
        public void DeleteBlock() {
            foreach (GameObject item in items) {
                if (item is Block) {
                    item.DeleteEntity();
                    break;
                }  
            }
        }

        /// <summary>TODO</summary>
        /// <param name="symbol">TODO</param>
        /// <param name="shape">TODO</param>
        /// <returns>TODO</returns>
        private Block chooseBlock(string symbol, StationaryShape shape) {
            var img = new Image(Path.Combine("..", "Breakout", "Assets", "Images", Legend[symbol]));
            var dmg = new Image(Path.Combine("..", "Breakout", "Assets", "Images", Legend[symbol].Replace(".png", "-damaged.png")));

            string hardened = MetaTransformer.StateToString(MetaType.BlockHardened);
            if (Meta.ContainsKey(hardened) && Meta[hardened] == symbol) 
                return new HardenedBlock(shape, img, dmg);
            string powerup = MetaTransformer.StateToString(MetaType.PowerUp);
            if (Meta.ContainsKey(powerup) && Meta[powerup] == symbol) {
                return new PowerupBlock(shape, img);
            }
            string unbreakable = MetaTransformer.StateToString(MetaType.BlockUnbreakable);
            if (Meta.ContainsKey(unbreakable) && Meta[unbreakable] == symbol)
                return new Unbreakable(shape, img);

            return new Block(shape, img);
        }

        /// <summary>
        /// For testing purposes
        /// </summary>
        /// <returns></returns>
        public int CountItems() {
            return items.CountEntities();
        }

        private bool hasTimeExceeded() {
            return timeToEnd != -1 && StaticTimer.GetElapsedMilliseconds() >= timeToEnd;
        }

        private void gameOver() {
            GameBus.TriggerEvent(GameEventType.StatusEvent, Player.PLAYER_DEAD_MSG);
        }

        private void nextLevel() {
            GameBus.TriggerEvent(GameEventType.StatusEvent, LevelContainer.NEXT_LEVEL_MSG);
        }

        /// <summary>For testing purposes</summary>
        /// <param name="obj">TODO</param>
        /// <returns>TODO</returns>
        public override bool Equals(object obj) {
            Level? other = obj as Level;
            if (other == null ||
                    other.Map.Length != Map.Length ||
                    other.Meta.Count != Meta.Count || other.Meta.Except(Meta).Any() ||
                    other.Legend.Count != Legend.Count || other.Legend.Except(Legend).Any())
                return false;
            for (int i = 0; i < Map.GetLength(0); i++)
                for (int j = 0; j < Map.GetLength(1); j++)
                    if (Map[i, j] != other.Map[i, j])
                        return false;
            return true;
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                switch (gameEvent.Message) {
                    case ADD_GAMEOBJECT_MSG:
                        AddGameObject((GameObject) gameEvent.ObjectArg1);
                        break;
                }
            }
        }
    }

}