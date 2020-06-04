using System;
using System.Runtime.InteropServices;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using SayedHa.Commands.Shared;

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
            app.Commands.Add(new OpenGithubCommand(GetFromServices<IUrlHelper>()));
            app.Commands.Add(new RemoveSubfoldersCommand());
            app.Commands.Add(new CloneRepoCommand());
            app.Commands.Add(new InitVsGitRepoCommand(
                                    GetFromServices<IReporter>(),
                                    GetFromServices<IPathHelper>(),
                                    GetFromServices<IWebHelper>()));
            app.Commands.Add(new RegexTesterCommand(GetFromServices<IReporter>()));
            app.Commands.Add(new ListSdksCommand(
                                     GetFromServices<IReporter>(),
                                     GetFromServices<INetCoreHelper>()));
            app.Commands.Add(new DeleteSdksCommand(
                                     GetFromServices<IReporter>(),
                                     GetFromServices<INetCoreHelper>()));
            app.Commands.Add(new ListRuntimesCommand(
                                     GetFromServices<IReporter>(),
                                     GetFromServices<INetCoreHelper>()));
            app.Commands.Add(new DeleteRuntimesCommand(
                                     GetFromServices<IReporter>(),
                                     GetFromServices<INetCoreHelper>()));

            app.Commands.Add(new NewGuidCommand(GetFromServices<IReporter>()));
            
            app.Commands.Add(new LoremIpsumCommand());

            // macOS specific commands
            if (IsRunningOnMacOs()) {
                app.Commands.Add(new RestartTouchbarCommand());
            }

            app.Execute(_args);
        }

        protected void RegisterServices() {
            _services = new ServiceCollection();
            _serviceProvider = _services.AddSingleton(typeof(IConsole), PhysicalConsole.Singleton)
                                        .AddSingleton<IReporter,ConsoleReporter>()
                                        .AddScoped<INetCoreHelper,NetCoreHelper>()
                                        .AddScoped<IPathHelper,PathHelper>()
                                        .AddScoped<IWebHelper,WebHelper>()
                                        .AddScoped<IUrlHelper,UrlHelper>()
                                        .BuildServiceProvider();
        }

        private TType GetFromServices<TType>() {
            return _serviceProvider.GetRequiredService<TType>();
        }

        private bool IsRunningOnMacOs() {
            return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }
    }
}