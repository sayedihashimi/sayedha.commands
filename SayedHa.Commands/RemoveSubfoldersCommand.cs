using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;
using System.Linq;

namespace SayedHa.Commands {
    public class RemoveSubfoldersCommand : BaseCommandLineApplication{
        public RemoveSubfoldersCommand() : base(
            "removefolders",
            "removesubfolders",
            $"Removes the listed folders under the path specified. Default path is the current working directory '{Directory.GetCurrentDirectory()}'") {

            // options
            var optionRootFolder = this.Option<string>(
                "-r|--rootFolder",
                "The folder that will be used as the parent for the search for folders under it by the names provided",
                CommandOptionType.SingleOrNoValue);

            var optionFolderName = this.Option<string>(
                "-f|--folder",
                "Name of folder to look for, and delete, under the root folder. You can pass this multiple times.",
                CommandOptionType.MultipleValue);
            optionFolderName.IsRequired();

            var optionWhatIf = this.Option<bool>(
                "--whatif",
                "Will display the list of folders that would be removed if the command was executed without this switch.",
                CommandOptionType.NoValue);

            this.OnExecute(() => {
                Debug.Assert(optionFolderName.Values != null && optionFolderName.Values.Count > 0);

                var pathHelper = new PathHelper();
                var rootFolder = optionRootFolder.HasValue()
                    ? pathHelper.GetFullPath(optionRootFolder.Value())
                    : pathHelper.GetFullPath(Directory.GetCurrentDirectory());
                var whatif = optionWhatIf.HasValue();

                var folderNames = optionFolderName.Values;

                var foldersFoundToDelete = new List<string>();

                var wasAFolderFound = false;
                foreach(var folderName in folderNames) {
                    var found = Directory.GetDirectories(rootFolder, folderName, SearchOption.AllDirectories);

                    foreach(var folder in found) {
                        wasAFolderFound = true;
                        var fullpath = pathHelper.GetFullPath(folder);
                        if (whatif) {
                            System.Console.WriteLine($"Found folder: '{fullpath}'");
                        }
                        else {
                            // perform delete now
                            System.Console.WriteLine($"Deleting folder at '{fullpath}'");
                            Directory.Delete(fullpath, true);
                        }
                    }
                }

                if (!wasAFolderFound) Console.WriteLine($"No folders found to delete");
            });
        }
    }
}
