using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;
using Breakout.Levels;

public class PowerupBlock : Block {

    private IBaseImage powerupImage;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="image"></param>
    /// <param name="powerupImage"></param>
    public PowerupBlock(StationaryShape shape, IBaseImage image, IBaseImage powerupImage) : base(shape, image) {
        this.powerupImage = powerupImage;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Hit() {
        Console.WriteLine("powerup hit");
        Health--;
        if (Health < 0)
            DeleteEntity();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    public override void AtDeletion(Level level) {
        
    }

}