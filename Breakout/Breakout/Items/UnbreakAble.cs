using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Items;
using DIKUArcade.Physics;

public class Unbreakable : Block {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="image"></param>
    public Unbreakable(StationaryShape shape, IBaseImage image) : base(shape, image) {
        IsDestroyable = false;
    }

    /// <summary>
    /// 
    /// </summary>
    override public void Hit() { }

    public override void Accept(GameObject other, CollisionData data) {
        other.UnbreakableCollision(this, data);
    }

}