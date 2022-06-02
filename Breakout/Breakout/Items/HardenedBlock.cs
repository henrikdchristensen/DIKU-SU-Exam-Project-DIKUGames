using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;

public class HardenedBlock : Block {

    private IBaseImage blockStridesAlt;

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
        this.blockStridesAlt = blockStridesAlt;
    }

    /// <summary>
    /// Reduce health of block and change image if #hits has occured.
    /// If under zero then delete block.
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