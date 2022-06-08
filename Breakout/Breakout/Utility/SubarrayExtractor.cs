namespace Breakout.Utility {

    public class SubarrayExtractor {

        /// <summary>
        /// Static method for extracting for sub arrays between start and end keyword
        /// </summary>
        /// <param name="arr">String array subrtact strings from</param>
        /// <param name="startKeyword">Start word</param>
        /// <param name="endKeyword">End word</param>
        /// <returns>New subarray with the extracted lines from start to end keyword</returns>
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
