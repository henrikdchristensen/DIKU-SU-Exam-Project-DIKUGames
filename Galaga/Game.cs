using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class Game
    {
        private Player player;

        public Game() {
            player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        }

        //private void KeyHandler(KeyboardAction action, KeyboardKey key) {} // TODO: Outcomment

        // public override void Render()
        // {
        //     throw new System.NotImplementedException("Galaga game has nothing to render yet.");
        // }

        // public override void Update()
        // {
        //     throw new System.NotImplementedException("Galaga game has no entities to update yet.");
        // }
    }
}
