using System.Threading.Tasks;

namespace SayedHa.Commands.Shared {
    public interface IWebHelper {
        Task DownloadFile(string url, string destFilepath, bool allowOverwrite = false);
    }
}