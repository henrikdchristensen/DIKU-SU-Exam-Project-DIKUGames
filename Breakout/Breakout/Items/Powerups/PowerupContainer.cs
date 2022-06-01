namespace Breakout.Items.Powerups {

    public class PowerupContainer {

        private PowerupType[] powerupsToCollect;
        private List<PowerupType> powerups = new List<PowerupType>();
        private Queue<string> toProcess = new Queue<string>();

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="powerupsToCollect">TODO</param>
        public PowerupContainer(PowerupType[] powerupsToCollect) {
            this.powerupsToCollect = powerupsToCollect;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="str">TODO</param>
        public void Activate(string str) {
            PowerupType type = PowerupTransformer.StringToState(str);
            if (powerupsToCollect.Contains(type)) {
                if (!powerups.Contains(type))
                    toProcess.Enqueue($"{str}_ACTIVATE");
                powerups.Add(type);
            }           
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="str">TODO</param>
        public void Deactivate(string str) {
            PowerupType type = PowerupTransformer.StringToState(str);
            if (powerups.Contains(type)) {
                powerups.Remove(type);
                if (!powerups.Contains(type))
                    toProcess.Enqueue($"{str}_DEACTIVATE");
            }  
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns>TODO</returns>
        public string DequeEvent() {
            if (!IsEmpty())
                return toProcess.Dequeue();
            return "";
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns>TODO</returns>
        public bool IsEmpty() {
            return toProcess.Count() == 0;
        }

    }
}
