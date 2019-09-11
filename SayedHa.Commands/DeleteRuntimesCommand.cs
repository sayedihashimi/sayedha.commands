using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;
using System.Linq;

namespace SayedHa.Commands {
    public class DeleteRuntimesCommand : BaseCommandLineApplication {
        public DeleteRuntimesCommand(IReporter reporter, INetCoreHelper netCoreHelper) : base(
            "deleteruntimes",
            "deleteNetCoreRuntimes",
            "Deletes .NET Core runtimes that are installed."
            ) {

            Debug.Assert(reporter != null);
            Debug.Assert(netCoreHelper != null);

            var optionRuntimeToDelete = this.Option<string>(
                "-v|--version",
                "Version of the runtime to be deleted. If there are multiple runtimes, in different categories matching the version and the category parameter is not passed then all will be deleted.",
                CommandOptionType.MultipleValue);
            optionRuntimeToDelete.IsRequired(errorMessage: "Version to delete is required.");

            var optionCategoryToDelete = this.Option<string>(
                "-cat|--category",
                "Category for the runtime installation (for example 'Microsoft.NETCore.App' or 'Microsoft.AspNetCore.All)'",
                CommandOptionType.MultipleValue);

            var optionWhatIf = this.Option<string>(
                "-w|--whatif",
                "Will display the list of folders that would be removed if the command was executed without this switch.",
                CommandOptionType.NoValue);

            this.OnExecute(async () => {
                var runtimeVersionsToDelete = optionRuntimeToDelete.ParsedValues;
                var categories = optionCategoryToDelete.HasValue() ?
                                    optionCategoryToDelete.ParsedValues :
                                    null;

                var whatif = optionWhatIf.HasValue();

                var runtimesInstalled = await netCoreHelper.GetRuntimesInstalledAsync();

                var runtimesToInspect = runtimesInstalled;
                // filter installed by category if passed
                if(categories != null && categories.Count > 0) {
                    // runtimesToInspect = new List<IRuntimeInfo>();
                    // filter out any runtime that is not in the listed categories
                    
                }

                foreach(var rt in runtimesInstalled) {
                    if (!(categories.Any(cat => cat.Equals(rt.Category, StringComparison.OrdinalIgnoreCase)))) {
                        runtimesToInspect.Remove(rt);
                    }
                    else if (!(runtimeVersionsToDelete.Any(r => r.Equals(rt.Version, StringComparison.OrdinalIgnoreCase)))) {
                        runtimesToInspect.Remove(rt);
                    }
                }



                /*
                 * if( testList.Any( s => s.Equals(keyword, StringComparison.OrdinalIgnoreCase) ) )
                   Console.WriteLine("found in list");
                 */

                foreach (var todelete in runtimeVersionsToDelete) {
                    // get each runtime that matches
                    foreach(var runtime in runtimeVersionsToDelete) { }
                }

            });
        }
    }
}
