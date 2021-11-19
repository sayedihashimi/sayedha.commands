using System;
using SayedHa.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SayedHa.Commands {
    public class OpenGithubCommand : BaseCommandLineApplication {
        protected IUrlHelper _urlHelper;
        public OpenGithubCommand(IUrlHelper urlHeper) : base(
            "og",
            "OpenGithub",
            "If the repo has an origin pointing to github.com the project page will be opened.") {

            _urlHelper = urlHeper;
            // options
            var optionDirectory = this.Option<string>(
                "-d|--directory",
                "Directory of git repo that will be used to find the proejct URL. Default is pwd",
                CommandOptionType.SingleValue);

            var optionRemoteName = this.Option<string>(
                "-o|--origin",
                $"Specifies the name of the remote to look for. Default: {KnownStrings.DefaultRemoteName}",
                CommandOptionType.SingleValue);

            this.OnExecute(() => {
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

                _urlHelper.OpenUrl(repoUrl);
                // below code works but I believe the url helper should work as well let's see
                //var commandName = "cmd.exe";
                //var args = $"start {repoUrl}";
                //if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                //    commandName = "open";
                //    args = repoUrl;
                //    ICliCommand openCommand = new CliCommand
                //    {
                //        Command = commandName,
                //        Arguments = args
                //    };
                //    _ = await openCommand.RunCommand();
                //}
                //else
                //{
                //    //System.Diagnostics.Process.Start(repoUrl);
                //    OpenUrl(repoUrl);
                //}
            });
        }
        // taken from https://stackoverflow.com/a/43232486
        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
