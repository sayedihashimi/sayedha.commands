using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using SayedHa.Commands.Shared;
using Xunit;

namespace SayedHa.Commands.Test {
    public class TestWebHelper {
        [Fact]
        public async Task TestDownloadFileSuccess01Async() {
            string url = @"https://raw.githubusercontent.com/github/gitignore/master/VisualStudio.gitignore";
            string destFile = Path.GetTempFileName();
            
            if (File.Exists(destFile)) File.Delete(destFile);

            Assert.True(!File.Exists(destFile));
            await new WebHelper().DownloadFile(url, destFile, true);
            Assert.True(File.Exists(destFile));
        }

        [Fact]
        public async Task TestDownloadFileNotExists() {
            string url = @"https://raw.githubusercontent.com/github/gitignore/master/VisualStudio.gitignoreddddddddddd";
            string destFile = Path.GetTempFileName();

            if (File.Exists(destFile)) File.Delete(destFile);

            try {
                await new WebHelper().DownloadFile(url, destFile, true);
            }
            catch(WebException ex) {
                Assert.True(WebExceptionStatus.ProtocolError == ex.Status);
            }
        }
    }
}
