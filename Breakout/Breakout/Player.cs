using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using Breakout.Game;
using Breakout.Items;
using Breakout.Collision;

namespace Breakout {

    public class Player : GameObject, IGameEventProcessor {
        public const string SCALE_SPEED_MSG = "SCALE_SPEED";
        public const string SCALE_WIDE_MSG = "WIDE_SPEED";
        public const string ADD_LIFE_MSG = "ADD_LIFE";
        public const string LOOSE_LIFE_MSG = "LostLife";

        private Text display;
        private int moveLeft = 0;
        private int moveRight = 0;


        /// <summary> 
        /// Life of the player, is public due to testing purposes
        /// </summary>
        public int Life { get; private set; }
        private const float MOVEMENT_ACC = 0.005f;

        /// <summary> 
        /// Max speed of the player, is public due to testing purposes
        /// </summary>
        public float MaxSpeed { get; private set; }
        private const int START_LIVES = 3;
        private DynamicShape shape;
        private const float START_SPEED = 0.015f;

        /// <summary>Constructor of Player: Setup start life, speed and life</summary>
        /// <param name="shape">The shape of the player</param>
        /// <param name="image">The image of the player</param>
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            Life = START_LIVES;
            MaxSpeed = START_SPEED;

            display = new Text(Life.ToString(), new Vec2F(0.45f, 0.5f), new Vec2F(0.6f, 0.5f));
            display.SetColor(new Vec3F(1f, 1f, 1f));

        }

        /// <summary>Render the player</summary>
        public void Render() {
            RenderEntity();
            display.RenderText();
        }

        /// <summary>Move the player according to its direction</summary>
        public override void Update() {
            updateMovement();
            shape.Move();

        }

        /// <summary>TODO</summary>
        private void updateMovement() {
            float dirX = shape.Direction.X;
            int signDir = moveLeft + moveRight; //moveLeft = -1 on keypress and moveRight = 1
            if (signDir == 0) { //1. 1a: if player has stopped moving
                if (Math.Abs(dirX) <= MOVEMENT_ACC) // 2.a checking if slowing so much down that it will go in opposite direction then stop player
                    shape.Direction.X = 0;
                else // 2,b
                    shape.Direction.X -= Math.Sign(dirX) * MOVEMENT_ACC; // else reduce player speed with the acceleration
            } else { // 1b: if moving
                if (Math.Abs(dirX) + MOVEMENT_ACC <= MaxSpeed) // 3.a if under max speed keep accelerating by adding acceleration to current speed
                    shape.Direction.X += signDir * MOVEMENT_ACC;
                else // 3.b
                    shape.Direction.X = signDir * MaxSpeed; // if reached max speed keep going at max
            }

            // Prevent from escaping the screen
            if (shape.Position.X > 1 - shape.Extent.X) { // 4.a
                resetDir();
                shape.Position.X = 1 - shape.Extent.X;
            } else if (shape.Position.X < 0) { // 4.b
                resetDir();
                shape.Position.X = 0;
            }
        }

        /// <summary>Reset direction of the player</summary>
        private void resetDir() {
            moveLeft = 0;
            moveRight = 0;
            shape.Direction.X = 0;
        }

        /// <summary>Set movement to left</summary>
        /// <param name="val">If true then continous movement to left. If false, then no movement</param>
        private void setMoveLeft(bool val) {
            moveLeft = val ? -1 : 0;
        }

        /// <summary>Set movement to right</summary>
        /// <param name="val">If true then continous movement to right. If false, then no movement</param>
        private void setMoveRight(bool val) {
            moveRight = val ? 1 : 0;
        }

        /// <summary>Get the current position of the player</summary>
        /// <returns>The current position</returns>
        public Vec2F GetPosition() {
            return new Vec2F(shape.Position.X + shape.Extent.X / 2, shape.Position.Y);
        }

        /// <summary>Loose on life and reduce it also on the screen</summary>
        private void looseLife() {
            Life--;
            display.SetText(Life.ToString());
        }

        /// <summary>Adds a life to player and sets the new one to the screen</summary>
        private void addLife() {
            Life++;
            display.SetText(Life.ToString());
        }

        /// <summary>Reset amount of life</summary>
        public void Reset() {
            Life = START_LIVES;
            display.SetText(Life.ToString());
        }

        /// <summary>Trigger an event to reset to Main Menu in case of GameOver</summary>
        private void gameOver() {
            GameBus.TriggerEvent(GameEventType.StatusEvent, "PLAYER_DEAD");
        }

        /// <summary>To receive events from the event bus</summary>
        /// <param name="gameEvent">The game-event recieved</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case LOOSE_LIFE_MSG:
                        if (Life > 1)
                            looseLife();
                        else
                            gameOver();
                        break;
                    case "LeftPressed":
                        setMoveLeft(true);
                        break;
                    case "RightPressed":
                        setMoveRight(true);
                        break;
                    case "LeftReleased":
                        setMoveLeft(false);
                        break;
                    case "RightReleased":
                        setMoveRight(false);
                        break;
                }
            } else if (gameEvent.EventType == GameEventType.ControlEvent) {
                switch (gameEvent.Message) {
                    case ADD_LIFE_MSG:
                        addLife();
                        break;
                    case SCALE_WIDE_MSG:
                        float scale = (float) gameEvent.ObjectArg1;
                        Shape.Position.X += (Shape.Extent.X - Shape.Extent.X * scale) / 2;
                        Shape.Extent.X *= scale;
                        break;
                    case SCALE_SPEED_MSG:
                        MaxSpeed *= (float) gameEvent.ObjectArg1;
                        break;
                }
            }
        }

        /// <summary>
        /// Accepts another GameObject if an collision with another GameObject has occured
        /// (Visitor Pattern)
        /// </summary>
        /// <param name="other">The other GameObject</param>
        /// <param name="data">Collision data</param>
        public override void Accept(GameObject other, CollisionHandlerData data) {
            other.PlayerCollision(data);
        }

    }

}