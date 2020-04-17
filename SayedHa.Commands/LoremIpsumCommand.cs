using System;
using System.ComponentModel;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Xml;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;

namespace SayedHa.Commands {
    public class LoremIpsumCommand :BaseCommandLineApplication {
        private const string CmdName = "lorem";
        private const string CmdLongName = "lopremipsum";
        private const string Desc = "Generates new LoremIpsum strings.";
        private const string BaseUrl = "http://www.lipsum.com/feed/xml?start=yes&quot;";
        private const string RestulXpath = @"/feed/lipsum/text()";

        public LoremIpsumCommand() : base(CmdName, CmdLongName, Desc) {
            // options
            var argNumWords = this.Option<int>(
                "-w|--numwords",
                "Number of words to generate",
                CommandOptionType.SingleValue);

            var argNumParagraphs = this.Option<int>(
                "-p|--numparas",
                "Number of paragraphs to generate",
                CommandOptionType.SingleValue);

            var argNumBytes = this.Option<int>(
                "-b|--numbytes",
                "Number of bytes to generate",
                CommandOptionType.SingleValue);

            var argNumLists = this.Option<int>(
                "-l|--numlists",
                "Number of lists to generate",
                CommandOptionType.SingleValue);

            this.OnExecute(async () => {
                string what = string.Empty;
                int num = int.MinValue;
                string result = null;

                if (argNumWords.HasValue()) {
                    what = "words";
                    num = argNumWords.ParsedValue;
                }
                else if (argNumParagraphs.HasValue()) {
                    what = "paras";
                    num = argNumParagraphs.ParsedValue;
                }
                else if (argNumBytes.HasValue()) {
                    what = "bytes";
                    num = argNumBytes.ParsedValue;
                }
                else if (argNumLists.HasValue()) {
                    what = "lists";
                    num = argNumLists.ParsedValue;
                }
                else {
                    throw new MissingParameterException(
                        "One of the following parameters is required to have a value greater than 0: numwords,numparas,numbytes,numlists");
                }

                if(num <= 0) {
                    throw new MissingParameterException(
                        "One of the following parameters is required to have a value greater than 0: numwords,numparas,numbytes,numlists");
                }
                result = await GenerateAsync(what, num);
                Console.WriteLine(result);
            });
        }

        protected async System.Threading.Tasks.Task<string> GenerateAsync(string what, int numToGenerate) {
            string url = $"{BaseUrl}&what={what}&amount={numToGenerate}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();
            if(result != null) {
                result = result.Trim('\n').Trim('\r').Trim();
                var realResult = GetResultFromXml(result);
                return realResult;
            }
            else {
                throw new ApplicationException("Unknown error, no response");
            }
        }

        protected string GetResultFromXml(string xmlresult) {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlresult);
            var res = doc.SelectSingleNode(RestulXpath).Value;
            return res;
        }
    }
}
