using System;
namespace SayedHa.Commands.Shared {
    public static class Strings {
        public const string GetSdksInstalledRegex = @"(.*)\s\[([^\s]*)\]";
        public const string ListSdksCommandArgument = "--list-sdks";

        public const string GetRuntimesInstalledRegex = @"^([^\s]+)\s([^\s]+)\s\[([^\s]+)]";
        public const string ListRuntimesCommandArgument = "--list-runtimes";
    }
}
