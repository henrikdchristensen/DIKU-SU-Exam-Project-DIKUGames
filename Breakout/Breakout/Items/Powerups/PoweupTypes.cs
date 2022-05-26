﻿namespace Breakout.Items.Powerups {

    /// <summary>
    /// 
    /// </summary>
    public enum PowerupType {
        ExtraLife,
        DoubleSpeed,
        PlayerSpeed,
        //Invincible,
        Wide,
        DoubleSize,
        //Infinite,
        //Rocket,
        //GravityBall,
        //Split,
        HardBall
    }

    public class PowerupTransformer {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static PowerupType TransformStringToState(string state) {
            switch (state) {
                case "PLAYER_SPEED":
                    return PowerupType.PlayerSpeed;
                case "EXTRA_LIFE":
                    return PowerupType.ExtraLife;
                case "WIDE":
                    return PowerupType.Wide;
                case "HARD_BALL":
                    return PowerupType.HardBall;
                case "DOUBLE_SPEED":
                    return PowerupType.DoubleSpeed;
                case "DOUBLE_SIZE":
                    return PowerupType.DoubleSize;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string TransformStateToString(PowerupType state) {
            switch (state) {
                case PowerupType.PlayerSpeed:
                    return "PLAYER_SPEED";
                case PowerupType.ExtraLife:
                    return "EXTRA_LIFE";
                case PowerupType.Wide:
                    return "WIDE";
                case PowerupType.HardBall:
                    return "HARD_BALL";
                case PowerupType.DoubleSpeed:
                    return "DOUBLE_SPEED";
                case PowerupType.DoubleSize:
                    return "DOUBLE_SIZE";

                default:
                    throw new ArgumentException();
            }
        }

    }

}
