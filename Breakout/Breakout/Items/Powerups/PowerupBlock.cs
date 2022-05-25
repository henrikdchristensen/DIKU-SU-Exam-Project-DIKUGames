using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;
using Breakout.Levels;

namespace Breakout.Items.Powerups {
    public class PowerupBlock : Block {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="image"></param>
        /// <param name="powerupImage"></param>
        public PowerupBlock(StationaryShape shape, IBaseImage image) : base(shape, image) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public override void AtDeletion(Level level) {
            level.AddGameObject(Powerup.CreateRandom(Shape.AsDynamicShape()));
        }

    }
}