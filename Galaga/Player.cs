using DIKUArcade.Entities;
using DIKUArcade.Graphics;

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
            this.shape.Move();
        }

        private void UpdateMovement(){
            this.shape.Direction.X = moveLeft + moveRight;
        }

        public void SetMoveLeft(bool val){
            if (val){
                this.moveLeft = -MOVEMENT_SPEED;
            }
            else{
                this.moveLeft = 0;
            }
            UpdateMovement();
        }
        public void SetMoveRight(bool val){
            if (val){
                this.moveRight = +MOVEMENT_SPEED;
            }
            else{
                this.moveRight = 0;
            }
            UpdateMovement();
        }

    }

}