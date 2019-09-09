using System;
using SayedHa.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace SayedHa.Commands {
    public class ConvertClipboardToPlainText : BaseCommandLineApplication {
        public ConvertClipboardToPlainText():base(
            "convertclip",
            "ConvertClipboardToPlainText",
            "Converts clipboard contents to plain text and readds to clipboard."){

            this.OnExecute(() => {
                try {
                    var text = TextCopy.Clipboard.GetText();
                    TextCopy.Clipboard.SetText(text);
                    Console.WriteLine("Clipboard converted to plain text");
                }
                catch(Exception ex) {
                    Console.WriteLine($"Unable to convert clipboard to plain text. Error:\n{ex.ToString()}");
                }
            });
        }
    }
}
