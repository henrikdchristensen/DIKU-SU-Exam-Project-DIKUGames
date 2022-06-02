using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Events;
using Breakout.Levels;
using Breakout.Game;
using Breakout.Items.Powerups;

namespace Breakout.Items {

    public class Ball : GameObject, IGameEventProcessor {

        private PowerupContainer powerups;
        private const float SPEED = 0.01f;
        private const double MAX_START_ANGLE = Math.PI / 2;
        private bool isHard = false;

        /// <summary>Constructor for Ball: Setup the</summary>
        /// <param name="shape">The shape of the ball</param>
        /// <param name="image">An image which should be used for Ball</param>
        public Ball (DynamicShape shape, IBaseImage image) : base(shape, image) {
            Random rand = new Random();
            float angle = (float) (rand.NextDouble() * MAX_START_ANGLE + Math.PI / 4);
            Vec2F dir = new Vec2F((float) Math.Cos(angle), (float) Math.Sin(angle));
            dir *= SPEED;
            shape.Direction = dir;
            powerups = new PowerupContainer(
                new PowerupType[] { PowerupType.DoubleSize, PowerupType.DoubleSpeed, PowerupType.HardBall });
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
        public override void Accept(GameObject other, CollisionData data) {
            other.BallCollision(this, data);
        }

        /// <summary>Change direction if collision has occured with a block</summary>
        /// <param name="block">A Block object</param>
        /// <param name="data">Collision data</param>
        public override void BlockCollision(Block block, CollisionData data) {
            if (!isHard)
                changeDirection(block, data);
        }

        /// <summary>Change direction if collision has occured with a player</summary>
        /// <param name="player">A player object</param>
        /// <param name="data">Collision data</param>
        public override void PlayerCollision(Player player, CollisionData data) {
            changeDirection(player, data);
        }

        /// <summary>Change direction if collision has occured with a wall</summary>
        /// <param name="wall">A wall object</param>
        /// <param name="data">Collision data</param>
        public override void WallCollision(Wall wall, CollisionData data) {
            changeDirection(wall, data);
        }

        /// <summary>Change direction if collision has occured with a wall</summary>
        /// <param name="block">A block object</param>
        /// <param name="data">Collision data</param>
        public override void UnbreakableCollision(Unbreakable block, CollisionData data) {
            changeDirection(block, data);
        }

        /// <summary>Change direction based on collision data</summary>
        /// <param name="otherCol">Other GameObject</param>
        /// <param name="data">Collision data</param>
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
        /// Update movement of ball; handle powerups;
        /// and delete the ball if outside game window
        /// </summary>
        public override void Update() {
            Shape.AsDynamicShape().Move();
            while (!powerups.IsEmpty())
                handlePowerups(powerups.DequeEvent());
            if (Shape.Position.Y + Shape.Extent.Y < 0) {
                DeleteEntity();
            }
        }

        /// <summary>
        /// If ball has been marked to be deleted then trigger an event
        /// for loosing a life and remove 1 ball from the level
        /// </summary>
        /// <param name="level">A level object</param>
        public override void AtDeletion(Level level) {
            level.OnBallDeletion();
            GameBus.TriggerEvent(GameEventType.PlayerEvent, "LostLife");
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
                    case "POWERUP_ACTIVATE":
                        powerups.Activate(gameEvent.StringArg1);
                        break;
                    case "POWERUP_DEACTIVATE":
                        powerups.Deactivate(gameEvent.StringArg1);
                        break;
                }
            }
        }

        /// <summary>Handle powerup events</summary>
        /// <param name="type">String defining powerup type</param>
        private void handlePowerups(string type) {
            switch (type) {
                case "DOUBLE_SIZE_ACTIVATE":
                    Shape.Extent *= 2;
                    break;
                case "DOUBLE_SIZE_DEACTIVATE":
                    Shape.Extent /= 2;
                    break;
                case "DOUBLE_SPEED_ACTIVATE":
                    Shape.AsDynamicShape().Direction *= 2;
                    break;
                case "DOUBLE_SPEED_DEACTIVATE":
                    Shape.AsDynamicShape().Direction /= 2;
                    break;
                case "HARD_BALL_ACTIVATE":
                    isHard = true;
                    break;
                case "HARD_BALL_DEACTIVATE":
                    isHard = false;
                    break;
            }
        }

    }

}