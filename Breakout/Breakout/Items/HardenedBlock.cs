using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;
using DIKUArcade.Physics;

public class HardenedBlock : Block {

    private IBaseImage blockStridesAlt;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="image"></param>
    /// <param name="blockStridesAlt"></param>
    public HardenedBlock(StationaryShape shape, IBaseImage image, IBaseImage blockStridesAlt) : base(shape, image) {
        StartHealt *= 2;
        value *= 2;
        this.blockStridesAlt = blockStridesAlt;
    }

    /// <summary>
    /// 
    /// </summary>
    override public void Hit() {
        Console.WriteLine("Hardened hit");
        Health--;
        if (Health < 0)
            DeleteEntity();
        else if (Health < StartHealt / 2)
            Image = blockStridesAlt;
    }

}