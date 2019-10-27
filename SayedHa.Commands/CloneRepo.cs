using System;
using SayedHa.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using SayedHa.Commands.Shared.Exceptions;

namespace SayedHa.Commands {
    public class CloneRepoCommand : BaseCommandLineApplication {
        protected string _gitSshUrlFormat = @"git@github.com:{0}/{1}.git";
        protected string _gitHttpsUrlFormat = @"https://github.com/{0}/{1}.git";

        public CloneRepoCommand():base(
            "clonerepo",
            "clonegithubrepo",
            "Will clone a github repo based on the account and repo name."){

            // arguments
            var argumentRepoUrl = this.Argument<string>(
                "repourl",
                "URL for the github repo to be cloned. If this argument is passed and options for account or repo name are passed, the option value wins.");

            // options
            var optionAccountName = this.Option<string>(
                "--a|--accountname",
                "Name of the github account to be cloned.",
                CommandOptionType.SingleValue);

            var optionRepoName = this.Option<string>(
                "-r|--reponame",
                "Name of the github repo to be cloned",
                CommandOptionType.SingleValue);

            var optionDirectory = this.Option<string>(
                "-d|--directory",
                "The folder where the repo will be cloned into. The clone operation will be in a sub folder of this folder. Default folder is the current working directory",
                CommandOptionType.SingleValue);

            var optionUseSsh = this.Option<string>(
                "---ssh",
                "Use ssh for the clone operation.",
                CommandOptionType.NoValue);

            var optionUseHttps = this.Option<string>(
                "--https",
                "Use https for the clone operation",
                CommandOptionType.NoValue);

            this.OnExecute(async () => {
                var repoUrl = argumentRepoUrl.Value;

                var acctAndReopFromUrl = new GitHelper().GetAccountAndRepoNameFromUrl(repoUrl);

                var accountName = optionAccountName.HasValue()
                    ? optionAccountName.ParsedValue
                    : acctAndReopFromUrl.accountName;

                var repoName = optionRepoName.HasValue()
                    ? optionRepoName.ParsedValue
                    : acctAndReopFromUrl.repoName;

                var directory = optionDirectory.HasValue()
                    ? new PathHelper().GetFullPath(optionDirectory.ParsedValue)
                    : new PathHelper().GetFullPath(Directory.GetCurrentDirectory());

                var cloneMethod = CloneMethod.ssh;

                if (optionUseHttps.HasValue()) {
                    cloneMethod = CloneMethod.https;
                }

                string url = cloneMethod == CloneMethod.ssh
                    ? string.Format(_gitSshUrlFormat, accountName, repoName)
                    : string.Format(_gitHttpsUrlFormat, accountName, repoName);

                var targetDir = Path.Combine(directory, repoName);
                Console.WriteLine($"Cloning repo at {url} into directory {targetDir}");
                if (Directory.Exists(targetDir)) {
                    throw new FolderAlreadyExistsException($"Cannot clone because folder already exists at '{targetDir}'");
                }

                ICliCommand cloneCommand = new CliCommand {
                    Command = "git",
                    Arguments = $"clone {url}",
                    SupressExceptionOnNonZeroExitCode = true
                };

                if (VerboseEnabled) Console.WriteLine("Cloning now");

                await cloneCommand.RunCommand();

                // TODO: Not working
                // here is what we need to do but it's windows specific https://stackoverflow.com/a/10222171/105999
                //if (Directory.Exists(targetDir)) {
                //    Directory.SetCurrentDirectory(targetDir);
                //    if (VerboseEnabled) Console.WriteLine($"attempting to change directory to '{targetDir}'");
                //    var res = await new CliCommand {
                //        Command = "chdir",
                //        Arguments = $"'{targetDir}'",
                //        SupressExceptionOnNonZeroExitCode = true
                //    }.RunCommand();
                //    if (VerboseEnabled) Console.WriteLine($"exit code: {res.ExitCode}");
                //}
                //else {
                //    throw new DirectoryNotFoundException($"Unknown error, folder not found at: {targetDir}");
                //}
            });
        }
    }
}
