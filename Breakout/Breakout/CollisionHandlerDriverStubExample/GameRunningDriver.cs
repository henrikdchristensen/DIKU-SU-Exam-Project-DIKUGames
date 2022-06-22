using DIKUArcade.Math;

namespace Breakout.Collision {

    public class GameRunningDriver {
        private CollisionHandler collisionHandler;
        private GameObjectStubAndSpy object1;
        public GameRunningDriver(GameObjectStubAndSpy obj1, GameObjectStubAndSpy obj2) {
            object1 = obj1;
            collisionHandler = CollisionHandler.GetInstance();
            collisionHandler.ClearList();
            collisionHandler.Subsribe(obj1);
            collisionHandler.Subsribe(obj2);
        }
        public void CollideObjects() {
            object1.Shape.AsDynamicShape().ChangeDirection(new Vec2F(0.0f, 1.0f));
            collisionHandler.Update();
        }
}
}
