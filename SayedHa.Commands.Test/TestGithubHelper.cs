using System;
using LibGit2Sharp;
using SayedHa.Commands.Shared;
using Xunit;

namespace SayedHa.Commands.Test {
    public class TestGithubHelper {
        public class TestGetAccountAndRepo {
            [Fact]
            public void PassFullUrl01() {
                string url = @"https://github.com/sayedihashimi/scratch";

                string expectedAcctName = "sayedihashimi";
                string expectedRepoName = "scratch";

                var result = new GitHelper().GetAccountAndRepoNameFromUrl(url);

                Assert.Equal(expectedAcctName, result.accountName);
                Assert.Equal(expectedRepoName, result.repoName);
            }

            [Fact]
            public void PassFullUrl02() {
                string url = @"https://github.com/ligershark/sidewafflev2/";

                string expectedAcctName = "ligershark";
                string expectedRepoName = "sidewafflev2";

                var result = new GitHelper().GetAccountAndRepoNameFromUrl(url);

                Assert.Equal(expectedAcctName, result.accountName);
                Assert.Equal(expectedRepoName, result.repoName);
            }

            [Fact]
            public void PassPartialUrl01() {
                string url = @"ligershark/sidewafflev2/";

                string expectedAcctName = "ligershark";
                string expectedRepoName = "sidewafflev2";

                var result = new GitHelper().GetAccountAndRepoNameFromUrl(url);

                Assert.Equal(expectedAcctName, result.accountName);
                Assert.Equal(expectedRepoName, result.repoName);
            }

            [Fact]
            public void PassPartialUrl02() {
                string url = @"sayedihashimi/scratch";

                string expectedAcctName = "sayedihashimi";
                string expectedRepoName = "scratch";

                var result = new GitHelper().GetAccountAndRepoNameFromUrl(url);

                Assert.Equal(expectedAcctName, result.accountName);
                Assert.Equal(expectedRepoName, result.repoName);
            }
        }

        //[Fact]
        //public void TestPassingFullUrlSSH() {
        //    string url = @"https://github.com/sayedihashimi/scratch";
        //    string expectedGitUrl = @"git@github.com:sayedihashimi/scratch.git";
        //}
    }
}
