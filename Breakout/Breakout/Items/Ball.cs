﻿using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Events;
using Breakout.Levels;
using Breakout.Game;
using Breakout.Items.Powerups;

namespace Breakout.Items {

    public class Ball : GameObject, IGameEventProcessor {

        public const string SET_HARD_MSG = "SET_HARD";
        public const string SCALE_SIZE_MSG = "SCALE_BALL_SIZE";
        public const string SCALE_SPEED_MSG = "SCALE_BALL_SPEED";

        private const float START_SPEED = 0.01f;
        private const double MAX_START_ANGLE = Math.PI / 2;
        public bool isHard { get; private set; }
        private readonly Vec2F startPos;

        /// <summary>Constructor for Ball: Setup the</summary>
        /// <param name="shape">The shape of the ball</param>
        /// <param name="image">An image which should be used for Ball</param>
        public Ball (Vec2F position, Vec2F extent, IBaseImage image, float speed = START_SPEED, int dir = 1, bool isHard = false, Vec2F startPos = null) : base(new DynamicShape(position, extent), image) {
            this.startPos = startPos ?? position.Copy();
            SetRandomDirection(speed, dir);
            this.isHard = isHard;  
        }

        private void SetRandomDirection(float speed, int direction = 1) {
            Random rand = new Random();
            float angle = (float) (rand.NextDouble() * MAX_START_ANGLE + Math.PI / 4);
            Vec2F dir = new Vec2F((float) Math.Cos(angle), (float) Math.Sin(angle) * Math.Sign(direction));
            dir *= speed;
            DynamicShape shape = Shape.AsDynamicShape();
            shape.Direction = dir;
        }

        public void ResetPosition() {
            Shape.Position = startPos.Copy();
            SetRandomDirection(START_SPEED);
        }

        /// <summary>TODO</summary>
        /// <param name="dir">TODO</param>
        /// <returns>TODO</returns>
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

        /// <summary>TODO</summary>
        /// <param name="other">TODO</param>
        /// <param name="data">TODO</param>
        public override void Accept(GameObject other, CollisionData data) {
            other.BallCollision(this, data);
        }

        /// <summary>TODO</summary>
        /// <param name="block">TODO</param>
        /// <param name="data">TODO</param>
        public override void BlockCollision(Block block, CollisionData data) {
            if (!isHard)
                changeDirection(block, data);
        }

        /// <summary>TODO</summary>
        /// <param name="player">TODO</param>
        /// <param name="data">TODO</param>
        public override void PlayerCollision(Player player, CollisionData data) {
            changeDirection(player, data);
        }

        /// <summary>TODO</summary>
        /// <param name="wall">TODO</param>
        /// <param name="data">TODO</param>
        public override void WallCollision(Wall wall, CollisionData data) {
            changeDirection(wall, data);
        }

        /// <summary>TODO</summary>
        /// <param name="block">TODO</param>
        /// <param name="data">TODO</param>
        public override void UnbreakableCollision(Unbreakable block, CollisionData data) {
            changeDirection(block, data);
        }

        /// <summary>TODO</summary>
        /// <param name="otherCol">TODO</param>
        /// <param name="data">TODO</param>
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

        /// <summary>TODO</summary>
        public override void Update() {
            Shape.AsDynamicShape().Move();

            if (Shape.Position.Y + Shape.Extent.Y < 0) {
                DeleteEntity();
            }
        }

        /// <summary>TODO</summary>
        /// <param name="level">TODO</param>
        public override void AtDeletion(Level level) {
            GameBus.TriggerEvent(GameEventType.PlayerEvent, "LostLife");
        }

        public Ball Clone() {
            var shape = Shape.AsDynamicShape();
            float speed = (float) shape.Direction.Length();
            return new Ball(shape.Position.Copy(), shape.Extent.Copy(), Image, speed, Math.Sign(shape.Direction.Y), isHard, startPos);
        }

        /// <summary>TODO</summary>
        public void Render() {
            RenderEntity();
        }

        /// <summary>TODO</summary>
        /// <param name="gameEvent">TODO</param>
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