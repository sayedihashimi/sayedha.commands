﻿using System;
using SayedHa.Commands.Shared;

namespace SayedHa.Commands {
    public class RestartTouchbarCommand : BaseCommandLineApplication {
        public RestartTouchbarCommand(): base(
            "rt",
            "restart-touchbar",
            "Restarts macOS touchbar") {

            this.OnExecute(async () => {
                // pkill "Touch Bar agent";
                // killall "ControlStrip";

                ICliCommand pkillCmd = new CliCommand {
                    Command = "pkill",
                    Arguments = @"Touch Bar agent",
                    SupressExceptionOnNonZeroExitCode = true
                };

                var res1 = await pkillCmd.RunCommand();

                var res2 = await new CliCommand {
                    Command = "killall",
                    Arguments = @"ControlStrip",
                    SupressExceptionOnNonZeroExitCode = true
                }.RunCommand();

                return 0;
            });

        }
    }
}
