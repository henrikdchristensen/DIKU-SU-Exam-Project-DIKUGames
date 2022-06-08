using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Events;
using Breakout.Levels;
using Breakout.Game;
using Breakout.Entities.Powerups;
using Breakout.Collision;

namespace Breakout.Entities {

    public class Ball : GameObject, IGameEventProcessor {

        public const string SET_HARD_MSG = "SET_HARD";
        public const string SCALE_SIZE_MSG = "SCALE_BALL_SIZE";
        public const string SCALE_SPEED_MSG = "SCALE_BALL_SPEED";

        private const double RAND_ANGLE_INTERVAL = Math.PI / 2;
        private readonly Vec2F defaultDir = new Vec2F(0, 0.01f);
        private readonly Vec2F originalPos;

        public bool IsHard { get; private set; } = false;
        

        /// <summary>Constructor for Ball: Setup the</summary>
        /// <param name="shape">The shape of the ball</param>
        /// <param name="image">An image which should be used for Ball</param>
        private Ball (Vec2F position, Vec2F extent, Vec2F dir, Vec2F originalPos, IBaseImage image) : base(new DynamicShape(position, extent), image) {
            this.originalPos = originalPos;
            SetRandomDirection(dir);
        }


        public Ball(Vec2F position, Vec2F extent, IBaseImage image) : base(new DynamicShape(position, extent), image) {
            originalPos = position.Copy();
            SetRandomDirection(defaultDir);
        }

        /// <summary>
        /// Setting a random direction within a 90 degree span based on the dir vector
        /// </summary>
        /// <param name="dir">The direction from which to calculate the 90 degree span</param>
        private void SetRandomDirection(Vec2F dir) {
            Random rand = new Random();
            float angle = (float) (rand.NextDouble() * RAND_ANGLE_INTERVAL + Math.PI / 4) * Math.Sign(dir.Y);
            Vec2F newDir = new Vec2F((float) Math.Cos(angle), (float) Math.Sin(angle));
            newDir *= (float) dir.Length();
            Shape.AsDynamicShape().Direction = newDir;
        }

        /// <summary>
        /// Resets the position of the ball from the bottom of the screen and a direction 
        /// updwards within a 90 degree span
        /// </summary>
        public void ResetPosition() {   
            Shape.Position = originalPos.Copy();
            float len = (float) Shape.AsDynamicShape().Direction.Length();
            SetRandomDirection(Vec2F.Normalize(defaultDir) * len);
        }

        /// <summary>Calculates the new direction of the ball based on the collision data</summary>
        /// <param name="dir">Collision data</param>
        /// <returns>The new direction of the ball</returns>
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
        /// Accepts another GameObject in case of collision,
        /// and put the ball's instance itself into the other GameObject.
        /// (Visitor Pattern)
        /// </summary>
        /// <param name="other">Another GameObject</param>
        /// <param name="data">Collision data</param>
        public override void Accept(IGameObjectVisitor other, CollisionHandlerData data) {
            other.BallCollision(data);
        }
 
        /// <summary>Change direction if collision has occured with a block</summary>
        /// <param name="block">A Block object</param>
        /// <param name="data">Collision data</param>
        public override void BlockCollision(CollisionHandlerData data) {
            if (!IsHard)
                changeDirection(data);
        }

        /// <summary>Change direction if collision has occured with a player</summary>
        /// <param name="player">A player object</param>
        /// <param name="data">Collision data</param>
        public override void PlayerCollision(CollisionHandlerData data) {
            changeDirection(data);
        }

        /// <summary>Change direction if collision has occured with a wall</summary>
        /// <param name="wall">A wall object</param>
        /// <param name="data">Collision data</param>
        public override void WallCollision(CollisionHandlerData data) {
            changeDirection(data);
        }

        /// <summary>Change direction if collision has occured with a wall</summary>
        /// <param name="block">A block object</param>
        /// <param name="data">Collision data</param>
        public override void UnbreakableCollision(CollisionHandlerData data) {
            changeDirection(data);
        }

        /// <summary>Change direction based on collision data</summary>
        /// <param name="otherCol">Other GameObject</param>
        /// <param name="data">Collision data</param>
        private void changeDirection(CollisionHandlerData data) {
            float rot = getDirFromCollisionVec(data.CollisionDir);
            //normal vector of the other game object are calculated
            Vec2F normal = new Vec2F((float) Math.Cos(rot), (float) Math.Sin(rot));

            //calculate: dir - 2 (dir dot-produkt normal) * normal
            Vec2F dir = Shape.AsDynamicShape().Direction;
            float speed = (float) dir.Length();
            float dotProduct = Vec2F.Dot(normal, dir); 
            Vec2F newDir = dir - 2 * dotProduct * normal;
            newDir += data.Direction * 0.25f;
            if (newDir.Length() != 0)
                newDir *= speed / (float) newDir.Length();
            Shape.AsDynamicShape().ChangeDirection(newDir);
        }

        /// <summary>
        /// Update movement of ball; handle powerups;
        /// and delete the ball if outside game window
        /// </summary>
        public override void Update() {
            Shape.AsDynamicShape().Move();

            if (Shape.Position.Y + Shape.Extent.Y < 0) {
                DeleteEntity();
            }
        }

        /// <summary>
        /// When the balls 
        /// </summary>
        public override void OnDeletion() {
            GameBus.TriggerEvent(GameEventType.PlayerEvent, "LostLife");
        }

        public Ball Clone() {
            var shape = Shape.AsDynamicShape();
            return new Ball(shape.Position.Copy(), shape.Extent.Copy(),
                            shape.Direction.Copy(), originalPos.Copy(), Image);
        }

        /// <summary>Render the ball on the screen</summary>
        public void Render() {
            RenderEntity();
        }

        /// <summary>Process ball events: Powerup activate/deactivate</summary>
        /// <param name="gameEvent">A GameEvent</param>
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
                        IsHard = (bool) gameEvent.ObjectArg1;
                        break;
                }
            }
        }

    }

}