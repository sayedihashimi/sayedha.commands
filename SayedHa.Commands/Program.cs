using System;
using McMaster.Extensions.CommandLineUtils;

namespace SayedHa.Commands {
    class Program {
        static void Main(string[] args) {
            using var app = new CommandLineApplication {
                Name = "sayedha",
                UsePagerForHelpText = false
            };

            app.HelpOption(inherited: true);

            app.Commands.Add(new ConvertClipboardToPlainText());
            app.Commands.Add(new OpenGithubCommand());
            app.Commands.Add(new RemoveSubfoldersCommand());
            app.Commands.Add(new CloneRepoCommand());
            app.Commands.Add(new InitVsGitRepoCommand());

            app.Execute(args);

        }
    }
}
 