using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities {

    public class HardenedBlock : Block {

        public IBaseImage BlockStridesAlt { get; private set; }

        /// <summary>
        /// Constructor for HardenedBlock: StartHealth is 2 times higher than normal block,
        /// also point reward is 2 times higher than normal
        /// </summary>
        /// <param name="shape">StationaryShape of the HardenedBlock</param>
        /// <param name="image">Image of the HardenedBlock</param>
        /// <param name="blockStridesAlt">Image of the HardenedBlock after hits</param>
        public HardenedBlock(StationaryShape shape, IBaseImage image, IBaseImage blockStridesAlt) : base(shape, image) {
            StartHealt *= 2;
            PointReward *= 2;
            this.BlockStridesAlt = blockStridesAlt;
        }

        /// <summary>
        /// Reduce health of block and change image if #hits has occured.
        /// If under zero then delete block.
        /// </summary>
        override public void Hit() {
            Health--;
            if (Health < 0) // B1
                DeleteEntity();
            else if (Health < StartHealt / 2) // B2
                Image = BlockStridesAlt;
        }

    }

}