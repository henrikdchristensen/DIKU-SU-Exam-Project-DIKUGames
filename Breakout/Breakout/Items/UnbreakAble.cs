using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;

public class Unbreakable : Block {

    public Unbreakable(StationaryShape shape, IBaseImage image) : base(shape, image) {
        IsDestroyable = false;
    }

    override public void Hit() { }

}