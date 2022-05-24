using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;
using DIKUArcade.Physics;

public class PowerupBlock : Block {

    private IBaseImage powerupImage;

    public PowerupBlock(StationaryShape shape, IBaseImage image, IBaseImage powerupImage) : base(shape, image) {
        this.powerupImage = powerupImage;
    }

    override public void Hit() {
        Console.WriteLine("powerup hit");
        Health--;
        if (Health < 0) {
            DeleteEntity();
        }
    }

    public void ReleasePowerup() {
        
    }


}