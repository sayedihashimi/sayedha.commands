using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SayedHa.Commands.Shared.Extensions;

namespace SayedHa.Commands.Shared {
    public class WebHelper : IWebHelper {
        private static readonly HttpClient httpClient = new HttpClient();

        public async Task DownloadFile(string url, string destFilepath, bool allowOverwrite = false) {
            Debug.Assert(!string.IsNullOrEmpty(url));
            Debug.Assert(!string.IsNullOrEmpty(destFilepath));

            if (allowOverwrite || !File.Exists(destFilepath)) {
                var data = await httpClient.GetByteArrayAsync(url);
                using var content = new MemoryStream(data);
                using var outstream = new FileStream(destFilepath, FileMode.Create);
                await content.CopyToAsync(outstream);
            }
        }
    }
}
