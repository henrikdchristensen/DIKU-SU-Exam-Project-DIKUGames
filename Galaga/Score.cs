using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Score {
        private int score;
        private Text display;

        /// <summary> A scoreboard </summary>
        /// <param name = "position"> Where the scoreboard is rendered</param>
        /// <param name = "extent"> The extent (size) of the scoreboard </param>
        /// <returns> A Score instance </returns>
        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);
            display.SetColor(new Vec3F(1f, 1f, 1f));
        }

        /// <summary> Add one point to the score </summary>
        public void AddPoints() {
            score++;
            display.SetText(score.ToString());
        }

        /// <summary> Render the score on the screen </summary>
        public void RenderScore() {
            display.RenderText();
        }
    }
}