using SayedHa.Commands.Shared;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SayedHa.Commands {
    public class CalendarCommand : BaseCommandLineApplication {
        public CalendarCommand():base(
            "cal",
            "calendar",
            "shows a calendar") {

            this.OnExecute(() => {
                var date = DateTime.Now;
                
                

                var calendar = new Spectre.Console.Calendar(date.Year, date.Month);
                calendar.Culture(CultureInfo.CurrentCulture.Name);
                calendar.HighlightStyle(Style.Parse("yellow bold"));
                calendar.HeaderStyle(Style.Parse("blue bold"));

                calendar.AddCalendarEvent(date);

                // set to last month then print, current month then print, and next month then print
                var lastmonth = date.AddMonths(-1);
                calendar.Year = lastmonth.Year;
                calendar.Month = lastmonth.Month;
                calendar.Day = lastmonth.Day;
                AnsiConsole.Write(calendar);

                calendar.Year = date.Year;
                calendar.Month = date.Month;
                calendar.Day = date.Day;
                AnsiConsole.Write(calendar);

                var nextmonth = date.AddMonths(1);
                calendar.Year = nextmonth.Year;
                calendar.Month = nextmonth.Month;
                calendar.Day = nextmonth.Day;
                AnsiConsole.Write(calendar);



                return 0;
            });
        }
    }
}
