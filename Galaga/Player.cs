using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga {

    public class Player {

        private Entity entity;
        private DynamicShape shape;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }
        
        public void Render(){
            // TODO: render the player entity
        }

    }

}