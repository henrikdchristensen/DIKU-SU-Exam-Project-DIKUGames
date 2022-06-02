namespace Breakout.Items.Powerups {

    /// <summary>
    /// TODO: ADD SUMMARY AND REMOVE Uncommented code
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
        Split,
        HardBall
    }

    public class PowerupTransformer {

        /// <summary>Converts a String to a PowerupType
        /// 
        /// </summary>
        /// <param name="state">A string to be converted to a PowerupType</param>
        /// <returns>A PowerupType</returns>
        /// <exception cref="ArgumentException"></exception>
        public static PowerupType StringToState(string state) {
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

        /// <summary>Converts a PowerupType to a String</summary>
        /// <param name="state">A PowerupType to be converted to a String</param>
        /// <returns>A String</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string StateToString(PowerupType state) {
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
