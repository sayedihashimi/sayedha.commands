using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace SayedHa.Commands.Shared {
    public class NetCoreHelper : INetCoreHelper {
        public async Task<List<SdkInfo>> GetSdksInstalledAsync() {
            return await GetSdksInstalledAsync(true);
        }

        public async Task<List<SdkInfo>> GetSdksInstalledAsync(bool verifyFolderIsOnDisk) {
            string sdksInstalledStr = await GetSdksInstalledString();
            if (string.IsNullOrEmpty(sdksInstalledStr)) { return null; }

            List<SdkInfo> sdksInstalled = new List<SdkInfo>();

            // process each line of the output at a time
            var sdkRegex = new Regex(Strings.GetSdksInstalledRegex, RegexOptions.Compiled);
            foreach (var line in sdksInstalledStr.Split('\n')) {
                var result = sdkRegex.Match(line);
                if (result.Success) {
                    string version = result.Groups?[1]?.Value;
                    string installBasePath = result.Groups?[2]?.Value;

                    if (string.IsNullOrEmpty(version) ||
                        string.IsNullOrEmpty(installBasePath)) {
                        continue;
                    }
                    var sdkInfo = new SdkInfo {
                        InstallPath = Path.Combine(installBasePath, version),
                        Version = version
                    };

                    if (!verifyFolderIsOnDisk || Directory.Exists(sdkInfo.InstallPath)) {
                        sdksInstalled.Add(sdkInfo);
                    }
                    else {
                        // shouldn't get here
                        Console.Error.WriteLine($@"SDK directory not found at {sdkInfo.InstallPath}");
                    }
                }
            }

            return sdksInstalled;
        }

        // marking virtual so it can be mocked
        public virtual async Task<string> GetSdksInstalledString() {
            // run the command dotnet --list-sdks and return the result

            var cliCommand = new CliCommand {
                Command = "dotnet",
                Arguments = Strings.ListSdksCommandArgument
            };

            return (await cliCommand.RunCommand()).StandardOutput;
        }

        // marking virtual so it can be mocked
        public virtual async Task<string> GetRuntimesInstalledString() {
            // run the command dotnet --list-runtimes and return the result

            var cliCommand = new CliCommand {
                Command = "dotnet",
                Arguments = Strings.ListRuntimesCommandArgument
            };

            return (await cliCommand.RunCommand()).StandardOutput;
        }
        public async Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync() {
            return await GetRuntimesInstalledAsync(true);
        }
        public async Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync(bool verifyFolderIsOnDisk) {
            // TODO: Should try to just call this once and save the value
            string runtimesInstalledString = await GetRuntimesInstalledString();
            if (string.IsNullOrEmpty(runtimesInstalledString)) { return null; }

            List<IRuntimeInfo> runtimesInstalled = new List<IRuntimeInfo>();
            var runtimesRegex = new Regex(Strings.GetRuntimesInstalledRegex, RegexOptions.Compiled);
            foreach (var line in runtimesInstalledString.Split('\n')) {
                var result = runtimesRegex.Match(line);
                if (result.Success) {
                    string category = result.Groups?[1]?.Value;
                    string version = result.Groups?[2]?.Value;
                    string installBasePath = result.Groups?[3]?.Value;

                    if (string.IsNullOrEmpty(category) ||
                        string.IsNullOrEmpty(version) ||
                        string.IsNullOrEmpty(installBasePath)) {
                        continue;
                    }

                    var runtimeInfo = new RuntimeInfo {
                        Category = category,
                        Version = version,
                        InstallPath = Path.Combine(installBasePath, version)
                    };

                    if (!verifyFolderIsOnDisk || Directory.Exists(runtimeInfo.InstallPath)) {
                        runtimesInstalled.Add(runtimeInfo);
                    }
                    else {
                        Console.Error.WriteLine($"Runtime directory not found at {runtimeInfo.InstallPath}");
                    }
                }
            }

            return runtimesInstalled;
        }

        public async Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync(IList<string> versions, IList<string> categories) {
            return await GetRuntimesInstalledAsync(versions, categories, true);
        }
        /// <summary>
        /// Gets the runtimes installed by version and category. Version is required but category is optional.
        /// If there is a match of a given version, and no category is passed all versions will be returned.
        /// </summary>
        /// <param name="versions">Required parameter</param>
        /// <param name="categories">Optional</param>
        public async Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync(IList<string> versions, IList<string> categories, bool verifyFolderIsOnDisk) {
            var runtimesInstalled = await GetRuntimesInstalledAsync(verifyFolderIsOnDisk);
            var runtimesMatched = runtimesInstalled.ToList();

            foreach(var rt in runtimesInstalled) {
                if ( categories != null &&
                    !(categories.Any(cat => cat.Equals(rt.Category, StringComparison.OrdinalIgnoreCase)))) {
                    runtimesMatched.Remove(rt);
                }
                else if (versions != null &&
                        !(versions.Any(r => r.Equals(rt.Version, StringComparison.OrdinalIgnoreCase)))) {
                    runtimesMatched.Remove(rt);
                }
            }
            
            return runtimesMatched;
        }
    }
}
