using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Items.Powerups {

    public class ExtraLife : Powerup {

        public ExtraLife(DynamicShape shape, IBaseImage image) : base(shape, image) {}

        public override PowerupType TAG => PowerupType.ExtraLife;

        /// <summary>
        /// Precondition: expects the gamobject to be a player
        /// Postcondition: the player has been added a life
        /// </summary>
        /// <param name="obj"></param>
        public override void Activate(GameObject obj) {
            Player p = (Player) obj;
            p.AddLife();
        }

        public override void Deactivate(GameObject obj) {}
    }
}
