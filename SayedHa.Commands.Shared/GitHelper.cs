using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace SayedHa.Commands.Shared {
    public class GitHelper {
        public IList<(string name, string pushUrl)> GetRemotes(string repopath) {
            using var repo = new Repository(repopath);

            var result = new List<(string, string)>();
            foreach(var remote in repo.Network.Remotes) {
                result.Add((remote.Name, remote.PushUrl));
            }

            return result;
        }

        public (string name, string pushUrl) GetNameAndPushUrlForRemote(string repopath, string remoteName) {
            var remotes = GetRemotes(repopath);

            if (remotes != null) {
                var found = (from r in remotes
                             where string.Compare(remoteName, r.name, StringComparison.CurrentCultureIgnoreCase) == 0
                             select new {
                                 name = r.name,
                                 pushUrl = r.pushUrl
                             }).FirstOrDefault();

                return found != null
                    ? (found.name, found.pushUrl)
                    : (null, null);
            }
            else {
                return (null, null);
            }
        }
    }
}
