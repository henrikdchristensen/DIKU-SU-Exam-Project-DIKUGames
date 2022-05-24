using System;

namespace Breakout.Input {

    public class FilePath {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetAbsolutePath(string path) {
            string? dirPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var dir = new DirectoryInfo(dirPath);

            while (dir.Name != "bin") {
                dir = dir.Parent;
            }
            dir = dir.Parent;

            return Path.Combine(dir.FullName.ToString(), path);
        }
    }
}
