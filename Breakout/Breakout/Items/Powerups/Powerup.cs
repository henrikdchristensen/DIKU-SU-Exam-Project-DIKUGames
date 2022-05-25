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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="image"></param>
        public Powerup (DynamicShape shape, IBaseImage image) : base(shape, image) {
            shape.ChangeDirection(new Vec2F(0, -SPEED));
        }

        public abstract void Apply(GameObject obj);

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
