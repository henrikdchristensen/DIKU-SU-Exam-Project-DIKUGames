using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Game {

    public class Score {

        private int points;
        private Text display;
        private readonly Vec3F COLOR = new Vec3F(247f / 255f, 145f / 255f, 0);

        /// <summary>Constructor for Score: Setup a scoreboard</summary>
        /// <param name="position"> Where the scoreboard is rendered</param>
        /// <param name="extent"> The extent (size) of the scoreboard</param>
        public Score(Vec2F position, Vec2F extent) {
            points = 0;
            display = new Text(points.ToString(), position, extent);
            display.SetColor(COLOR);
        }

        /// <summary>Add points to the score</summary>
        /// <param name="add">The points to be added to the overall score</param>
        public void AddPoints(int add = 1) {
            if (add > 0 && points < int.MaxValue) {
                if (points < int.MaxValue - add) {
                    points += add;
                } else {
                    points = int.MaxValue;
                }
                display.SetText(points.ToString());
            }
            
        }

        /// <summary>Render the score on the screen</summary>
        public void Render() {
            display.RenderText();
        }

        /// <summary>Reset score</summary>
        public void Reset() {
            points = 0;
            display.SetText(points.ToString());
        }

        /// <summary>Get current score</summary>
        /// <returns>The score</returns>
        public int GetScore() {
            return points;
        }

    }

}