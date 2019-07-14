using System;

namespace SayedHa.Commands.Shared
{
    public class CliCommandResult : ICliCommandResult
    {
        public int ExitCode { get; set; }
        public string StandardOutput { get; set; }
        public string StandardError { get; set; }
        public Exception Exception { get; set; }
    }
}
