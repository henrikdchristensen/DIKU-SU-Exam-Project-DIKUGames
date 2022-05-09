namespace Breakout.Items {

    public enum BlockType {
        Hardened,
        Unbreakable
    }

    public class BlockTransformer {

        public static string TransformStateToString(BlockType state) {
            switch (state) {
                case BlockType.Hardened:
                    return "Hardened";
                case BlockType.Unbreakable:
                    return "Unbreakable";
                default:
                    throw new ArgumentException();
            }
        }
    }

}
