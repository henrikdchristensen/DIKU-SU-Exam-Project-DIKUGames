using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;

public class HardenedBlock : Block {

    private IBaseImage blockStridesAlt;

    public HardenedBlock(StationaryShape shape, IBaseImage image, IBaseImage blockStridesAlt) : base(shape, image) {
        StartHealt = base.StartHealt * 2;
        this.blockStridesAlt = blockStridesAlt;
    }

    override public bool Hit() {
        Health--;
        if (Health < 0)
            return true;
        else if (Health < StartHealt / 2) {
            Image = blockStridesAlt;
        }
        return false;
    }

}