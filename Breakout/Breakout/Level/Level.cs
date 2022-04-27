namespace Breakout.Level
{
    public class Level {
        public char[,] Map {get;}
        public Dictionary<string, string> Meta {get;}
        public Dictionary<string, string> Legend {get;}
        
        public Level (char[,] map, Dictionary<string, string> meta, Dictionary<string, string> legend) {
            Map = map;
            Meta = meta;
            Legend = legend;
        }

        public override bool Equals(object obj) {
            Level? other = obj as Level;

            if (other == null ||
                    other.Map.Length != Map.Length ||
                    other.Meta.Count != Meta.Count || !other.Meta.Except(Meta).Any() ||
                    other.Legend.Count != Legend.Count || !other.Legend.Except(Legend).Any()) 
                return false;

            for (int i = 0; i < Map.GetLength(0); i++) 
                for (int j = 0; j < Map.GetLength(1); j++) 
                    if (Map[i, j] != other.Map[i, j])
                        return false;
                
            return true;
        }

        public override string ToString() {
            string str = "{{ ";
            for (int i = 0; i < Map.GetLength(0); i++) {
                for (int j = 0; j < Map.GetLength(1); j++) {
                    if (j == 0)
                        str += Map[i, j];
                    else
                        str += Map[i, j];
                }
                str += " }, { ";
            }
            return str + "}}";
        }

    }
}