using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;

public class HardenedBlock : Block {

    public HardenedBlock(StationaryShape shape, IBaseImage image) : base(shape, image) {
        Health = Health*2;
        
    }


}