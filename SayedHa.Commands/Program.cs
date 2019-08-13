using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

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
            app.Commands.Add(new RegexTesterCommand());

            app.Execute(args);
        }

        protected string[] _args;
        protected ServiceCollection services = null;

        private Program(string[] args) {
            _args = args;
            RegisterServices();

        }

        protected void RegisterServices() {
            services.AddSingleton<IConsole,PhysicalConsole.Singleton>();
        }
    }

    //public static int Main2(string[] args) {
    //    var services = new ServiceCollection()
    //        .AddSingleton<IMyService, MyServiceImplementation>()
    //        .AddSingleton<IConsole>(PhysicalConsole.Singleton)
    //        .BuildServiceProvider();

    //    var app = new CommandLineApplication<Program>();
    //    app.Conventions
    //        .UseDefaultConventions()
    //        .UseConstructorInjection(services);
    //    return app.Execute(args);
    //}
}
