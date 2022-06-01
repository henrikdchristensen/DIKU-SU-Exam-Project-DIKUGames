namespace Breakout.Utility {

    public class SubarrayExtractor {

        public static string[] Extract(string[] arr, string startKeyword, string endKeyword) {
            List<string> lst = new List<string>();
            bool startExtract = false;
            foreach (string line in arr) {
                if (line == startKeyword)
                    startExtract = true;
                else if (line == endKeyword)
                    return lst.ToArray();
                else if (startExtract)
                    lst.Add(line);
            }
            return new string[] { };
        }

    }
}
