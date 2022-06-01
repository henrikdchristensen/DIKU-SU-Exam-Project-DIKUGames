using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

namespace Breakout.Items {

    public class GameObjectSpy : GameObject {

        public bool hasCollided { get; private set; }

        /// <summary>TODO</summary>
        /// <param name="shape">TODO</param>
        /// <param name="image">TODO</param>
        public GameObjectSpy(Shape shape, IBaseImage image) : base(shape, image) { }

        /// <summary>TODO</summary>
        /// <param name="other">TODO</param>
        /// <param name="data">TODO</param>
        public override void Accept(GameObject other, CollisionData data) {
            hasCollided = true;
        }
    }
}
