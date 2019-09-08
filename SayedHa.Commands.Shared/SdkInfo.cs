using System;
namespace SayedHa.Commands.Shared {

    public interface ISdkInfo {
        string Version { get; set; }
        string InstallPath { get; set; }
    }

    public class SdkInfo : ISdkInfo {
        public string Version { get; set; }
        public string InstallPath { get; set; }
    }
}
