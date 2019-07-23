using System;
using System.IO;
using SayedHa.Commands.Shared;
using Xunit;

namespace SayedHa.Commands.Test {
    public class TestWebHelper {
        [Fact]
        public async System.Threading.Tasks.Task TestDownloadFileSuccess01Async() {
            string url = @"https://raw.githubusercontent.com/github/gitignore/master/VisualStudio.gitignore";
            string destFile = Path.GetTempFileName();
            
            if (File.Exists(destFile)) File.Delete(destFile);

            Assert.True(!File.Exists(destFile));
            await new WebHelper().DownloadFile(url, destFile, true);
            Assert.True(File.Exists(destFile));
            

        }
    }
}
