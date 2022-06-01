namespace Breakout.Items {

    /// <summary>TODO</summary>
    public enum MetaType {
        BlockHardened,
        BlockUnbreakable,
        Time,
        PowerUp
    }

    public class MetaTransformer {

        /// <summary>TODO</summary>
        /// <param name="state">TODO</param>
        /// <returns>TODO</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string TransformStateToString(MetaType state) {
            switch (state) {
                case MetaType.BlockHardened:
                    return "Hardened";
                case MetaType.BlockUnbreakable:
                    return "Unbreakable";
                case MetaType.Time:
                    return "Time";
                case MetaType.PowerUp:
                    return "PowerUp";
                default:
                    throw new ArgumentException();
            }
        }

    }

}
