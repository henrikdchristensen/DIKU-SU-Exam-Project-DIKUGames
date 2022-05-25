﻿namespace Breakout.Items {

    /// <summary>
    /// 
    /// </summary>
    public enum MetaType {
        BlockHardened,
        BlockUnbreakable,
        Time,
        PowerUp
    }

    public class MetaTransformer {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
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
