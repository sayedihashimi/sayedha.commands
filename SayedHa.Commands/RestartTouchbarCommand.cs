using System;
using SayedHa.Commands.Shared;

namespace SayedHa.Commands {
    public class RestartTouchbarCommand : BaseCommandLineApplication {
        public RestartTouchbarCommand(): base(
            "rt",
            "restart-touchbar",
            "Restarts macOS touchbar") {

        }
    }
}
