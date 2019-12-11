using System;
using SayedHa.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SayedHa.Commands {
    public class InitVsGitRepoCommand : BaseCommandLineApplication {
        public InitVsGitRepoCommand(
                IReporter reporter,
                IPathHelper pathHelper,
                IWebHelper webHelper) : base(
            "initvsrepo",
            "initvsgitrepo",
            "Initalize the directory as a new git repo. It will call git init, add a .gitignore and a .gitattributes and also do an initial commit.") {

            Debug.Assert(reporter != null);
            Debug.Assert(pathHelper != null);
            Debug.Assert(webHelper != null);
            /*
             * var optionRuntimeToDelete = this.Option<string>(
                "-v|--version",
                "Version of the runtime to be deleted. If there are multiple runtimes, in different categories matching the version and the category parameter is not passed then all will be deleted.",
                CommandOptionType.MultipleValue);
            optionRuntimeToDelete.IsRequired(errorMessage: "Version to delete is required.");

             */
            // options
            var folderOption = this.Option<string>(
                "-f|--folder",
                "Folder for the root of the git repo, default is current working directory",
                CommandOptionType.SingleValue);

            this.OnExecute(async () => {
                var rootFolder = folderOption.HasValue() ?
                                    pathHelper.GetFullPath(folderOption.Value()) :
                                    pathHelper.GetFullPath(Directory.GetCurrentDirectory());

                reporter.Output($"Initalizing VS git repo at {rootFolder}");
                var previousWd = Directory.GetCurrentDirectory();
                try {
                    Directory.SetCurrentDirectory(rootFolder);
                    // git init
                    await new CliCommand {
                        Command = "git",
                        Arguments = "init",
                        SupressExceptionOnNonZeroExitCode = true
                    }.RunCommand();

                    // add .gitignore
                    reporter.Output("adding .gitignore");
                    await webHelper.DownloadFile(
                        KnownStrings.GitIgnoreUrl,
                        Path.Combine(rootFolder, KnownStrings.GitIgnoreFilename));

                    // add .gitattributes
                    reporter.Output("adding .gitattributes");
                    await webHelper.DownloadFile(
                        KnownStrings.GitAttributesUrl,
                        Path.Combine(rootFolder, KnownStrings.GitAttributesFilename));
                    
                    // git add .
                    reporter.Output("adding files");
                    await new CliCommand {
                        Command = "git",
                        Arguments = "add .",
                        SupressExceptionOnNonZeroExitCode = true
                    }.RunCommand();

                    reporter.Output("creating initial commit");
                    await new CliCommand {
                        Command = "git",
                        Arguments = @"commit -m init",
                        SupressExceptionOnNonZeroExitCode = true
                    }.RunCommand();

                    reporter.Output("✓ all done");
                }
                finally {
                    Directory.SetCurrentDirectory(previousWd);
                }
            });
        }
    }
}
