using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga{

    public class PlayerShot : Entity{
        private static Vec2F extent = new Vec2F(0.008f, 0.021f);
        private static Vec2F direction = new Vec2F(0.0f, 0.1f);

        /// <summary> A player-shot </summary>
        /// <param name = "position"> the start position of the player-shot </param>
        /// <param name = "image"> the image of the player-shot </param>
        /// <returns> A player-shot instance </returns>
        public PlayerShot(Vec2F position, IBaseImage image)
        : base(new DynamicShape(position, extent, direction), image) { }

        
    }

}