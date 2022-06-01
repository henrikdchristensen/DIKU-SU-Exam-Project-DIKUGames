using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System;
using Breakout.Collision;
using DIKUArcade.Physics;
using Breakout.Game;
using Breakout.Items;
using Breakout.Items.Powerups;

namespace Breakout {

    public class Player : GameObject, IGameEventProcessor {

        private PowerupContainer powerups;
        private Text display;
        private int moveLeft = 0;
        private int moveRight = 0;
        private int life;
        private const float MOVEMENT_ACC = 0.005f;
        private float maxSpeed;
        private const int START_LIVES = 3;
        private DynamicShape shape;
        private const float SIZE_SCALE = 1.3f;
        private const float SPEED_SCALE = 1.5f;
        private const float START_SPEED = 0.015f;

        /// <summary> A player in the game </summary>
        /// <param name = "shape"> the shape of the player </param>
        /// <param name = "image"> the image of the player </param>
        /// <returns> A player instance </returns>
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            life = START_LIVES;
            maxSpeed = START_SPEED;

            display = new Text(life.ToString(), new Vec2F(0.45f, 0.5f), new Vec2F(0.6f, 0.5f));
            display.SetColor(new Vec3F(1f, 1f, 1f));

            powerups = new PowerupContainer(
                new PowerupType[] { PowerupType.PlayerSpeed, PowerupType.ExtraLife, PowerupType.Wide });
        }

        /// <summary> Render the player </summary>
        public void Render() {
            RenderEntity();
            display.RenderText();
        }

        /// <summary> Move the player according to its direction </summary>
        public override void Update() {
            UpdateMovement();
            shape.Move();
            Console.WriteLine("Found player speed: " + shape.Direction.X );

            while (!powerups.IsEmpty())
                handlePowerups(powerups.DequeEvent());

            if (shape.Position.X > 1 - shape.Extent.X) {
                ResetDir();
                shape.Position.X = 1 - shape.Extent.X;
            } else if (shape.Position.X < 0) {
                ResetDir();
                shape.Position.X = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateMovement() {
            float dirX = shape.Direction.X;
            int signDir = moveLeft + moveRight; //moveLeft = -1 on keypress and moveRight = 1
            if (signDir == 0) { // if player has stopped moving
                if (Math.Abs(dirX) <= MOVEMENT_ACC) // checking if slowing so much down that it will go in opposite direction stop player
                    shape.Direction.X = 0;
                else
                    shape.Direction.X -= Math.Sign(dirX) * MOVEMENT_ACC; // else reduce player speed with the acceleration
            } else { // if moving
                if (Math.Abs(dirX) + MOVEMENT_ACC <= maxSpeed) // if under max speed keep accelerating by adding acceleration to current speed
                    shape.Direction.X += signDir * MOVEMENT_ACC;
                else
                    shape.Direction.X = signDir * maxSpeed; // if reached max speed keep going at max
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetDir() {
            moveLeft = 0;
            moveRight = 0;
            shape.Direction.X = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        private void SetMoveLeft(bool val) {
            moveLeft = val ? -1 : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        private void SetMoveRight(bool val) {
            moveRight = val ? 1 : 0;
        }

        /// <summary> Get the current position of the player. </summary>
        /// <returns> The current position </returns>
        public Vec2F GetPosition() {
            return new Vec2F(shape.Position.X + shape.Extent.X / 2, shape.Position.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LooseLife() {
            life--;
            display.SetText(life.ToString());
        }
        private void addLife() {
            life++;
            display.SetText(life.ToString());
        }

        /// <summary> Reset life </summary>
        public void Reset() {
            life = START_LIVES;
            display.SetText(life.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        private void GameOver() {
            GameBus.TriggerEvent(GameEventType.GameStateEvent, "CHANGE_STATE_RESET", StateTransformer.TransformStateToString(GameStateType.MainMenu));
        }

        /// <summary> To receive events from the event bus. </summary>
        /// <param name = "gameEvent"> the game-event recieved </param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "LostLife":
                        if (life > 1) 
                            LooseLife();
                        else 
                            GameOver();
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
            } else if (gameEvent.EventType == GameEventType.ControlEvent) {
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

        private void handlePowerups(string type) {
            switch (type) {
                case "EXTRA_LIFE_ACTIVATE":
                    addLife();
                    break;
                case "WIDE_ACTIVATE":
                    Shape.Position.X += (Shape.Extent.X - Shape.Extent.X * SIZE_SCALE) / 2;
                    Shape.Extent.X *= SIZE_SCALE;
                    break;
                case "WIDE_DEACTIVATE":
                    Shape.Extent.X /= SIZE_SCALE;
                    break;
                case "PLAYER_SPEED_ACTIVATE":
                    Console.WriteLine("PLAYER SPEED");
                    maxSpeed *= SPEED_SCALE;
                    break;
                case "PLAYER_SPEED_DEACTIVATE":
                    maxSpeed /= SPEED_SCALE;
                    break;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="data"></param>
        public override void Accept(GameObject other, CollisionData data) {
            other.PlayerCollision(this, data);
        }


    }

}