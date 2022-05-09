using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System;
using Breakout.Collision;
using DIKUArcade.Physics;

namespace Breakout {

    public class Player : IGameEventProcessor, ICollidable {

        private Entity entity;
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private float MOVEMENT_SPEED = 0.015f;

        /// <summary> A player in the game </summary>
        /// <param name = "shape"> the shape of the player </param>
        /// <param name = "image"> the image of the player </param>
        /// <returns> A player instance </returns>
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }

        /// <summary> Render the player </summary>
        public void Render() {
            entity.RenderEntity();
        }

        /// <summary> Move the player according to its direction </summary>
        public void Move() {
            shape.Move();

            shape.Position.X = Math.Max(0, shape.Position.X);
            shape.Position.X = Math.Min(1 - shape.Extent.X, shape.Position.X);
        }

        private void UpdateMovement() {
            this.shape.Direction.X = moveLeft + moveRight;
        }

        private void ResetMovement() {
            
        }

        private void SetMoveLeft(bool val) {
            moveLeft = val ? -MOVEMENT_SPEED : 0;
            UpdateMovement();
        }
        private void SetMoveRight(bool val) {
            moveRight = val ? MOVEMENT_SPEED : 0;
            UpdateMovement();
        }

        /// <summary> Get the current position of the player. </summary>
        /// <returns> The current position </returns>
        public Vec2F GetPosition() {
            return new Vec2F(shape.Position.X + shape.Extent.X / 2, shape.Position.Y);
        }

        /// <summary> To receive events from the event bus. </summary>
        /// <param name = "gameEvent"> the game-event recieved </param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "LeftPressed":
                        SetMoveLeft(true);
                        break;
                    case "RightPressed":
                        SetMoveRight(true);
                        break;
                    case "LeftReleased":
                        SetMoveLeft(false);
                        break;
                    case "RightReleased":
                        SetMoveRight(false);
                        break;
                }
            }
        }

        public float GetMovementSpeed() {
            return MOVEMENT_SPEED;
        }

        public DynamicShape GetShape() {
            return shape;
        }

        public void IsCollided(DynamicShape shape, CollisionData data) { }

        public bool IsDestroyed() {
            return false;
        }

    }

}