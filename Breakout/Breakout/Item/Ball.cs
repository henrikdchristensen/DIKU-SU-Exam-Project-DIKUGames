using DIKUArcade.Entities;
using Breakout.Collision;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System;

namespace Breakout.Items {


    class Ball : Entity, ICollidable {

        public Ball (DynamicShape shape, IBaseImage image) : base(shape, image) {
            CollisionHandler.GetInstance().Subsribe(this);
        }

        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        private float getDirFromCollisionVec(CollisionDirection dir) {
            switch (dir) {
                case CollisionDirection.CollisionDirDown:
                    return (float) (Math.PI / 2);
                case CollisionDirection.CollisionDirUp:
                    return (float) (Math.PI / 2);
                case CollisionDirection.CollisionDirLeft:
                    return 0;
                case CollisionDirection.CollisionDirRight:
                    return 0;
                default:
                    return 0;
            }
        }

        public void AtCollision(DynamicShape other, CollisionData data) {
            float rot = getDirFromCollisionVec(data.CollisionDir);
            //normal vector of the other game object are calculated
            Vec2F normal = new Vec2F((float)Math.Cos(rot), (float)Math.Sin(rot));

            //calculate: dir - 2 (dir dot-produkt normal) * normal
            Vec2F dir = Shape.AsDynamicShape().Direction;
            float dotProduct = Vec2F.Dot(normal, dir); //TODO
            Vec2F newDir = dir - 2 * dotProduct * normal;
            newDir.X += other.Direction.X * 0.5f;
            Shape.AsDynamicShape().ChangeDirection(newDir);
        }

        public bool IsDestroyed() {
            return false;
        }

        public void Move() {
            Shape.AsDynamicShape().Move();
        }

        public void Render() {
            RenderEntity();
        }

    }
}
