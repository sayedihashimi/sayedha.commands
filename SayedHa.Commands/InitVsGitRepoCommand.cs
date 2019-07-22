using System;
using SayedHa.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace SayedHa.Commands {
    public class InitVsGitRepoCommand : BaseCommandLineApplication {
        public InitVsGitRepoCommand() : base(
            "initvsrepo",
            "initvsgitrepo",
            "Initalize the directory as a new git repo. It will call git init, add a .gitignore and a .gitattributes and also do an initial commit.") {

            this.OnExecute(() => {
                Console.WriteLine(@"To be implemented");
            });
        }
    }
}
