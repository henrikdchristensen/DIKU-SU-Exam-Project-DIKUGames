using Breakout.Collision;
using DIKUArcade.Physics;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout.Game;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace Breakout.Items.Powerups {

    public class Powerup : GameObject {

        private const float SPEED = 0.01f;
        private double duration;
        private const float SIZE = 0.06f;

        private PowerupType type;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="image"></param>
        public Powerup (PowerupType type, float duration, DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.type = type;
            this.duration = duration;
            shape.ChangeDirection(new Vec2F(0, -SPEED));
            shape.Extent.X = shape.Extent.Y = SIZE;
        }

        public override void Update() {
            Shape.Move();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="data"></param>
        public override void Accept(GameObject other, CollisionData data) {
            other.PowerUpCollision(this, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="data"></param>
        public override void PlayerCollision(Player player, CollisionData data) {
            GameBus.GetBus().RegisterEvent(new GameEvent {
                Message = "POWERUP_ACTIVATE",
                EventType = GameEventType.ControlEvent,
                StringArg1 = PowerupTransformer.TransformStateToString(type)
            });
            if (duration > 0) {
                GameBus.GetBus().RegisterTimedEvent(new GameEvent {
                    Message = "POWERUP_DEACTIVATE",
                    EventType = GameEventType.ControlEvent,
                    StringArg1 = PowerupTransformer.TransformStateToString(type)
                }, TimePeriod.NewSeconds(duration));
            }
            

            DeleteEntity(); 
        }

    }

}
