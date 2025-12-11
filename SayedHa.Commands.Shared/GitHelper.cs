using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LibGit2Sharp;

namespace SayedHa.Commands.Shared {
    public class GitHelper {
        protected string _gitRemoteGithubPattern = @"^(https:\/\/|git@github.com:)(.*)\.git$";
        protected string _repoUrlPattern = @"([^\/]*)\/([^\/]*)$";

        public IList<(string name, string pushUrl)> GetRemotes(string repopath) {
            var result = new List<(string, string)>();
            if (string.IsNullOrWhiteSpace(repopath) || !Repository.IsValid(repopath)) {
                return result;
            }

            using var repo = new Repository(repopath);
            foreach (var remote in repo.Network.Remotes) {
                // Prefer push URL if available, else fetch URL
                string pushUrl = remote.PushUrl;
                if (string.IsNullOrWhiteSpace(pushUrl) && remote.Url != null) {
                    pushUrl = remote.Url;
                }
                if (!string.IsNullOrWhiteSpace(pushUrl)) {
                    result.Add((remote.Name, pushUrl));
                }
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

        public string GetGithubUrlForRepo(string repopath, string remoteName = "origin") {
            var remoteInfo = GetNameAndPushUrlForRemote(repopath, remoteName);
            if (!string.IsNullOrEmpty(remoteInfo.name) && !string.IsNullOrEmpty(remoteInfo.pushUrl)) {
                var regex = new Regex(_gitRemoteGithubPattern, RegexOptions.Compiled);
                var match = regex.Match(remoteInfo.pushUrl);

                if (match != null && match.Groups?.Count > 0) {
                    var repoPath = match.Groups[2]?.Value; // account/repo
                    if (!string.IsNullOrEmpty(repoPath)) {
                        if (!repoPath.StartsWith("github.com", StringComparison.OrdinalIgnoreCase)) {
                            repoPath = $"github.com/{repoPath}";
                        }
                        return $"https://{repoPath}";
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Return account and repo name given the repo's GitHub-like URL.
        /// Accepts full or partial URLs.
        /// </summary>
        public (string accountName, string repoName) GetAccountAndRepoNameFromUrl(string url) {
            string accountName = null;
            string repoName = null;

            if (!string.IsNullOrEmpty(url)) {
                var regex = new Regex(_repoUrlPattern, RegexOptions.Compiled);
                var match = regex.Match(url.TrimEnd('/'));

                if (match != null && match.Groups.Count == 3) {
                    accountName = match.Groups[1]?.Value;
                    repoName = match.Groups[2]?.Value;
                }
            }
            return (accountName, repoName);
        }

        public string GetLicenseString(string year, string copyrightName) {
            string licString = Strings.ApacheLicString;
            if (string.IsNullOrWhiteSpace(year)) {
                year = DateTime.Now.Year.ToString();
            }
            licString = licString.Replace("2017", year);

            if (!string.IsNullOrEmpty(copyrightName)) {
                licString = licString.Replace("Sayed Ibrahim Hashimi", copyrightName);
            }

            return licString;
        }
    }
}
