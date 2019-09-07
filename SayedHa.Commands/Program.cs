﻿using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace SayedHa.Commands {
    class Program {
        static void Main(string[] args) {
            new Program(args).Run();
        }

        protected string[] _args;
        protected ServiceCollection _services = null;
        protected ServiceProvider _serviceProvider = null;

        private Program(string[] args) {
            _args = args;
            RegisterServices();
        }

        private void Run() {
            using var app = new CommandLineApplication {
                Name = "sayedha",
                UsePagerForHelpText = false
            };

            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection();

            app.HelpOption(inherited: true);

            app.Commands.Add(new ConvertClipboardToPlainText());
            app.Commands.Add(new OpenGithubCommand());
            app.Commands.Add(new RemoveSubfoldersCommand());
            app.Commands.Add(new CloneRepoCommand());
            app.Commands.Add(new InitVsGitRepoCommand());
            app.Commands.Add(new RegexTesterCommand(GetFromServices<IReporter>()));
            app.Commands.Add(new ManageSdksCommand(GetFromServices<IReporter>()));
            app.Execute(_args);
        }

        protected void RegisterServices() {
            _services = new ServiceCollection();
            _serviceProvider = _services.AddSingleton(typeof(IConsole), PhysicalConsole.Singleton)
                                        .AddSingleton<IReporter,ConsoleReporter>()
                                        .BuildServiceProvider();
        }

        private TType GetFromServices<TType>() {
            return _serviceProvider.GetRequiredService<TType>();
        }
    }
}