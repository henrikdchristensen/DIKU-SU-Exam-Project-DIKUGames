using DIKUArcade.Entities;
using DIKUArcade.Physics;
using Breakout.Items;


namespace Breakout.Collision {

    public interface ICollidable {

        public DynamicShape GetShape();

        public void Accept(ICollidable other, CollisionData data);

        public bool IsDestroyed();

        public void BlockCollision(Block block, CollisionData data) { }
        public void BallCollision(Ball block, CollisionData data) { }
        public void WallCollision(Wall wall, CollisionData data) { }
        public void PlayerCollision(Player player, CollisionData data) { }
        public void PowerUpCollision(Block block, CollisionData data) { }
    }
}
