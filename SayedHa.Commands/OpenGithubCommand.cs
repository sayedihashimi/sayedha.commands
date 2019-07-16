using System;
using SayedHa.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Text.RegularExpressions;

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



            this.OnExecute(() => {
                var directory = optionDirectory.HasValue()
                    ? optionDirectory.ParsedValue
                    : Directory.GetCurrentDirectory();

                Console.WriteLine($"Finding proejct url in directory '{directory}'");

                var gitHelper = new GitHelper();
                var origin = gitHelper.GetNameAndPushUrlForRemote(directory, "origin");

                var regex = new Regex(_gitRemoteGithubPattern, RegexOptions.Compiled);
                var match = regex.Match(origin.pushUrl);

                Console.WriteLine("foo");


            });

        }
    }
}
