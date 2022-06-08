using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.Collision;

namespace Breakout.Entities {

    /// <summary>
    /// For test purposes. Checks if its methods has been called.
    /// </summary>
    public class GameObjectSpy : GameObject {

        public bool hasCollided { get; private set; } // flag for checking whether Accept method has been called (Spy object)

        /// <summary>A Spy object for testing CollisionHandler's Update method</summary>
        /// <param name="shape">Dummy Shape</param>
        /// <param name="image">Dummy Image</param>
        public GameObjectSpy(Shape shape, IBaseImage image) : base(shape, image) { }

        /// <summary>Accept method used for testing CollisionHandler's Update method</summary>
        /// <param name="other">Dummy GameObject</param>
        /// <param name="data">Dummy Collision data</param>
        public override void Accept(IGameObjectVisitor other, CollisionHandlerData data) {
            hasCollided = true;
        }

    }

}
