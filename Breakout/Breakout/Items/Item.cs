using Breakout.Collision;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Levels;
using Breakout.Items.Powerups;

namespace Breakout.Items {

    public abstract class Item : Entity, ICollidable {

        public bool IsDestroyable { get;  protected set; } = false;
        public Item(Shape shape, IBaseImage image) : base(shape, image) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public virtual void AtDeletion(Level level) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsDestroyed() {
            return IsDeleted();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Update() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="data"></param>
        public abstract void Accept(ICollidable other, CollisionData data);

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
        /// <param name="block"></param>
        /// <param name="data"></param>
        public virtual void PowerUpCollision(Block block, CollisionData data) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="powerup"></param>
        /// <param name="data"></param>
        public virtual void PowerUpCollision(Powerup powerup, CollisionData data) {
        }

    }
}
