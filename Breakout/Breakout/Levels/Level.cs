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

        /// <summary>TODO</summary>
        /// <param name="map">TODO</param>
        /// <param name="meta">TODO</param> 
        /// <param name="legend">TODO</param>
        public Level(char[,] map, Dictionary<string, string> meta, Dictionary<string, string> legend) {
            Map = map;
            Meta = meta;
            Legend = legend;
            
            blockSize = new Vec2F(1f / Map.GetLength(1), 1f / Map.GetLength(0));

            generateBlocks();
        }

        /// <summary>TODO</summary>
        /// <param name="obj">TODO</param>
        public void AddGameObject(GameObject obj) {
            items.AddEntity(obj);
            CollisionHandler.GetInstance().Subsribe(obj);
        }



        /// <summary>TODO</summary>
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

        /// <summary>TODO</summary>
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

        /// <summary>TODO</summary>
        /// <param name="entities">TODO</param>
        /// <returns>TODO</returns>
        private EntityContainer<GameObject> DeleteMarkedEntity(EntityContainer<GameObject> entities)  {
            var newList = new EntityContainer<GameObject>();
            foreach (GameObject obj in entities) {
                if (!obj.IsDeleted()) {
                    newList.AddEntity(obj);
                }
                else {
                    obj.AtDeletion();
                }
            }
            return newList;
        }

        /// <summary>TODO</summary>
        public void Render() {
            items.RenderEntities();
            ballContainer.Render();
            renderTime();
        }

        /// <summary>TODO</summary>
        private void renderTime() {
            long time = (timeToEnd - StaticTimer.GetElapsedMilliseconds()) / 1000;
            display.SetText(time.ToString());
            display.RenderText();
        }

        /// <summary>TODO</summary>
        /// <returns>TODO</returns>
        private bool hasWon() {
            foreach (GameObject item in items) {
                if (item.IsDestroyable && !item.IsDeleted()) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>TODO</summary>
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

        /// <summary>TODO</summary>
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

        /// <summary>TODO</summary>
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

        /// <summary>TODO</summary>
        /// <returns>TODO</returns>
        public override string ToString() {
            string map = "map = {{";
            for (int i = 0; i < Map.GetLength(0); i++) {
                if (i != 0)
                    map += "}, {";
                for (int j = 0; j < Map.GetLength(1); j++) {
                    if (j == 0)
                        map += $"'{Map[i, j]}'";
                    else
                        map += $", '{Map[i, j]}'";
                }
            }
            map += "}}\n";

            string meta = "meta = {";
            foreach (KeyValuePair<string, string> pair in Meta)
                meta += $"{{\"{pair.Key}\", \"{pair.Value}\"}}, ";
            if (Meta.Count > 0)
                meta = meta.Remove(meta.Length - 2);
            meta += "}\n";

            string legend = "legend = {";
            foreach (KeyValuePair<string, string> pair in Legend)
                legend += $"{{\"{pair.Key}\", \"{pair.Value}\"}}, ";
            if (Legend.Count > 0)
                legend = legend.Remove(legend.Length - 2);
            legend += "}";

            return map + meta + legend;
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