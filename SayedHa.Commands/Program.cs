using System;
using McMaster.Extensions.CommandLineUtils;

namespace SayedHa.Commands {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello Sayed!");

            using var app = new CommandLineApplication {
                Name = "sayedha",
                UsePagerForHelpText = false
            };

            app.HelpOption(inherited: true);


        }
    }
}
 