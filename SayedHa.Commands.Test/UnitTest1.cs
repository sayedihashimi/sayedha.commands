using System;
using SayedHa.Commands.Shared;
using Xunit;

namespace SayedHa.Commands.Test {
    public class TestPathHelper {
        [Fact]
        public void TestAreEqualWithEqalValuesOneHasTrailingSlash() {
            string path1 = "/one/two/three";
            string path2 = "/one/two/three/";

            Assert.True(new PathHelper().ArePathsEqual(path1, path2));
        }

        [Fact]
        public void TestAreEqualNotEqualPaths() {
            string path1 = "/one/two/three";
            string path2 = "/one/two/threessss";

            Assert.False(new PathHelper().ArePathsEqual(path1, path2));
        }

        [Fact]
        public void TestAreEqualNotEqualPaths2() {
            string path1 = "/one/two/three";
            string path2 = "/one/two/t";

            Assert.False(new PathHelper().ArePathsEqual(path1, path2));
        }
    }
}
