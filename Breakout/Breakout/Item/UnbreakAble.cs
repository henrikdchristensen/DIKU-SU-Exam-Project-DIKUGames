using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;

public class Unbreakable : Block {

    public Unbreakable(StationaryShape shape, IBaseImage image) : base(shape, image) { }

    override public bool Hit() {
        return false;
    }

}