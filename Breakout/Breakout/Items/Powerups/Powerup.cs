using Breakout.Collision;
using DIKUArcade.Physics;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout.Game;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace Breakout.Items.Powerups {

    public abstract class Powerup : GameObject {

        private const float SPEED = 0.01f;
        protected double duration = -1;
        private const float SIZE = 0.08f;

        public abstract PowerupType TAG { get; }

        public static void HandlePowerup(GameObject obj, Powerup powerup) {
            if (powerup.duration <= 0)
                powerup.Activate(obj);
            else {
                Console.WriteLine("TEST1");
                bool otherExists = doesOtherPowerupExist(powerup, obj.Active);
                if (obj.Active.Contains(powerup)) {
                    obj.Active.Remove(powerup);
                    if (!otherExists)
                        powerup.Deactivate(obj);
                } else {
                    obj.Active.Add(powerup);
                    if (!otherExists) {
                        Console.WriteLine("TEST2");
                        powerup.Activate(obj);
                    }
                        
                }
            }
        }
        private static bool doesOtherPowerupExist(Powerup powerup, List<Powerup> list) {
            foreach (Powerup other in list) {
                if (other != powerup && other.TAG == powerup.TAG) {
                    return true;
                }
            }
            return false;
        }

        public static Powerup CreateRandom(DynamicShape shape) {
            Random random = new Random();
            Type type = typeof(PowerupType);
            Array values = type.GetEnumValues();
            int index = random.Next(values.Length);
            PowerupType value = (PowerupType) values.GetValue(index);
            switch (value) {
                case PowerupType.ExtraLife:
                    return new ExtraLife(shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "LifePickUp.png")));
                case PowerupType.Wide:
                    return new Wide(shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "WidePowerUp.png")));
                case PowerupType.DoubleSpeed:
                    return new DoubleSpeed(shape, new Image(Path.Combine("..", "Breakout", "Assets", "Images", "SpeedPickUp.png")));
                default:
                    return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="image"></param>
        public Powerup (DynamicShape shape, IBaseImage image) : base(shape, image) {
            shape.ChangeDirection(new Vec2F(0, -SPEED));
            shape.Extent.X = shape.Extent.Y = SIZE;
        }

        public abstract void Activate(GameObject obj);
        public abstract void Deactivate(GameObject obj);

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
            Console.WriteLine("POWERUP ADDED TO EVENTBUS");
            GameBus.GetBus().RegisterEvent(new GameEvent {
                Message = "POWERUP",
                EventType = GameEventType.ControlEvent,
                ObjectArg1 = this
            });
            if (duration > 0) {
                Console.WriteLine("TIMED POWERUP ADDED TO EVENTBUS");
                GameBus.GetBus().RegisterTimedEvent(new GameEvent {
                    Message = "POWERUP",
                    EventType = GameEventType.ControlEvent,
                    ObjectArg1 = this
                }, TimePeriod.NewSeconds(duration));
            }
            

            DeleteEntity(); 
        }

    }

}
