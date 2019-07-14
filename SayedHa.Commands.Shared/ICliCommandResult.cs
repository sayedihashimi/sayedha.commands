namespace SayedHa.Commands.Shared
{
    public interface ICliCommandResult
    {
        int ExitCode { get; set; }
        string StandardOutput { get; set; }
        string StandardError { get; set; }
        System.Exception Exception { get; set; }
    }
}