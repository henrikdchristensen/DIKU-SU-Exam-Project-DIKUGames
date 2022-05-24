using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Levels;

namespace Breakout.Item {

    class Item : Entity {
        public Item(Shape shape, IBaseImage image) : base(shape, image) { }

        public virtual void Die(Level level) { }

    }
}
