using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using SayedHa.Commands.Shared.Extensions;

namespace SayedHa.Commands.Shared {
    public class WebHelper {
        public async Task DownloadFile(string url, string destFilepath, bool allowOverwrite = false) {
            Debug.Assert(!string.IsNullOrEmpty(url));
            Debug.Assert(!string.IsNullOrEmpty(destFilepath));

            if(!allowOverwrite && File.Exists(destFilepath)) {
                throw new FileExistsException($"File already exists at destination, and overwrite is set to false, path='{destFilepath}'");
            }

            using var client = new WebClient();
            var data = client.DownloadData(url);
            using var content = new MemoryStream(data);
            using var outstream = new FileStream(destFilepath, FileMode.Create);
            await content.CopyToAsync(outstream);

            content.Close();
            outstream.Close();
        }
    }
}
