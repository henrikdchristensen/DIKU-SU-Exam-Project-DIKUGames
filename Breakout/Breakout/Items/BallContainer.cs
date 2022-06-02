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

        private bool splitBallsNext = false;
        private int numOfTimes;

        public BallContainer() {}

        public void AddBall() {
            Ball ball;
            if (balls.Count == 0) {
                ball = new Ball(
                    new Vec2F(0.5f, 0.1f), new Vec2F(0.03f, 0.03f),
                    new Image(Path.Combine("..", "Breakout", "Assets", "Images", "ball.png")));
            } else {
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
                ball.AtDeletion(null);
            } 
            balls.Remove(ball);
        }

        public void Destroy() {
            foreach (Ball ball in balls)
                ball.DeleteEntity();
        }

        private void splitBalls() {
            int len = balls.Count;
            for (int i = 0; i < numOfTimes; i++) {
                for (int j = len - 1; j >= 0; j--)
                    addBall(balls[j].Clone());
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.ControlEvent) {
                switch (gameEvent.Message) {
                    case SPLIT_BALLS_MSG:
                        splitBallsNext = true;
                        numOfTimes = gameEvent.IntArg1;
                        break;
                }
            }
        }
    }
}
