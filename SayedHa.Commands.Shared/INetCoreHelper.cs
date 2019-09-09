using System.Collections.Generic;
using System.Threading.Tasks;

namespace SayedHa.Commands.Shared {
    public interface INetCoreHelper {
        Task<List<SdkInfo>> GetSdksInstalled();
        Task<string> GetSdksInstalledString();
    }
}