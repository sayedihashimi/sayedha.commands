using System;
using McMaster.Extensions.CommandLineUtils;
using SayedHa.Commands.Shared;
using System.Linq;
using McMaster.Extensions.CommandLineUtils.Conventions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

namespace SayedHa.Commands {
    public class RegexTesterCommand : BaseCommandLineApplication {
        private readonly IReporter _reporter;

        public RegexTesterCommand(IReporter reporter) : base(
            "regex",
            "regextester",
            "Tests your regular expressions") {

            Debug.Assert(reporter != null);

            _reporter = reporter;
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

                foreach (var pattern in patterns) {
                    Regex rp = null;
                    try {
                        rp = new Regex(pattern, GetRegexOptions(), GetTimeout());
                    }
                    catch (ArgumentException) {
                        _reporter.Error($"Ignoring pattern '{pattern}' because it is not a valid regular expression");
                        continue;
                    }

                    _reporter.Output($"pattern: {rp.ToString()}");
                    foreach (var sample in samples) {
                        _reporter.Output($"{new string(' ', 4)}sample: {sample}");
                        
                        var matchResult = rp.Match(sample);
                        _reporter.Output($"{new string(' ', 8)}Is Match: {matchResult.Success}");
                        if (matchResult.Success) {
                            _reporter.Output($"{new string(' ', 8)}value: {matchResult.Value}");

                            if (matchResult.Groups != null && matchResult.Groups.Count > 0) {
                                foreach (Group group in matchResult.Groups) {
                                    var indent = new string(' ', 8);
                                    var msg = @$"{indent}Groups
{indent + indent}name:{group.Name}
{indent + indent}value:{group.Value}";
                                    _reporter.Output(msg);
                                }
                            }
                        }
                        _reporter.Output(string.Empty);
                    }
                    _reporter.Output(string.Empty);
                }
            });
        }

        private RegexOptions GetRegexOptions() {
            RegexOptions ro = RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline;

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
