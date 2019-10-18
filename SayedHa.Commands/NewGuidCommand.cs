using System;
using System.Diagnostics;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;

namespace SayedHa.Commands {
    public class NewGuidCommand : BaseCommandLineApplication {
        public NewGuidCommand(IReporter reporter) : base(
            "newguid",
            "newguid",
            "Creates a new guid") {

            Debug.Assert(reporter != null);

            // options
            var optionNumguids = this.Option<int>(
                "-n|--num",
                "Generates one, or more, new guids",
                CommandOptionType.SingleValue);

            this.OnExecute( () => {
                int numguids = optionNumguids.HasValue() ?
                                optionNumguids.ParsedValue :
                                1;

                for(int i = 0; i < numguids; i++) {
                    reporter.Output(Guid.NewGuid().ToString());
                }
            });
        }
    }
}
