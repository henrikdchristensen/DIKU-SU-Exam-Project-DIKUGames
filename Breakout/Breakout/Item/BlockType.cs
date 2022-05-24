namespace Breakout.Items {

    public enum BlockType {
        Hardened,
        Unbreakable,
        Powerup
    }

    public class BlockTransformer {

        public static string TransformStateToString(BlockType state) {
            switch (state) {
                case BlockType.Hardened:
                    return "Hardened";
                case BlockType.Unbreakable:
                    return "Unbreakable";
                case BlockType.Powerup:
                    return "Powerup";
                default:
                    throw new ArgumentException();
            }
        }
    }

}
