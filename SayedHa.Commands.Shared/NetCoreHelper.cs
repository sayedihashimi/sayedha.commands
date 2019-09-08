using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SayedHa.Commands.Shared {
    public class NetCoreHelper {
        public async Task<List<SdkInfo>> GetSdksInstalled() {
            string sdksInstalledStr = await GetSdksInstalledString();
            if (string.IsNullOrEmpty(sdksInstalledStr)) { return null; }

            List<SdkInfo> sdksInstalled = new List<SdkInfo>();

            // process each line of the output at a time
            var sdkRegex = new Regex(Strings.GetSdksInstalledRegex,RegexOptions.Compiled);
            foreach(var line in sdksInstalledStr.Split('\n')) {
                var result = sdkRegex.Match(line);
                if (result.Success) {
                    string version = result.Groups?[1]?.Value;
                    string installBasePath = result.Groups?[2]?.Value;

                    if(string.IsNullOrEmpty(version) ||
                        string.IsNullOrEmpty(installBasePath)) {
                        continue;
                    }
                    var sdkInfo = new SdkInfo {
                        InstallPath = Path.Combine(installBasePath, version),
                        Version = version
                    };

                    if (Directory.Exists(sdkInfo.InstallPath)) {
                        sdksInstalled.Add(sdkInfo);
                    }
                    else {
                        // shouldn't get here
                        Console.Error.WriteLine($@"SDK directory not found at ${sdkInfo.InstallPath}");
                    }
                }
            }

            return sdksInstalled;
        }

        protected async Task<string> GetSdksInstalledString() {
            // run the command dotnet --list-sdks and return the result

            var cliCommand = new CliCommand {
                Command="dotnet",
                Arguments="--list-sdks"
            };

            var commandResult = await cliCommand.RunCommand();

            return commandResult.StandardOutput;
        }
    }
}
