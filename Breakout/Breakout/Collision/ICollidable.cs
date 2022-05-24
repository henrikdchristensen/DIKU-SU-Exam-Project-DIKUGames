using DIKUArcade.Entities;
using DIKUArcade.Physics;
using Breakout.Items;
using Breakout.Levels;
using Breakout.Items.Powerups;

namespace Breakout.Collision {

    public interface ICollidable {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DynamicShape GetShape();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="data"></param>
        public void Accept(ICollidable other, CollisionData data);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsDestroyed();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <param name="data"></param>
        public void BlockCollision(Block block, CollisionData data) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <param name="data"></param>
        public void BallCollision(Ball block, CollisionData data) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wall"></param>
        /// <param name="data"></param>
        public void WallCollision(Wall wall, CollisionData data) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="data"></param>
        public void PlayerCollision(Player player, CollisionData data) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="powerup"></param>
        /// <param name="data"></param>
        public void PowerUpCollision(Powerup powerup, CollisionData data) { }

    }

}
