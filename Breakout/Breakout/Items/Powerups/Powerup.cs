using DIKUArcade.Physics;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout.Game;
using Breakout.Collision;
using Breakout.Items;

namespace Breakout.Items.Powerups {

    public abstract class Powerup : GameObject {

        public const string CAN_ACTIVATE_MSG = "CAN_POWERUP_ACTIVATE";
        public const string CAN_DEACTIVATE_MSG = "CAN_POWERUP_DEACTIVATE";

        private const float SPEED = 0.01f;
        public float Duration { get; } = -1;
        private const float SIZE = 0.06f;
        public abstract PowerupType Type { get; }

        /// <summary>Creates an random powerup</summary>
        /// <param name="shape">The shape of the generated power-up</param>
        /// <returns>A new random power-up</returns>
        public static Powerup CreateRandom(DynamicShape shape) {
            Random random = new Random();
            Type type = typeof(PowerupType);
            Array values = type.GetEnumValues();
            int index = random.Next(values.Length);
            PowerupType value = (PowerupType) values.GetValue(index);
            string imgPath = Path.Combine("..", "Breakout", "Assets", "Images");

            switch (value) {
                case PowerupType.ExtraLife:
                    return new ExtraLife(shape, new Image(Path.Combine(imgPath, "LifePickUp.png")));
                case PowerupType.Wide:
                    return new Wide(shape, new Image(Path.Combine(imgPath, "WidePowerUp.png")));
                case PowerupType.PlayerSpeed:
                    return new PlayerSpeed(shape, new Image(Path.Combine(imgPath, "DoubleSpeedPowerUp.png")));
                case PowerupType.DoubleSpeed:
                    return new DoubleSpeed(shape, new Image(Path.Combine(imgPath, "SpeedPickUp.png")));
                case PowerupType.DoubleSize:
                    return new DoubleSize(shape, new Image(Path.Combine(imgPath, "BigPowerUp.png")));
                case PowerupType.HardBall:
                    return new HardBall(shape, new Image(Path.Combine(imgPath, "ExtraBallPowerUp.png")));
                case PowerupType.Split:
                    return new Split(shape, new Image(Path.Combine(imgPath, "SplitPowerUp.png")));
            }
            return null;
        }

        /// <summary>Construtor for Powerup: Sets up e.g. the direction and duration of the powerup</summary>
        /// <param name="shape">The shape of the powerup</param>
        /// <param name="image">Image which should be used for the powerup</param>
        public Powerup (DynamicShape shape, IBaseImage image, float duration = -1) : base(shape, image) {
            this.Duration = duration;
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
        public override void Accept(IGameObjectVisitor other, CollisionHandlerData data) {
            other.PowerUpCollision(data);
        }


        /// <summary>Activate/Deactivate powerups after collided (recieved) by the player</summary>
        /// <param name="player">A Player instance</param>
        /// <param name="data">Collision data passed along with the Player</param>
        public override void PlayerCollision(CollisionHandlerData data) {
            GameBus.TriggerEvent(GameEventType.ControlEvent, CAN_ACTIVATE_MSG, objArg: this);
            if (Duration > 0) 
                GameBus.TriggerTimedEvent(GameEventType.ControlEvent, Duration, CAN_DEACTIVATE_MSG, objArg: this);

            DeleteEntity(); 
        }

        public abstract void Activate();

        public abstract void Deactivate();

    }

}