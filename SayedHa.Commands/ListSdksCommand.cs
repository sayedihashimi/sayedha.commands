using System;
using System.Diagnostics;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;

namespace SayedHa.Commands {
    public class ListSdksCommand : BaseCommandLineApplication {
        public ListSdksCommand(IReporter reporter, INetCoreHelper netCoreHelper) : base(
            "listSdks",
            "listNetScoreSdks",
            "Lists thet .NET Core SDKs that are installed."){

            Debug.Assert(netCoreHelper != null);

            // no options
            this.OnExecute(async () => {
                reporter.Output(await netCoreHelper.GetSdksInstalledString());
            });
        }
    }
}
