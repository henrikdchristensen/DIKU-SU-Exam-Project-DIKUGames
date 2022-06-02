using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using Breakout.Game;
using Breakout.Collision;

namespace Breakout.Items {

    public class BallContainer : IGameEventProcessor {

        public const string SPLIT_BALLS_MSG = "SPLIT_BALLS_MSG";

        private List<Ball> balls = new List<Ball>();

        //This bool is created, since splitBalls-method
        //cannot be called directly from processevent due to eventbus-error
        private bool splitBallsNext = false;
        private const int SPLIT_NUM = 3;

        public BallContainer() {
            AddBall();
        }

        public void AddBall() {
            Ball ball = new Ball(
                    new Vec2F(0.5f, 0.1f), new Vec2F(0.03f, 0.03f),
                    new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));

            //If balls exist, then a ball is copied to keep state affected by powerup
            if (balls.Count != 0) {
                ball = balls[0].Clone();
                ball.ResetPosition();
            }

            addBall(ball);
            
        }

        private void addBall(Ball ball) {
            balls.Add(ball);
            GameBus.GetBus().Subscribe(GameEventType.ControlEvent, ball);
            CollisionHandler.GetInstance().Subsribe(ball);
        }

        public void Render() {
            foreach (Ball ball in balls)
                ball.Render();
        }

        public void Update() {
            if (splitBallsNext) {
                splitBalls();
                splitBallsNext = false;
            }

            for (int i = balls.Count - 1; i >= 0; i--)
                if (balls[i].IsDeleted())
                    RemoveBall(balls[i]);
                else
                    balls[i].Update();
        }

        private void RemoveBall(Ball ball) {
            if (balls.Count == 1) {
                AddBall();
                ball.AtDeletion();
            } 
            balls.Remove(ball);
        }

        public void Destroy() {
            foreach (Ball ball in balls)
                ball.DeleteEntity();
        }

        private void splitBalls() {
            int len = balls.Count;
            for (int j = len - 1; j >= 0; j--) {
                for (int i = 0; i < SPLIT_NUM; i++) {
                    addBall(balls[j].Clone());
                }
                balls.RemoveAt(j);
            }
            
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                switch (gameEvent.Message) {
                    case SPLIT_BALLS_MSG:
                        splitBallsNext = true;
                        break;
                }
            }
        }
    }
}
