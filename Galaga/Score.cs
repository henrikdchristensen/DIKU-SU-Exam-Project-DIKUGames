using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Score {
        private int score;
        private Text display;
        public Score(Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), position, extent);
            display.SetColor(new Vec3F(1f, 1f, 1f));
        }
        public void AddPoints() {
            score++;
            display.SetText(score.ToString());
        }
        public void RenderScore() {
            display.RenderText();
        }
    }
}