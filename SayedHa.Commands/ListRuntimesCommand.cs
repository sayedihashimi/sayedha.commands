using System;
using System.Diagnostics;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;

namespace SayedHa.Commands {
    public class ListRuntimesCommand : BaseCommandLineApplication {
        public ListRuntimesCommand(IReporter reporter, INetCoreHelper netCoreHelper) : base(
            "listruntimes",
            "listNetCoreRuntimes",
            "Lists the .NET Core runtimes that are installed.") {

            Debug.Assert(reporter != null);
            Debug.Assert(netCoreHelper != null);

            // no options
            this.OnExecute(async () => {
                reporter.Output(await netCoreHelper.GetRuntimesInstalledString());
            });
        }
    }
}
