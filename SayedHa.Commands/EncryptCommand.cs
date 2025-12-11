using SayedHa.Commands.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace SayedHa.Commands {
    public class EncryptCommand : BaseCommandLineApplication {
        public EncryptCommand():base(
            "encrypt",
            "encryptvalue",
            "Will encrypt the provided value with DPAPI per user/machine."
            ) {
            // arguments
            var valueToEncrypt = this.Argument<string>(
                "valuetoencrypt",
                "The string value to encrypt");

            this.OnExecute(() => {
#if WINDOWS
                byte[] encBytes = ProtectedData.Protect(Encoding.Unicode.GetBytes(valueToEncrypt.Value), optionalEntropy: null, scope: DataProtectionScope.CurrentUser);
                string base64 = Convert.ToBase64String(encBytes);
                Console.WriteLine(base64);
#else
                Console.WriteLine("This command is only supported on Windows.");
#endif
            });
        }
    }
}
