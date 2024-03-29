using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Game;
using Breakout.Levels;

namespace Breakout.Entities.Powerups {

    public class PowerupBlock : Block {

        /// <summary>Constructor of PowerupBlock: Containing no code</summary>
        /// <param name="shape">A StationaryShape for the block</param>
        /// <param name="image">An image which should be used for the PowerupBlock</param>
        /// <param name="powerupImage"></param>
        public PowerupBlock(StationaryShape shape, IBaseImage image) : base(shape, image) { }

        /// <summary>T</summary>
        /// <param name="level"></param>
        public override void OnDeletion() {
            GameBus.TriggerEvent(GameEventType.ControlEvent, Level.ADD_GAMEOBJECT_MSG,
                                objArg: Powerup.CreateRandom(Shape.AsDynamicShape()));
        }
    }
}