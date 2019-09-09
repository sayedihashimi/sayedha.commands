using System;
namespace SayedHa.Commands.Shared {
    public interface IRuntimeInfo {
        string Category { get; set; }
        string Version { get; set; }
        string InstallPath { get; set; }
    }

    public class RuntimeInfo : IRuntimeInfo {
        public string Category { get; set; }
        public string Version { get; set; }
        public string InstallPath { get; set; }
    }
}
