using McMaster.Extensions.CommandLineUtils;

namespace SayedHa.Commands.Shared {
    public class BaseCommandLineApplication : CommandLineApplication {
        public BaseCommandLineApplication(
            string name,
            string fullname,
            string description,
            bool enableVerboseOption = true,
            bool usePagerForHelp = false) {

            Name = name;
            FullName = fullname;
            Description = description;
            this.HelpOption();
            UsePagerForHelpText = usePagerForHelp;

            if (enableVerboseOption) {
                VerboseOption = this.Option<bool>(
                    "--verbose",
                    "Enable verbose logging",
                    CommandOptionType.NoValue);
            }
        }

        protected CommandOption<bool> VerboseOption { get; set; }
        protected bool VerboseEnabled {
            get {
                return VerboseOption != null && VerboseOption.HasValue();
            }
        }
    }
}
