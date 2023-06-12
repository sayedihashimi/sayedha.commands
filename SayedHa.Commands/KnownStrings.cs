using System;
namespace SayedHa.Commands {
    public static class KnownStrings {
        public const string DefaultRemoteName = "origin";
        public const string GitFolderName = ".git";
        public const string GitIgnoreUrl = @"https://raw.githubusercontent.com/github/gitignore/master/VisualStudio.gitignore";
        public const string GitAttributesUrl = @"https://raw.githubusercontent.com/sayedihashimi/sayed-tools/master/.gitattributes";
        public const string GitIgnoreFilename = ".gitignore";
        public const string GitAttributesFilename = ".gitattributes";
        public const string LicenseFilename = "LICENSE";
        public const string ApacheLicenseUrl = @"http://www.apache.org/licenses/LICENSE-2.0.txt";
        public const string MitLicenseUrl = @"http://www.apache.org/licenses/LICENSE-2.0.txt";

        public static string GetLicenseText()
        {
            return $@"
Copyright (c) {DateTime.Now.Year} Sayed Ibrahim Hashimi

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
";
        }
    }
}
