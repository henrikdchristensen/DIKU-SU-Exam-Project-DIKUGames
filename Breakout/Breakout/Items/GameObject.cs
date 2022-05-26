using Breakout.Collision;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Levels;
using Breakout.Items.Powerups;
using DIKUArcade.Events;

namespace Breakout.Items {

    public abstract class GameObject : Entity {
        public List<Powerup> Active { get; } = new List<Powerup>();


        public bool IsDestroyable { get;  protected set; } = false;
        public GameObject(Shape shape, IBaseImage image) : base(shape, image) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public virtual void AtDeletion(Level level) { }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Update() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="data"></param>
        public abstract void Accept(GameObject other, CollisionData data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <param name="data"></param>
        public virtual void BlockCollision(Block block, CollisionData data) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <param name="data"></param>
        public virtual void BallCollision(Ball block, CollisionData data) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wall"></param>
        /// <param name="data"></param>
        public virtual void WallCollision(Wall wall, CollisionData data) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="data"></param>
        public virtual void PlayerCollision(Player player, CollisionData data) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="powerup"></param>
        /// <param name="data"></param>
        public virtual void PowerUpCollision(Powerup powerup, CollisionData data) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <param name="data"></param>
        public virtual void UnbreakableCollision(Unbreakable block, CollisionData data) {}

    }
}
