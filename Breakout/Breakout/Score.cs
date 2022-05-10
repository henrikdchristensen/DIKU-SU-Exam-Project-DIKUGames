﻿using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public class Score {

        private int points;

        private Text display;

        /// <summary> A scoreboard </summary>
        /// <param name = "position"> Where the scoreboard is rendered</param>
        /// <param name = "extent"> The extent (size) of the scoreboard </param>
        /// <returns> A Score instance </returns>
        public Score(Vec2F position, Vec2F extent) {
            points = 0;
            display = new Text(points.ToString(), position, extent);
            display.SetColor(new Vec3F(1f, 1f, 1f));
        }

        /// <summary> Add one point to the score </summary>
        public void AddPoints(int add = 1) {
            // If the integer points overflows the size of an int,
            // an OverflowException error is raised
            if (points <= int.MaxValue - add) {
                points += add;
                display.SetText(points.ToString());
            }
            
        }

        /// <summary> Render the score on the screen </summary>
        public void Render() {
            display.RenderText();
        }

        /// <summary> Reset score </summary>
        public void Reset() {
            points = 0;
            display.SetText(points.ToString());
        }

        /// <summary> Get current score </summary>
        public int GetScore() {
            return points;
        }
    }
}