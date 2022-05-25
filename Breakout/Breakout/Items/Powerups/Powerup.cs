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

        public abstract PowerupType TAG { get; }

        public static void HandlePowerup(GameObject obj, Powerup powerup) {
            //If the gameobject alredy have the powerup, it needs to be deactivated
            bool otherExists = doesOtherPowerupExist(powerup, obj.Active);
            if (obj.Active.Contains(powerup)) {
                obj.Active.Remove(powerup);
                if (!otherExists)
                    powerup.Deactivate(obj);
            } else {
                obj.Active.Add(powerup);
                if (!otherExists)
                    powerup.Activate(obj);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="image"></param>
        public Powerup (DynamicShape shape, IBaseImage image) : base(shape, image) {
            shape.ChangeDirection(new Vec2F(0, -SPEED));
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
