using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SayedHa.Commands.Shared {
    public class PathHelper {
        public string GetHomeFolder() {
            var envHome = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "HOMEPATH" : "HOME";
            var home = Environment.GetEnvironmentVariable(envHome);
            return Path.GetFullPath(home);
        }

        public string GetFullPath(string path) {
            if (string.IsNullOrWhiteSpace(path)) return null;

            var pathstr = path.Trim();
            if (pathstr.StartsWith("~")) {
                pathstr = pathstr.Substring(1, pathstr.Length - 1);
                pathstr = new PathHelper().GetHomeFolder() + pathstr;
            }

            return Path.GetFullPath(pathstr);
        }

        public bool ArePathsEqual(string path1, string path2) {
            // to deal with potential trailing slash, combine path with a known end path node
            string fullpath1 = Path.Combine(GetFullPath(path1), "end");
            string fullpath2 = Path.Combine(GetFullPath(path2), "end");

            fullpath1 = fullpath1.TrimEnd('/', '\\');
            fullpath2 = fullpath2.TrimEnd('/', '\\');

            return string.Compare(fullpath1, fullpath2, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}
