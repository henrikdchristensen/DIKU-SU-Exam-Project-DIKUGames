namespace Breakout.Items {

    public enum MetaType {
        BlockHardened,
        BlockUnbreakable,
        Time
    }

    public class MetaTransformer {

        public static string TransformStateToString(MetaType state) {
            switch (state) {
                case MetaType.BlockHardened:
                    return "Hardened";
                case MetaType.BlockUnbreakable:
                    return "Unbreakable";
                case MetaType.Time:
                    return "Time";
                default:
                    throw new ArgumentException();
            }
        }
    }

}
