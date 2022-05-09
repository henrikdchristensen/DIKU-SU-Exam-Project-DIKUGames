using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Items;

namespace Breakout.Levels
{
    public class Level {
        public char[,] Map {get;}
        public Dictionary<string, string> Meta {get;}
        public Dictionary<string, string> Legend {get;}

        public EntityContainer<Entity> entities {
            get; private set;
        } = new EntityContainer<Entity>();

        private Vec2F blockSize;

        public Level (char[,] map, Dictionary<string, string> meta, Dictionary<string, string> legend) {
            Map = map;
            Meta = meta;
            Legend = legend;

            blockSize = new Vec2F(1f / Map.GetLength(1), 1f / Map.GetLength(0));

            generateBlocks();

        }

        public void Update() {
            //Delete entities that have been marked for deletion
            var newList = new EntityContainer<Entity>();
            foreach (Entity obj in entities) {
                if (!obj.IsDeleted()) {
                    newList.AddEntity(obj);
                }
            }
            entities = newList;
        }

        public void Render() {
            entities.RenderEntities();
        }

        private void generateBlocks() {
            for (int i = 0; i < Map.GetLength(0); i++) {
                for (int j = 0; j < Map.GetLength(1); j++) {
                    string c = Map[i, j] + "";
                    if (Legend.ContainsKey(Map[i, j] + "")) {
                        entities.AddEntity(
                            new Block(
                                new StationaryShape(j * blockSize.X, 1 - (i * blockSize.Y + blockSize.Y), blockSize.X, blockSize.Y),
                                new Image(Path.Combine("..", "Breakout", "Assets", "Images", Legend[c]))));
                    }
                
                }
            }
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