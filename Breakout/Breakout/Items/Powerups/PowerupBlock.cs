using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;
using Breakout.Levels;

namespace Breakout.Items.Powerups {
    public class PowerupBlock : Block {

        /// <summary>Constructor of PowerupBlock: Containing no code</summary>
        /// <param name="shape">A StationaryShape for the block</param>
        /// <param name="image">An image which should be used for the PowerupBlock</param>
        /// <param name="powerupImage"></param>
        public PowerupBlock(StationaryShape shape, IBaseImage image) : base(shape, image) { }

        /// <summary>TODO</summary>
        /// <param name="level"></param>
        public override void AtDeletion(Level level) {
            level.AddGameObject(Powerup.CreateRandom(Shape.AsDynamicShape()));
        }

    }
}