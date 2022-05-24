using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Levels;

namespace Breakout.Items {

    public abstract class Item : Entity {
        public Item(Shape shape, IBaseImage image) : base(shape, image) { }

        public virtual void Die(Level level) { }

    }
}
