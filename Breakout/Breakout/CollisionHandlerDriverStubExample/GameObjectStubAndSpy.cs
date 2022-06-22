using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Entities;

namespace Breakout.Collision {

    public class GameObjectStubAndSpy : GameObject {
        public bool RecievedAccept { get; private set; }
        public StubType CollidedWith { get; private set; } = StubType.NoOne;
        private StubType stubType;
        public GameObjectStubAndSpy(StubType type, Shape shape, IBaseImage image) : base(shape, image) {
            stubType = type;
        }
        public override void Accept(IGameObjectVisitor other, CollisionHandlerData data) {
            RecievedAccept = true;
            switch (stubType) {
                case StubType.NoOne:
                    break;
                case StubType.Block:
                    other.BlockCollision(data);
                    break;
                case StubType.Ball:
                    other.BallCollision(data);
                    break;
                case StubType.Wall:
                    other.WallCollision(data);
                    break;
                case StubType.Player:
                    other.PlayerCollision(data);
                    break;
                case StubType.PowerUp:
                    other.PowerUpCollision(data);
                    break;
                case StubType.Unbreakable:
                    other.UnbreakableCollision(data);
                    break;
            }
        }
        public void SetCollidedWith(StubType type) {
            if (CollidedWith == StubType.NoOne) {
                CollidedWith = type;
            } else {
                throw new InvalidOperationException("Already collided with another");
            }
        }
        public override void BlockCollision(CollisionHandlerData data) {
            if (stubType == StubType.Ball) {
                SetCollidedWith(StubType.Block);
            }
        }
        public override void BallCollision(CollisionHandlerData data) {
            if (stubType == StubType.Block ||
                stubType == StubType.Player ||
                stubType == StubType.Wall ||
                stubType == StubType.Unbreakable) {
                SetCollidedWith(StubType.Ball);
            }
        }
        public override void WallCollision(CollisionHandlerData data) {
            if (stubType == StubType.Ball) {
                SetCollidedWith(StubType.Wall);
            }
        }
        public override void PlayerCollision(CollisionHandlerData data) {
            if (stubType == StubType.Ball ||
                stubType == StubType.PowerUp) {
                SetCollidedWith(StubType.Player);
            }
        }
        public override void PowerUpCollision(CollisionHandlerData data) {
            if (stubType == StubType.Player) {
                SetCollidedWith(StubType.PowerUp);
            }
        }
        public override void UnbreakableCollision(CollisionHandlerData data) {
            if (stubType == StubType.Ball) {
                SetCollidedWith(StubType.Unbreakable);
            }
        }
    }
}
