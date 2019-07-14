using System;
using System.Threading.Tasks;

namespace SayedHa.Commands.Shared {
    public class FolderHelper {

        public async Task OpenFolderInFinder(string path, bool verbose) {
            var openCommand = new CliCommand {
                Command = "open",
                Arguments = path
            };

            if (verbose) Console.WriteLine($"Opening log folder {path}");

            var cmdresult = await openCommand.RunCommand();
            if (!string.IsNullOrEmpty(cmdresult.StandardError)) {
                Console.WriteLine($"Error: {cmdresult.StandardError}");
            }
        }

    }
}
