namespace Breakout.Entities {

    /// <summary>Enum containing the different Meta types</summary>
    public enum MetaType {
        BlockHardened,
        BlockUnbreakable,
        Time,
        PowerUp
    }

    public class MetaTransformer {

        /// <summary>Convert a MetaType to a String</summary>
        /// <param name="type">MetaType</param>
        /// <returns>The converted MetaType as String</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string StateToString(MetaType type) {
            switch (type) {
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