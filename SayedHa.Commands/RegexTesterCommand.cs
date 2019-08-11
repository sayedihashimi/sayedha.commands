using System;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;
using System.Linq;
using McMaster.Extensions.CommandLineUtils.Conventions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace SayedHa.Commands {
    public class RegexTesterCommand : BaseCommandLineApplication {
        public RegexTesterCommand() : base(
            "regex",
            "regextester",
            "Tests your regular expressions") {

            // options
            var optionPatterns = this.Option<string>(
                "-p|--pattern",
                "Regular expression pattern that should be used. This parameter may be passed multiple times.",
                CommandOptionType.MultipleValue);
            optionPatterns.IsRequired(allowEmptyStrings: false, errorMessage: "Pattern is required");

            var optionSamples = this.Option<string>(
                "-s|--sample",
                "Sample text that will be used to execute the regular expression patterns",
                CommandOptionType.MultipleValue);
            optionSamples.IsRequired(allowEmptyStrings: false, errorMessage: "Sample text is required");

            this.OnExecute(() => {
                // get the values for options
                var patterns = optionPatterns.ParsedValues.ToList<string>();
                var samples = optionSamples.ParsedValues.ToList<string>();

                List<Regex> regexList = new List<Regex>();
                foreach (var pattern in patterns) {
                    var r = new Regex(pattern, GetRegexOptions(), GetTimeout());
                    regexList.Add(r);
                }

                // TODO: Probably can get away with getting rid of regexList
                foreach (var rp in regexList) {
                    Console.WriteLine($"pattern: {rp.ToString()}");
                    foreach (var sample in samples) {
                        Console.WriteLine($"{new string(' ', 4)}sample: {sample}");

                        var matchResult = rp.Match(sample);
                        Console.WriteLine($"{new string(' ', 8)}Is Match: {matchResult.Success}");
                        if (matchResult.Success) {
                            Console.WriteLine($"{new string(' ', 8)}value: {matchResult.Value}");
                        }
                        if (matchResult.Groups != null && matchResult.Groups.Count > 0) {
                            foreach (Group group in matchResult.Groups) {
                                var indent = new string(' ', 8);
                                var msg = @$"{indent}Groups
{indent + indent}name:{group.Name}
{indent + indent}value:{group.Value}";
                                Console.WriteLine(msg);
                            }
                        }
                        // matchResult.Success
                        // matchResult.Value
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    // var matchResult = rp.Match()
                }
            });
        }

        private RegexOptions GetRegexOptions() {
            RegexOptions ro = RegexOptions.Compiled; // || RegexOptions.CultureInvariant || RegexOptions.IgnoreCase || RegexOptions.Multiline;

            return ro;
        }
        private TimeSpan GetTimeout() {
            return new TimeSpan(0, 1, 0);
        }

        private string GetOutputStringFor(Match match) {
            var r = @$"IsMatch: {match.Success}
";
            return r.ToString();
        }
    }
}
