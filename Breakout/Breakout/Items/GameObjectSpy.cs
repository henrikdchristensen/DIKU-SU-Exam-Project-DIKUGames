using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

namespace Breakout.Items {

    public class GameObjectSpy : GameObject {

        public bool hasCollided {
            get; private set;
        }

        public GameObjectSpy(Shape shape, IBaseImage image) : base(shape, image) { }

        public override void Accept(GameObject other, CollisionData data) {
            hasCollided = true;
        }
    }
}
