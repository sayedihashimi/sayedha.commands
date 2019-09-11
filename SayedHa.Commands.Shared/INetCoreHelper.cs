using System.Collections.Generic;
using System.Threading.Tasks;

namespace SayedHa.Commands.Shared {
    public interface INetCoreHelper {
        Task<List<SdkInfo>> GetSdksInstalledAsync();
        Task<string> GetSdksInstalledString();

        Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync();
        Task<string> GetRuntimesInstalledString();
        Task<List<IRuntimeInfo>> GetRuntimesInstalledAsync(IList<string> versions, IList<string> categories);
    }
}
