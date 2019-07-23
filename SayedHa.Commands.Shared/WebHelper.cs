using System;
using System.Diagnostics;
using System.IO;
using SayedHa.Commands.Shared.Extensions;

namespace SayedHa.Commands.Shared {
    public class WebHelper {
        public void DownloadFile(string url, string destFilepath, bool allowOverwrite = false) {
            Debug.Assert(!string.IsNullOrEmpty(url));
            Debug.Assert(!string.IsNullOrEmpty(destFilepath));

            if(!allowOverwrite && File.Exists(destFilepath)) {
                throw new FileExistsException($"File already exists at destination, and overwrite is set to false, path='{destFilepath}'");
            }


            throw new NotImplementedException();
        }
    }
}
