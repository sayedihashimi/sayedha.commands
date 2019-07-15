using System;
using SayedHa.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;
using System.IO;

namespace SayedHa.Commands {
    public class OpenGithubCommand : BaseCommandLineApplication {
        protected string _githubPattern = @"origin\tgit@github.com:([a-zA-S-.]+)/([a-zA-Z-]+)\.git.*(fetch\))";
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

                var originalPwd = Directory.GetCurrentDirectory();

                if (new PathHelper().ArePathsEqual(originalPwd, directory)) {

                }





            });

        }
    }
}
