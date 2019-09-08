using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;

namespace SayedHa.Commands {
    public class ManageSdksCommand : BaseCommandLineApplication{
        public ManageSdksCommand(IReporter reporter) : base(
            "deletesdks",
            "deletesdks",
            "Enables you to delete .NET Core SDKs that are installed."){

            Debug.Assert(reporter != null);

            // options
            var optionSdkToDelete = this.Option<string>(
                "-v|--version",
                "SDK version to delete",
                CommandOptionType.MultipleValue);
            optionSdkToDelete.IsRequired(errorMessage: "Version to delete is required");

            var optionWhatIf = this.Option<string>(
                "--whatif",
                "Will display the list of folders that would be removed if the command was executed without this switch.",
                CommandOptionType.NoValue);

            this.OnExecute(async () => {
                var sdkVersionsToDelete = optionSdkToDelete.ParsedValues;
                var whatIf = optionWhatIf.HasValue();

                var nchelper = new NetCoreHelper();
                var sdksInstalled = await nchelper.GetSdksInstalled();

                // TODO: This fails if there is more than one install of the same version
                var sdksMap = new Dictionary<string, ISdkInfo>();
                foreach(var sdk in sdksInstalled) {
                    sdksMap.Add(sdk.Version, sdk);
                }

                foreach(var verToDelete in sdkVersionsToDelete) {
                    ISdkInfo todelete;

                    if (!(sdksMap.TryGetValue(verToDelete, out todelete)) ||
                        string.IsNullOrEmpty(todelete?.InstallPath)) {
                        reporter.Warn($"No installed version found for '{verToDelete}'");
                        continue;
                    }

                    if (Directory.Exists(todelete.InstallPath) ){
                        reporter.Output($"Deleting folder {todelete.InstallPath}");

                        if (!whatIf) {
                            // perform deletion
                            Directory.Delete(todelete.InstallPath, true);
                        }
                    }
                    else {
                        reporter.Warn($"SDK directory not found at '{todelete.InstallPath}'");
                    }
                }
            });
        }
    }
}
