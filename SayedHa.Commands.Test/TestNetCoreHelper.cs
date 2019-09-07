using System;
using System.Threading.Tasks;
using SayedHa.Commands.Shared;
using Xunit;

namespace SayedHa.Commands.Test {
    public class TestNetCoreHelper {

        [Fact]
        public async Task TestGetSdkPaths() {
            var nchelper = new NetCoreHelper();
            var result = await nchelper.GetSdksInstalled();

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }
    }
}
