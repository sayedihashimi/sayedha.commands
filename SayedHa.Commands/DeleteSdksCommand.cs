﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;

namespace SayedHa.Commands {
    public class DeleteSdksCommand : BaseCommandLineApplication{
        public DeleteSdksCommand(IReporter reporter,INetCoreHelper netCoreHelper) : base(
            "deletesdks",
            "deletesdks",
            "Deletes .NET Core SDKs that are installed."){

            Debug.Assert(reporter != null);
            Debug.Assert(netCoreHelper != null);

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

                var sdksInstalled = await netCoreHelper.GetSdksInstalledAsync();

                foreach(var verToDelete in sdkVersionsToDelete) {
                    bool foundVerLocalPathToDelete = false;
                    foreach(var sdkInfo in sdksInstalled) {
                        if (string.Compare(verToDelete, sdkInfo.Version, StringComparison.OrdinalIgnoreCase) != 0) {
                            continue;
                        }

                        if (Directory.Exists(sdkInfo.InstallPath)) {
                            reporter.Output($"Deleting folder {sdkInfo.InstallPath}");
                            foundVerLocalPathToDelete = true;

                            if (!whatIf) {
                                // perform deletion
                                Directory.Delete(sdkInfo.InstallPath, true);
                            }
                        }
                        else {
                            reporter.Warn($"SDK directory not found at '{sdkInfo.InstallPath}'");
                        }
                    }

                    if (!foundVerLocalPathToDelete) {
                        reporter.Warn($"No installed version found for '{verToDelete}'");
                    }
                }
            });
        }
    }
}