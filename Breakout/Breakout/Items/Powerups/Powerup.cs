using DIKUArcade.Physics;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout.Game;

namespace Breakout.Items.Powerups {

    public class Powerup : GameObject {

        private const float SPEED = 0.01f;
        private double duration;
        private const float SIZE = 0.06f;
        private PowerupType type;

        /// <summary>Creates an random powerup</summary>
        /// <param name="shape">The shape of the generated power-up</param>
        /// <returns>A new random power-up</returns>
        public static Powerup CreateRandom(DynamicShape shape) {
            Random random = new Random();
            Type type = typeof(PowerupType);
            Array values = type.GetEnumValues();
            int index = random.Next(values.Length);
            PowerupType value = (PowerupType) values.GetValue(index);

            switch (value) {
                case PowerupType.ExtraLife:
                    return new Powerup(value, -1, shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "LifePickUp.png")));
                case PowerupType.Wide:
                    return new Powerup(value, 5, shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "WidePowerUp.png")));
                case PowerupType.PlayerSpeed:
                    return new Powerup(value, 5,shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "DoubleSpeedPowerUp.png")));
                case PowerupType.DoubleSpeed:
                    return new Powerup(value, 5, shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "SpeedPickUp.png")));
                case PowerupType.DoubleSize:
                    return new Powerup(value, 5, shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "BigPowerUp.png")));
                case PowerupType.HardBall:
                    return new Powerup(value, 5, shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ExtraBallPowerUp.png")));
            }
            return null;
        }

        /// <summary>Construtor for Powerup: Sets up e.g. the direction and duration of the powerup</summary>
        /// <param name="shape">The shape of the powerup</param>
        /// <param name="image">Image which should be used for the powerup</param>
        public Powerup (PowerupType type, float duration, DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.type = type;
            this.duration = duration;
            shape.ChangeDirection(new Vec2F(0, -SPEED));
            shape.Extent.X = shape.Extent.Y = SIZE;
        }

        /// <summary>Move the shape</summary>
        public override void Update() {
            Shape.Move();
        }

        /// <summary>
        /// Accepts an incomming GameObject and transfer the instance itself to that one.
        /// (Visitor Pattern)
        /// </summary>
        /// <param name="other">The incomming GameObject</param>
        /// <param name="data">Collision data passed along with the GameObject</param>
        public override void Accept(GameObject other, CollisionData data) {
            other.PowerUpCollision(this, data);
        }

        /// <summary>Activate/Deactivate powerups after collided (recieved) by the player</summary>
        /// <param name="player">A Player instance</param>
        /// <param name="data">Collision data passed along with the Player</param>
        public override void PlayerCollision(Player player, CollisionData data) {
            GameBus.TriggerEvent(GameEventType.ControlEvent, "POWERUP_ACTIVATE", PowerupTransformer.StateToString(type));
            if (duration > 0) {
                GameBus.GetBus().RegisterTimedEvent(new GameEvent {
                    Message = "POWERUP_DEACTIVATE",
                    EventType = GameEventType.ControlEvent,
                    StringArg1 = PowerupTransformer.StateToString(type)
                }, TimePeriod.NewSeconds(duration));
            }
            DeleteEntity(); 
        }

    }

}