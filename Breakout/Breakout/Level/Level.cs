using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Items;
using Breakout;
using Breakout.Input;
using Breakout.Collision;
using System.Diagnostics;

namespace Breakout.Levels {
    public class Level {
        private Text display;
        private Stopwatch stopwatch = new Stopwatch();
        private int timeToEnd;
        public char[,] Map {
            get;
        }
        public Dictionary<string, string> Meta {
            get;
        }
        public Dictionary<string, string> Legend {
            get;
        }

        private EntityContainer<Item> items = new EntityContainer<Item>();
        private int balls = 0;

        private Vec2F blockSize;

        public Level(char[,] map, Dictionary<string, string> meta, Dictionary<string, string> legend) {
            Map = map;
            Meta = meta;
            Legend = legend;
            
            blockSize = new Vec2F(1f / Map.GetLength(1), 1f / Map.GetLength(0));

            string time = MetaTransformer.TransformStateToString(MetaType.Time);
            if (Meta.ContainsKey(time)) {
                try {
                    timeToEnd = Int32.Parse(Meta[time]);
                    stopwatch.Start();
                    Console.WriteLine("TIME ADDED: {0}", timeToEnd);
                    if (timeToEnd > 0) {
                        display = new Text((timeToEnd-stopwatch.ElapsedMilliseconds/1000).ToString(), new Vec2F(0.75f, 0.5f), new Vec2F(0.6f, 0.5f));
                        display.SetColor(new Vec3F(1f, 1f, 1f));
                    }
                } catch (FormatException) {
                    Console.WriteLine($"Unable to parse meta time '{Meta[time]}'");
                }
            }

            generateBlocks();
        }

        private void spawnBall() {
            balls++;
            Ball ball = new Ball(
                new DynamicShape(0.5f, 0.1f, 0.03f, 0.03f),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            CollisionHandler.GetInstance().Subsribe(ball);
            items.AddEntity(ball);
        }

        public void OnBallDeletion() {
            balls--;
            if (balls <= 0)
                spawnBall();
        }



        public void Activate() {
            items.Iterate(CollisionHandler.GetInstance().Subsribe);
            spawnBall();
        }

        public void Update() {
            //Delete entities that have been marked for deletion
            items = DeleteMarkedEntity(items);

            if (hasWon()) {
                Destroy();
                LevelContainer.GetLevelContainer().NextLevel();
                return;
            }

            foreach (Item item in items)
                item.Update();
        }

        private EntityContainer<Item> DeleteMarkedEntity(EntityContainer<Item> entities)  {
            var newList = new EntityContainer<Item>();
            foreach (Item obj in entities) {
                if (!obj.IsDeleted()) {
                    newList.AddEntity(obj);
                }
                else {
                    obj.AtDeletion(this);
                }
            }
            return newList;
        }

        public void Render() {
            items.RenderEntities();
            RenderTime();
        }

        private void RenderTime() {
            if (stopwatch.ElapsedMilliseconds / 1000 < timeToEnd) {
                display.SetText((timeToEnd - stopwatch.ElapsedMilliseconds / 1000).ToString());
                display.RenderText();
            }
        }

        private bool hasWon() {
            foreach (Item item in items) {
                if (item.IsDestroyable && !item.IsDeleted()) {
                    return false;
                }
            }
            return true;
        }

        private void generateBlocks() {
            for (int i = 0; i < Map.GetLength(0); i++) {
                for (int j = 0; j < Map.GetLength(1); j++) {
                    string c = Map[i, j] + "";
                    if (Legend.ContainsKey(c)) {

                        Block block = chooseBlock(c, new StationaryShape(j * blockSize.X, 1 - (i * blockSize.Y + blockSize.Y), blockSize.X, blockSize.Y));
                        items.AddEntity(block);
                    }

                }
            }
        }

        public void Destroy() {
            foreach (Item item in items)
                item.DeleteEntity();
        }

        public void DeleteBlock() {
            foreach (Item item in items) {
                if (item is Block) {
                    item.DeleteEntity();
                    break;
                }  
            }
        }

        private Block chooseBlock(string symbol, StationaryShape shape) {
            var img = new Image(Path.Combine("..", "Breakout", "Assets", "Images", Legend[symbol]));
            var dmg = new Image(Path.Combine("..", "Breakout", "Assets", "Images", Legend[symbol].Replace(".png", "-damaged.png")));

            string hardened = MetaTransformer.TransformStateToString(MetaType.BlockHardened);
            if (Meta.ContainsKey(hardened) && Meta[hardened] == symbol) {
                Console.WriteLine("HARDENED ADDED");
                return new HardenedBlock(shape, img, dmg);
            }


            string unbreakable = MetaTransformer.TransformStateToString(MetaType.BlockUnbreakable);
            if (Meta.ContainsKey(unbreakable) && Meta[unbreakable] == symbol)
                return new Unbreakable(shape, img);

            return new Block(shape, img);

        }

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

    }
}