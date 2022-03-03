using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;

namespace Galaga {

    public class Player {

        private Entity entity;
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        const float MOVEMENT_SPEED = 0.01f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }

        public void Render(){
            entity.RenderEntity();
        }

        public void Move(){
            shape.Move();
            shape.Position.X = Math.Max(0, shape.Position.X);
            shape.Position.X = Math.Min(1 - shape.Extent.X, shape.Position.X);
        }

        private void UpdateMovement(){
            this.shape.Direction.X = moveLeft + moveRight;
        }

        public void SetMoveLeft(bool val){
            moveLeft = val ? -MOVEMENT_SPEED : 0;
            UpdateMovement();
        }
        public void SetMoveRight(bool val){
            moveRight = val ? MOVEMENT_SPEED : 0;
            UpdateMovement();
        }

        public Vec2F GetPosition() {
            return new Vec2F(shape.Position.X + shape.Extent.X / 2, shape.Position.Y);
        }

    }

}