using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using LibGit2Sharp;

namespace SayedHa.Commands.Shared {
    public class GitHelper {
        protected string _gitRemoteGithubPattern = @"git@github\.com\:([^\/]*)\/(.*)\.git";

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

        public string GetGithubUrlForRepo(string repopath, string remoteName="origin") {
            Debug.Assert(!string.IsNullOrEmpty(repopath));
            Debug.Assert(!string.IsNullOrEmpty(remoteName));

            var remoteInfo = GetNameAndPushUrlForRemote(repopath, remoteName);
            if (!string.IsNullOrEmpty(remoteInfo.name) &&
                !string.IsNullOrEmpty(remoteInfo.pushUrl)) {
                var regex = new Regex(_gitRemoteGithubPattern, RegexOptions.Compiled);
                var match = regex.Match(remoteInfo.pushUrl);

                if (match != null && match.Groups?.Count > 0) {
                    var accountName = match?.Groups?[1]?.Value;
                    var repoName = match?.Groups[2]?.Value;
                    return $"https://github.com/{accountName}/{repoName}";
                }
            }

            return null;
        }
    }
}
