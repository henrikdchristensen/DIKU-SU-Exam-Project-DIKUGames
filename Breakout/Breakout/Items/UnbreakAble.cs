using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Items;
using Breakout.Collision;

public class Unbreakable : Block {

    /// <summary>Constructor of Unbreakable: Setup that it can't be destroyed</summary>
    /// <param name="shape">StationaryShape of the Unbreakable block</param>
    /// <param name="image">Image of the Unbreakable block</param>
    public Unbreakable(StationaryShape shape, IBaseImage image) : base(shape, image) {
        IsDestroyable = false;
    }

    /// <summary>Accepts another GameObject if collision has occured with another object.</summary>
    /// <param name="other">The other GameObject</param>
    /// <param name="data">Collision data</param>
    public override void Accept(GameObject other, CollisionHandlerData data) {
        other.UnbreakableCollision(data);
    }

}