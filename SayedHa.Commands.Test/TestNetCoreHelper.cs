using System;
using System.Threading.Tasks;
using Moq;
using SayedHa.Commands.Shared;
using Xunit;
using System.Linq;

namespace SayedHa.Commands.Test {
    public class TestNetCoreHelper {

        [Fact]
        public async Task TestGetSdkPaths() {
            var nchelper = GetNewNetCoreHelperMock();
            var result = await nchelper.GetSdksInstalledAsync();

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task TestGetRuntimesInstalled() {
            var nchelper = GetNewNetCoreHelperMock();
            var result = await nchelper.GetRuntimesInstalledAsync();

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task TestGetRuntimesByVersion01Async() {
            //var mock = new Mock<NetCoreHelper> { CallBase = true };
            //mock.Setup(m => m.GetRuntimesInstalledString()).ReturnsAsync(SampleRuntimesString01);

            var mock = GetNewNetCoreHelperMock();
            var result = await mock.GetRuntimesInstalledString();
            var installedRuntimes = await mock.GetRuntimesInstalledAsync();

            var versionsInstalled = (from ir in installedRuntimes
                                     select ir.Version).ToList();

            Assert.True(string.Compare(SampleRuntimesString01, result, StringComparison.OrdinalIgnoreCase) == 0);
            Assert.True(versionsInstalled.Contains("2.1.2"));
            Assert.True(versionsInstalled.Contains("3.0.0-preview8.19405.7"));
            Assert.True(versionsInstalled.Contains("2.1.11"));
        }

        private INetCoreHelper GetNewNetCoreHelperMock() {
            var mock = new Mock<NetCoreHelper> { CallBase = true };
            mock.Setup(m => m.GetRuntimesInstalledString()).ReturnsAsync(SampleRuntimesString01);
            mock.Setup(m => m.GetSdksInstalledString()).ReturnsAsync(SampleSdksString01);
            return mock.Object;
        }

        private const string SampleRuntimesString01 = @"Microsoft.AspNetCore.All 2.1.2 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.All]
Microsoft.AspNetCore.All 2.1.11 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.All]
Microsoft.AspNetCore.All 2.2.5 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.All]
Microsoft.AspNetCore.App 2.1.2 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
Microsoft.AspNetCore.App 2.1.11 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
Microsoft.AspNetCore.App 2.2.5 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
Microsoft.AspNetCore.App 3.0.0-preview8.19405.7 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
Microsoft.NETCore.App 1.0.16 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
Microsoft.NETCore.App 1.1.13 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
Microsoft.NETCore.App 2.0.3 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
Microsoft.NETCore.App 2.1.2 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
Microsoft.NETCore.App 2.1.11 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
Microsoft.NETCore.App 2.2.5 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
Microsoft.NETCore.App 3.0.0-preview8-28405-07 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]";

        private const string SampleSdksString01 = @"1.1.14 [/usr/local/share/dotnet/sdk]
2.1.2 [/usr/local/share/dotnet/sdk]
2.1.400 [/usr/local/share/dotnet/sdk]
2.1.700 [/usr/local/share/dotnet/sdk]
2.2.300 [/usr/local/share/dotnet/sdk]
3.0.100-preview8-013656 [/usr/local/share/dotnet/sdk]
3.0.100-preview9-014004 [/usr/local/share/dotnet/sdk]";
    }
}
