using System;
using System.Threading.Tasks;
using SayedHa.Commands.Shared;
using Xunit;

namespace SayedHa.Commands.Test {
    public class TestNetCoreHelper {

        [Fact]
        public async Task TestGetSdkPaths() {
            var nchelper = new NetCoreHelper();
            var result = await nchelper.GetSdksInstalledAsync();

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task TestGetRuntimesInstalled() {
            var nchelper = new NetCoreHelper();
            var result = await nchelper.GetRuntimesInstalledAsync();

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }
    }
}
