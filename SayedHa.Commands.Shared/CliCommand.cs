using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace SayedHa.Commands.Shared {
    public class CliCommand : ICliCommand {
        public string Command { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; } = Directory.GetCurrentDirectory();
        public string Username { get; set; }
        public string Password { get; set; }
        public ProcessWindowStyle WindowStyle { get; set; } = ProcessWindowStyle.Hidden;
        public bool CreateNoWindow { get; set; } = true;
        public int TimeoutMilliseconds { get; set; }
        public bool SupressExceptionOnNonZeroExitCode { get; set; }

        public async Task<ICliCommandResult> RunCommand() {
            var startInfo = new ProcessStartInfo {
                FileName = Command,
                CreateNoWindow = CreateNoWindow,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = WorkingDirectory
            };

            if (!string.IsNullOrWhiteSpace(Arguments)) {
                startInfo.Arguments = Arguments;
            }

            if (!string.IsNullOrWhiteSpace(Username)) {
                startInfo.UserName = Username;
            }
            if (!string.IsNullOrWhiteSpace(Password)) {
#if WINDOWS
                startInfo.PasswordInClearText = Password;
#endif
            }

            string stdout = null;
            string stderr = null;
            Exception exception = null;
            int exitCode = int.MinValue;
            try {
                using var cmdProcess = Process.Start(startInfo);
                if (TimeoutMilliseconds > 0) {
                    cmdProcess.WaitForExit(TimeoutMilliseconds);
                }
                else {
                    cmdProcess.WaitForExit();
                }
                exitCode = cmdProcess.ExitCode;
                stdout = await cmdProcess.StandardOutput.ReadToEndAsync();
                stderr = await cmdProcess.StandardError.ReadToEndAsync();
            }
            catch (Exception ex) {
                exception = ex;
            }

            ICliCommandResult result = new CliCommandResult {
                ExitCode = exitCode,
                StandardOutput = stdout,
                StandardError = stderr,
                Exception = exception
            };

            if(result.ExitCode != 0 && !SupressExceptionOnNonZeroExitCode) {
                throw new CliCommandReturnedNonZeroExitCodeException($"Exited with code {result.ExitCode}");
            }

            return result;
        }
    }
}
