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
        public virtual void AtDeletion(Level level) { }

        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }
        public bool IsDestroyed() {
            return IsDeleted();
        }

        public virtual void Update() {}

        public abstract void Accept(ICollidable other, CollisionData data);

        public virtual void BlockCollision(Block block, CollisionData data) {
        }
        public virtual void BallCollision(Ball block, CollisionData data) {
        }
        public virtual void WallCollision(Wall wall, CollisionData data) {
        }
        public virtual void PlayerCollision(Player player, CollisionData data) {
        }
        public virtual void PowerUpCollision(Block block, CollisionData data) {
        }

        public virtual void PowerUpCollision(Powerup powerup, CollisionData data) {
        }

    }
}
