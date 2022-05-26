namespace Breakout.Items.Powerups {

    public class PowerupContainer {

        private PowerupType[] powerupsToCollect;
        private List<PowerupType> powerups = new List<PowerupType>();
        private Queue<string> toProcess = new Queue<string>();

        public PowerupContainer(PowerupType[] powerupsToCollect) {
            this.powerupsToCollect = powerupsToCollect;
        }

        public void Activate(string str) {
            PowerupType type = PowerupTransformer.TransformStringToState(str);
            if (powerupsToCollect.Contains(type)) {
                if (!powerups.Contains(type))
                    toProcess.Enqueue($"{str}_ACTIVATE");
                powerups.Add(type);
            }           
        }

        public void Deactivate(string str) {
            PowerupType type = PowerupTransformer.TransformStringToState(str);
            if (powerups.Contains(type)) {
                powerups.Remove(type);
                if (!powerups.Contains(type))
                    toProcess.Enqueue($"{str}_DEACTIVATE");
            }  
        }

        public string DequeEvent() {
            if (!IsEmpty())
                return toProcess.Dequeue();
            return "";
        }

        public bool IsEmpty() {
            return toProcess.Count() == 0;
        }



    }
}
