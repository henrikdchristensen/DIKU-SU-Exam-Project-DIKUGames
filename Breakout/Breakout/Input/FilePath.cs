namespace Breakout.Input {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FilePath {
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
