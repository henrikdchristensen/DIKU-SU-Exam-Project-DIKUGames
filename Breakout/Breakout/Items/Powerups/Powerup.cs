using Breakout.Collision;
using DIKUArcade.Physics;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout.Game;
using DIKUArcade.Events;

namespace Breakout.Items.Powerups {

    public class Powerup : Item {

        private PowerupType type;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="image"></param>
        public Powerup (DynamicShape shape, IBaseImage image) : base(shape, image) {
            Array values = Enum.GetValues(typeof(PowerupType));
            Random random = new Random();
            type = (PowerupType) values.GetValue(random.Next(values.Length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="data"></param>
        public override void Accept(ICollidable other, CollisionData data) {
            other.PowerUpCollision(this, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="data"></param>
        public override void PlayerCollision(Player player, CollisionData data) {
            GameBus.GetBus().RegisterEvent(new GameEvent {
                Message = type.ToString(),
                EventType = GameEventType.TimedEvent
            });

            DeleteEntity(); 
        }

    }

}
