﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace SayedHa.Commands.Shared {
    public interface INetCoreHelper {
        Task<List<SdkInfo>> GetSdksInstalledAsync();
        Task<string> GetSdksInstalledString();
    }
}