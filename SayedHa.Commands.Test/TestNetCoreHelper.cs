﻿using System;
using System.Threading.Tasks;
using Moq;
using SayedHa.Commands.Shared;
using Xunit;
using System.Linq;
using System.Collections.Generic;

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

            var netcorehelper = GetNewNetCoreHelperMock();
            var result = await netcorehelper.GetRuntimesInstalledString();
            var installedRuntimes = await netcorehelper.GetRuntimesInstalledAsync();

            var versionsInstalled = (from ir in installedRuntimes
                                     select ir.Version).ToList();

            Assert.True(string.Compare(SampleRuntimesString01, result, StringComparison.OrdinalIgnoreCase) == 0);
            Assert.Contains<string>("2.1.2", versionsInstalled);
            Assert.Contains<string>("3.0.0-preview8.19405.7", versionsInstalled);
            Assert.Contains<string>("2.1.11", versionsInstalled);
        }

        [Fact]
        private async Task TestGetRuntimesInstalledAsync_OneVersionAsync() {
            var netcorehelper = GetNewNetCoreHelperMock();

            var result = await netcorehelper.GetRuntimesInstalledAsync(new List<string> { "2.1.2" }, null);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count == 3);
            Assert.Equal(0, string.Compare("2.1.2", result[0].Version));
            Assert.Equal(0, string.Compare("2.1.2", result[1].Version));
            Assert.Equal(0, string.Compare("2.1.2", result[2].Version));
        }

        private INetCoreHelper GetNewNetCoreHelperMock() {
            var netcorehelper = new Mock<NetCoreHelper> { CallBase = true };
            netcorehelper.Setup(m => m.GetRuntimesInstalledString()).ReturnsAsync(SampleRuntimesString01);
            netcorehelper.Setup(m => m.GetSdksInstalledString()).ReturnsAsync(SampleSdksString01);
            return netcorehelper.Object;
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
