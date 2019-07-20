using System;
using SayedHa.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace SayedHa.Commands {
    public class CloneRepoCommand : BaseCommandLineApplication {
        

        public CloneRepoCommand():base(
            "clonerepo",
            "clonegithubrepo",
            "Will clone a github repo based on the account and repo name."){

            // arguments
            var argumentRepoUrl = this.Argument<string>(
                "repourl",
                "URL for the github repo to be cloned. If this argument is passed and options for account or repo name are passed, the option value wins.");

            // options
            var optionAccountName = this.Option<string>(
                "--a|--accountname",
                "Name of the github account to be cloned.",
                CommandOptionType.SingleValue);

            var optionRepoName = this.Option<string>(
                "-r|--reponame",
                "Name of the github repo to be cloned",
                CommandOptionType.SingleValue);

            var optionDirectory = this.Option<string>(
                "-d|--directory",
                "The folder where the repo will be cloned into. The clone operation will be in a sub folder of this folder. Default folder is the current working directory",
                CommandOptionType.SingleValue);

            var optionUseSsh = this.Option<string>(
                "---ssh",
                "Use ssh for the clone operation.",
                CommandOptionType.NoValue);

            var optionUseHttps = this.Option<string>(
                "--https",
                "Use https for the clone operation",
                CommandOptionType.NoValue);

            this.OnExecute(() => {
                var repoUrl = argumentRepoUrl.Value;

                var acctAndReopFromUrl = new GitHelper().GetAccountAndRepoNameFromUrl(repoUrl);

                var accountName = optionAccountName.HasValue()
                    ? optionAccountName.ParsedValue
                    : acctAndReopFromUrl.accountName;

                var repoName = optionRepoName.HasValue()
                    ? optionRepoName.ParsedValue
                    : acctAndReopFromUrl.repoName;

                var cloneMethod = CloneMethod.ssh;

                if (optionUseHttps.HasValue()) {
                    cloneMethod = CloneMethod.https;
                }


            });
        }

    }
}
