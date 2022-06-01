using DIKUArcade.Events;
using Breakout.Game;

namespace Breakout.Items.Powerups {

    public class PowerupContainer : IGameEventProcessor {

        private static PowerupContainer instance = null;

        public static PowerupContainer GetPowerupContainer() {
            return instance ?? (instance = new PowerupContainer());
        }

        private PowerupContainer() {
            GameBus.GetBus().Subscribe(GameEventType.ControlEvent, this);
        }

        private List<PowerupType> active = new List<PowerupType>();


        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="powerup">TODO</param>
        private void activate(Powerup powerup) {
            if (!active.Contains(powerup.Type))
                powerup.Activate();
            if (powerup.Duration > 0)
                active.Add(powerup.Type);
                       
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="str">TODO</param>
        private void deactivate(Powerup powerup) {
            if (active.Contains(powerup.Type)) {
                active.Remove(powerup.Type);
                if (!active.Contains(powerup.Type)) {
                    Console.WriteLine("DEACTIVATE");
                    powerup.Deactivate();
                }
            }  
        }

        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.Message) {
                case Powerup.ACTIVATE_MSG:
                    activate((Powerup) gameEvent.ObjectArg1);
                    break;
                case Powerup.DEACTIVATE_MSG:
                    deactivate((Powerup) gameEvent.ObjectArg1);
                    break;
            }
        }
    }
}
