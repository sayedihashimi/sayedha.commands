using System;
using SayedHa.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace SayedHa.Commands {
    public class OpenGithubCommand : BaseCommandLineApplication {
        protected string _githubPattern = @"origin\tgit@github.com:([a-zA-S-.]+)/([a-zA-Z-]+)\.git.*(fetch\))";
        protected string _gitRemoteGithubPattern = @"git@github\.com\:([^\/]*)\/(.*)\.git";
        public OpenGithubCommand() : base(
            "og",
            "OpenGithub",
            "If the repo has an origin pointing to github.com the project page will be opened,") {
            // origin\tgit@github.com:([a-zA-S-.]+)/([a-zA-Z-]+)\.git.*(fetch\))

            // options
            var optionDirectory = this.Option<string>(
                "-d|--directory",
                "Directory of git repo that will be used to find the proejct URL. Default is pwd",
                CommandOptionType.SingleValue);

            var optionRemoteName = this.Option<string>(
                "-o|--origin",
                $"Specifies the name of the remote to look for. Default: {KnownStrings.DefaultRemoteName}",
                CommandOptionType.SingleValue);

            this.OnExecute(async () => {
                var directory = optionDirectory.HasValue()
                    ? optionDirectory.ParsedValue
                    : Directory.GetCurrentDirectory();

                var remoteName = optionRemoteName.HasValue()
                    ? optionRemoteName.Value()
                    : KnownStrings.DefaultRemoteName;

                Console.WriteLine($"Finding proejct url in directory '{directory}'");

                var gitHelper = new GitHelper();

                // we have to find the root of the repo folder
                string repoRootPath = new PathHelper().FindFolderWithNameInOrAbove(KnownStrings.GitFolderName, directory);

                if (string.IsNullOrEmpty(repoRootPath)) {
                    Console.WriteLine($"Unable to locate repo root folder for '{directory}'");
                    return;
                }

                var repoUrl = new GitHelper().GetGithubUrlForRepo(repoRootPath, remoteName);
                if (string.IsNullOrEmpty(repoUrl)) {
                    Console.WriteLine($"Unable to get github url for remoteName: '{remoteName}' directory: '{directory}'");
                    return;
                }

                var commandName = "start";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                    commandName = "open";
                }

                ICliCommand openCommand = new CliCommand {
                    Command =commandName,
                    Arguments = repoUrl
                };

                _ = await openCommand.RunCommand();

            });
        }
    }
}
