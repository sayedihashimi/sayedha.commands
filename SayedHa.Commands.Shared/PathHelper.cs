using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using SayedHa.Commands.Shared.Exceptions;

namespace SayedHa.Commands.Shared {
    public class PathHelper {
        public int RecursionLimit { get; protected set; } = 1000;

        public string GetHomeFolder() {
            var envHome = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "HOMEPATH" : "HOME";
            var home = Environment.GetEnvironmentVariable(envHome);
            return Path.GetFullPath(home);
        }

        public string GetFullPath(string path) {
            if (string.IsNullOrWhiteSpace(path)) return null;

            var pathstr = path.Trim();
            if (pathstr.StartsWith("~", StringComparison.CurrentCulture)) {
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

        public string FindFolderWithNameInOrAbove(string dirnameToFind, string startingDirectory) {
            Debug.Assert(!string.IsNullOrWhiteSpace(dirnameToFind));
            Debug.Assert(!string.IsNullOrWhiteSpace(startingDirectory));

            return FindFolderWithNameInOrAbove(dirnameToFind, startingDirectory, 0);
        }

        private string FindFolderWithNameInOrAbove(string dirNameToFind, string startingDirectory, int recursionCount) {
            Debug.Assert(!string.IsNullOrEmpty(dirNameToFind));
            Debug.Assert(!string.IsNullOrEmpty(startingDirectory));
            recursionCount++;

            if (recursionCount > RecursionLimit) { throw new RecursionLimitReachedException($"recursion limit ({RecursionLimit}) reached."); }

            // check current directory to see if there is a folder with the name dirToFind
            // do this in a way that works across all platforms
            var startDir = GetFullPath(startingDirectory);
            var dirToFind = Path.Combine(startDir, dirNameToFind);

            if (Directory.Exists(dirToFind)) {
                return dirToFind;
            }

            // if the folder has a parent rerun on that
            string parentDir = new DirectoryInfo(startDir).Parent?.FullName;
            if (!string.IsNullOrEmpty(parentDir)) {
                return FindFolderWithNameInOrAbove(dirNameToFind, parentDir, recursionCount);
            }

            return null;
        }
    }
}
