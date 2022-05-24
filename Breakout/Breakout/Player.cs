using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System;
using Breakout.Collision;
using DIKUArcade.Physics;
using Breakout.Game;

namespace Breakout {

    public class Player : IGameEventProcessor, ICollidable {

        private GameEventBus eventBus;
        private Entity entity;
        private DynamicShape shape;
        private int moveLeft = 0;
        private int moveRight = 0;
        private int life;
        private const float MOVEMENT_ACC = 0.005f;
        private const float MAX_SPEED = 0.02f;

        /// <summary> A player in the game </summary>
        /// <param name = "shape"> the shape of the player </param>
        /// <param name = "image"> the image of the player </param>
        /// <returns> A player instance </returns>
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            life = 3;
        }

        /// <summary> Render the player </summary>
        public void Render() {
            entity.RenderEntity();
        }

        /// <summary> Move the player according to its direction </summary>
        public void Move() {
            UpdateMovement();
            shape.Move();

            if (shape.Position.X > 1 - shape.Extent.X) {
                resetDir();
                shape.Position.X = 1 - shape.Extent.X;
            } else if (shape.Position.X < 0) {
                resetDir();
                shape.Position.X = 0;
            }
        }

        private void UpdateMovement() {
            float dirX = shape.Direction.X;
            int signDir = moveLeft + moveRight;
            if (signDir == 0) {
                if (Math.Abs(dirX) <= MOVEMENT_ACC)
                    shape.Direction.X = 0;
                else
                    shape.Direction.X -= Math.Sign(dirX) * MOVEMENT_ACC;
            } else {
                if (Math.Abs(dirX) + MOVEMENT_ACC <= MAX_SPEED)
                    shape.Direction.X += signDir * MOVEMENT_ACC;
                else
                    shape.Direction.X = signDir * MAX_SPEED;
            }
        }

        private void resetDir() {
            moveLeft = 0;
            moveRight = 0;
            shape.Direction.X = 0;
        }

        private void SetMoveLeft(bool val) {
            moveLeft = val ? -1 : 0;
        }
        private void SetMoveRight(bool val) {
            moveRight = val ? 1 : 0;
        }

        /// <summary> Get the current position of the player. </summary>
        /// <returns> The current position </returns>
        public Vec2F GetPosition() {
            return new Vec2F(shape.Position.X + shape.Extent.X / 2, shape.Position.Y);
        }

        private void LooseLife() {
            life--;
        }

        private void GameOver() {
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE_RESET",
                StringArg1 = StateTransformer.TransformStateToString(GameStateType.MainMenu)
            });
        }

        /// <summary> To receive events from the event bus. </summary>
        /// <param name = "gameEvent"> the game-event recieved </param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "LostLife":
                        if (life > 0) {
                            LooseLife();
                        } else {
                            GameOver();
                        }
                        break;
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

        public DynamicShape GetShape() {
            return shape;
        }

        public void AtCollision(DynamicShape other, CollisionData data) {

        }

        public bool IsDestroyed() {
            return false;
        }

    }

}