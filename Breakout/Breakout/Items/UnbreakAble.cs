using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Items;

public class Unbreakable : Block {

    /// <summary>TODO</summary>
    /// <param name="shape">TODO</param>
    /// <param name="image">TODO</param>
    public Unbreakable(StationaryShape shape, IBaseImage image) : base(shape, image) {
        IsDestroyable = false;
    }

    /// <summary>TODO</summary>
    override public void Hit() { }

    /// <summary>TODO</summary>
    /// <param name="other">TODO</param>
    /// <param name="data">TODO</param>
    public override void Accept(GameObject other, CollisionData data) {
        other.UnbreakableCollision(this, data);
    }

}