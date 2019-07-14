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

            app.Execute(args);

        }
    }
}
 