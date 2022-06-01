using DIKUArcade.Entities;
using Breakout.Collision;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Breakout.Levels;
using Breakout.Game;
using DIKUArcade.Events;
using Breakout.Items.Powerups;

namespace Breakout.Items {

    public class Ball : GameObject, IGameEventProcessor {

        public const string SET_HARD_MSG = "SET_HARD";
        public const string SCALE_SIZE_MSG = "SCALE_BALL_SIZE";
        public const string SCALE_SPEED_MSG = "SCALE_BALL_SPEED";

        private const float SPEED = 0.01f;
        private const double MAX_START_ANGLE = Math.PI / 2;
        private bool isHard = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="image"></param>
        public Ball (DynamicShape shape, IBaseImage image) : base(shape, image) {
            Random rand = new Random();
            float angle = (float) (rand.NextDouble() * MAX_START_ANGLE + Math.PI / 4);
            Vec2F dir = new Vec2F((float) Math.Cos(angle), (float) Math.Sin(angle));
            dir *= SPEED;
            shape.Direction = dir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private float getDirFromCollisionVec(CollisionDirection dir) {
            switch (dir) {
                case CollisionDirection.CollisionDirDown:
                    return -(float) (Math.PI / 2);
                case CollisionDirection.CollisionDirUp:
                    return (float) (Math.PI / 2);
                case CollisionDirection.CollisionDirLeft:
                    return (float) Math.PI;
                case CollisionDirection.CollisionDirRight:
                    return 0;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="data"></param>
        public override void Accept(GameObject other, CollisionData data) {
            other.BallCollision(this, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <param name="data"></param>
        public override void BlockCollision(Block block, CollisionData data) {
            if (!isHard)
                changeDirection(block, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="data"></param>
        public override void PlayerCollision(Player player, CollisionData data) {
            changeDirection(player, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wall"></param>
        /// <param name="data"></param>
        public override void WallCollision(Wall wall, CollisionData data) {
            changeDirection(wall, data);
        }

        public override void UnbreakableCollision(Unbreakable block, CollisionData data) {
            changeDirection(block, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="otherCol"></param>
        /// <param name="data"></param>
        private void changeDirection(GameObject otherCol, CollisionData data) {
            DynamicShape other = otherCol.Shape.AsDynamicShape();
            float rot = getDirFromCollisionVec(data.CollisionDir);
            //normal vector of the other game object are calculated
            Vec2F normal = new Vec2F((float) Math.Cos(rot), (float) Math.Sin(rot));

            //calculate: dir - 2 (dir dot-produkt normal) * normal
            Vec2F dir = Shape.AsDynamicShape().Direction;
            float dotProduct = Vec2F.Dot(normal, dir); //TODO
            Vec2F newDir = dir - 2 * dotProduct * normal;
            newDir += other.Direction * 0.25f;
            Shape.AsDynamicShape().ChangeDirection(newDir);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Update() {
            Shape.AsDynamicShape().Move();

            if (Shape.Position.Y + Shape.Extent.Y < 0) {
                DeleteEntity();
            }
                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public override void AtDeletion(Level level) {
            level.OnBallDeletion();
            GameBus.TriggerEvent(GameEventType.PlayerEvent, "LostLife");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Render() {
            RenderEntity();
        }


        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                switch (gameEvent.Message) {
                    case SCALE_SPEED_MSG:
                        Shape.AsDynamicShape().Direction *= (float) gameEvent.ObjectArg1;
                        break;
                    case SCALE_SIZE_MSG:
                        Shape.Extent *= (float) gameEvent.ObjectArg1;
                        break;
                    case SET_HARD_MSG:
                        isHard = (bool) gameEvent.ObjectArg1;
                        break;
                }
            }
        }

    }

}
