using System.Collections.Generic;
using System.Threading.Tasks;

namespace SayedHa.Commands.Shared {
    public interface INetCoreHelper {
        Task<List<SdkInfo>> GetSdksInstalledAsync();
        Task<List<SdkInfo>> GetSdksInstalledAsync(bool verifyFolderIsOnDisk);

        Task<string> GetSdksInstalledString();

        Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync();
        Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync(bool verifyFolderIsOnDisk);

        Task<string> GetRuntimesInstalledString();
        Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync(IList<string> versions, IList<string> categories);
        Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync(IList<string> versions, IList<string> categories, bool verifyFolderIsOnDisk);
    }
}
