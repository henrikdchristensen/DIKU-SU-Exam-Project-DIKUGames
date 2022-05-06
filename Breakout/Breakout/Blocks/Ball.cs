using DIKUArcade.Entities;
using Breakout.Collision;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;

namespace Breakout.Blocks {


    class Ball : Entity, ICollidable {

        public Ball (DynamicShape shape, IBaseImage image) : base(shape, image) {
            CollisionHandler.GetInstance().Subsribe(this);
        }

        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }        

        public void IsCollided(DynamicShape other) {
            //normal vectori of the other game object
            float rot = other.Rotation + (float)(Math.PI / 2);
            Vec2F normal = new Vec2F((float)Math.Cos(rot), (float)Math.Sin(rot));

            //calculate: dir - 2 (dir dot-produkt normal) * normal
            Vec2F dir = Shape.AsDynamicShape().Direction;
            float dotProduct = Vec2F.Dot(normal, dir);
            Vec2F newDir = dir - 2 * dir * normal;
            Shape.AsDynamicShape().ChangeDirection(newDir);
        }

        public void Move() {
            Shape.AsDynamicShape().Move();
        }

        public void Render() {
            RenderEntity();
        }

    }
}
