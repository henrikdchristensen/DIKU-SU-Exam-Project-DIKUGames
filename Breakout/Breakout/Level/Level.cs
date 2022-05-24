using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Items;
using Breakout;
using Breakout.Input;
using Breakout.Collision;

namespace Breakout.Levels {
    public class Level {
        public char[,] Map {
            get;
        }
        public Dictionary<string, string> Meta {
            get;
        }
        public Dictionary<string, string> Legend {
            get;
        }

        private EntityContainer<Block> blocks = new EntityContainer<Block>();

        private EntityContainer<Ball> balls = new EntityContainer<Ball>();

        private Vec2F blockSize;

        public Level(char[,] map, Dictionary<string, string> meta, Dictionary<string, string> legend) {
            Map = map;
            Meta = meta;
            Legend = legend;

            blockSize = new Vec2F(1f / Map.GetLength(1), 1f / Map.GetLength(0));

            balls.AddEntity(new Ball(
                new DynamicShape(0.5f, 0.1f, 0.03f, 0.03f),
                new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png"))));

            generateBlocks();
        }

        public void Activate() {
            blocks.Iterate(CollisionHandler.GetInstance().Subsribe);
            balls.Iterate(CollisionHandler.GetInstance().Subsribe);
        }

        public void Update() {
            //Delete entities that have been marked for deletion
            blocks = DeleteMarkedEntity(blocks);
            balls = DeleteMarkedEntity(balls);

            if (isAllBlocksDestroyed()) {
                Destroy();
                LevelContainer.GetLevelContainer().NextLevel();
                return;
            }

            foreach (Ball b in balls)
                b.Move();
        }

        private EntityContainer<T> DeleteMarkedEntity<T>(EntityContainer<T> entities) where T : Entity {
            var newList = new EntityContainer<T>();
            foreach (T obj in entities) {
                if (!obj.IsDeleted()) {
                    newList.AddEntity(obj);
                }
            }
            return newList;
        }

        public void Render() {
            blocks.RenderEntities();
            balls.RenderEntities();
        }

        private bool isAllBlocksDestroyed() {
            foreach (Entity obj in blocks) {
                if (obj is not Unbreakable && !obj.IsDeleted()) {
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
                        blocks.AddEntity(block);
                    }

                }
            }
        }

        public void Destroy() {
            foreach (Block b in blocks)
                b.DeleteEntity();
            foreach (Ball b in balls)
                b.DeleteEntity();
        }

        private Block chooseBlock(string symbol, StationaryShape shape) {
            var img = new Image(Path.Combine("..", "Breakout", "Assets", "Images", Legend[symbol]));
            var dmg = new Image(Path.Combine("..", "Breakout", "Assets", "Images", Legend[symbol].Replace(".png", "-damaged.png")));

            string hardened = BlockTransformer.TransformStateToString(BlockType.Hardened);
            if (Meta.ContainsKey(hardened) && Meta[hardened] == symbol) {
                Console.WriteLine("HARDENED ADDED");
                return new HardenedBlock(shape, img, dmg);
            }

            string unbreakable = BlockTransformer.TransformStateToString(BlockType.Unbreakable);
            if (Meta.ContainsKey(unbreakable) && Meta[unbreakable] == symbol)
                return new Unbreakable(shape, img);


            string powerup = BlockTransformer.TransformStateToString(BlockType.Powerup);
            if (Meta.ContainsKey(powerup) && Meta[unbreakable] == symbol)
                return new PowerupBlock(shape, img, dmg);

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