using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;

public class HardenedBlock : Block {

    private IBaseImage blockStridesAlt;

    /// <summary>Constructor for HardenedBlock: StartHealth is 2 times higher than normal block,</summary>
    /// <param name="shape">TODO</param>
    /// <param name="image">TODO</param>
    /// <param name="blockStridesAlt">TODO</param>
    public HardenedBlock(StationaryShape shape, IBaseImage image, IBaseImage blockStridesAlt) : base(shape, image) {
        StartHealt *= 2;
        PointReward *= 2;
        this.blockStridesAlt = blockStridesAlt;
    }

    /// <summary>TODO</summary>
    override public void Hit() {
        Console.WriteLine("Hardened hit");
        Health--;
        if (Health < 0)
            DeleteEntity();
        else if (Health < StartHealt / 2)
            Image = blockStridesAlt;
    }

}