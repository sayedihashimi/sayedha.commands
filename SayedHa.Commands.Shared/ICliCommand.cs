using System.Diagnostics;
using System.Threading.Tasks;

namespace SayedHa.Commands.Shared
{
    public interface ICliCommand
    {
        string Command { get; set; }
        string Arguments { get; set; }
        string WorkingDirectory { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        ProcessWindowStyle WindowStyle { get; set; }
        bool CreateNoWindow { get; set; }
        int TimeoutMilliseconds { get; set; }

        Task<ICliCommandResult> RunCommand();
    }
}