using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeKeeper
{
    static class Reports
    {
        public static void ReportDailyTask(TimeEntry inputEntry, SaveFileDialog saveFileDialogReport)
        {
            frmChooseDay chooser = new frmChooseDay();
            chooser.Title = "Select Days to Include in the Report";
            chooser.SelectionCount = 100;

            if (chooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StringBuilder result = new StringBuilder();

                for (DateTime day = chooser.SelectedDates.Start; day <= chooser.SelectedDates.End; day = day.AddDays(1))
                {
                    TimeSpan total = new TimeSpan(0);

                    foreach (TimeEntry entry in CurrentSettings.settings.TimeEntries.Where(e => e.Start.Date == day.Date && e.Project == inputEntry.Project && e.Task == inputEntry.Task && e.Employer == inputEntry.Employer))
                    {
                        total += (entry.Stop - entry.Start);
                    }
                    if (total.TotalHours > 0)
                    {
                        result.AppendLine(day.Date.ToString("d-MMM") + "," + Math.Round(total.TotalHours, 2).ToString());
                    }
                }

                if (saveFileDialogReport.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialogReport.FileName, result.ToString());
                        System.Diagnostics.Process.Start(saveFileDialogReport.FileName);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private static string Dub(string col)
        {
            return "INDIRECT(\"\"" + col + ":" + col + "\"\")";
        }

        private static string Dub0(string col)
        {
            return "INDIRECT(\"\"" + col + "\"\"&ROW())";
        }

        private static string Dub1(string col)
        {
            return "INDIRECT(\"\"" + col + "\"\"&ROW()-1)";
        }

        private static string Interval = "IF(ISBLANK(" + Dub0("F") + "),NOW()-" + Dub0("D") + "," + Dub0("F") + ")-" + Dub0("E") + "";
        private static string Time = "IF(ISBLANK(" + Dub0("A") + "),0,ROUND(24*(" + Interval + "+IF(" + Interval + "<0,1,0)),2))";
        private static string HMM = "IF(" + Time + "<=0,\"\"\"\",INT(" + Time + ")&\"\"+\"\"&TEXT(ROUND((" + Time + "-INT(" + Time + "))*60,0),\"\"00\"\"))";
        private static string TransitionTime = "ROUND(24*(" + Dub0("E") + "-" + Dub1("F") + "+IF(" + Dub0("E") + "-" + Dub1("F") + "<0,1,0)),2)";
        private static string TTime_HMM = "INT(" + TransitionTime + ")&\"\"+\"\"&TEXT(ROUND((" + TransitionTime + "-INT(" + TransitionTime + "))*60,0),\"\"00\"\")";
        private static string Gap = "IF(" + Dub0("D") + "=" + Dub1("D") + ",IF(" + Dub0("E") + ">" + Dub1("F") + "," + TTime_HMM + ",IF(" + Dub0("E") + "<" + Dub1("F") + ",\"\"OVERBILL\"\",\"\"\"\")),\"\"\"\")";
        private static string Exclude = "$N$1";
        private static string DailyTime = "IF(" + Dub0("D") + "=" + Dub1("D") + ",\"\"\"\",SUMIFS($H:$H,$D:$D,\"\"=\"\"&" + Dub0("D") + ",$A:$A,\"\"<>Personal\"\",$A:$A,\"\"<>\"\"&" + Exclude + "))";

        private static string DayStart = "MAX(" + Dub("A") + ")";
        private static string YesterdayStart = DayStart + " - 1";
        private static string WeekStartsOn = "$O$4";
        private static string WeekStart = "MAX(" + Dub("D") + ")-MOD(WEEKDAY(MAX(" + Dub("D") + "))-MATCH(" + WeekStartsOn + ",{\"\"Sun\"\",\"\"Mon\"\",\"\"Tue\"\",\"\"Wed\"\",\"\"Thu\"\",\"\"Fri\"\",\"\"Sat\"\"},0),7)";
        private static string MonthStart = "DATE(YEAR(MAX(" + Dub("D") + ")),MONTH(MAX(" + Dub("D") + ")),1)";
        private static string Label2 = "LEFT(" + Dub0("M") + ",IF(ISERROR(FIND(\"\":\"\"," + Dub0("M") + ")),LEN(" + Dub0("M") + "),FIND(\"\":\"\"," + Dub0("M") + ")-1))";
        private static string DailyHours = "SUMIFS(" + Dub("H") + "," + Dub("D") + ",\"\">=\"\"&" + DayStart + "," + Dub("A") + ",IF(" + Label2 + "=\"\"Total\"\",\"\">_\"\"," + Label2 + "))";
        private static string YesterdaysHours = "SUMIFS(" + Dub("H") + "," + Dub("D") + ",\"\">=\"\"&" + YesterdayStart + "," + Dub("D") + ",\"\"<\"\"&" + DayStart + "," + Dub("A") + ",IF(" + Label2 + "=\"\"Total\"\",\"\">_\"\"," + Label2 + "))";
        private static string WeeklyHours = "SUMIFS(" + Dub("H") + "," + Dub("D") + ",\"\">=\"\"&" + WeekStart + "," + Dub("A") + ",IF(" + Label2 + "=\"\"Total\"\",\"\">_\"\"," + Label2 + "))";
        private static string MonthlyHours = "SUMIFS(" + Dub("H") + "," + Dub("D") + ",\"\">=\"\"&" + MonthStart + "," + Dub("A") + ",IF(" + Label2 + "=\"\"Total\"\",\"\">_\"\"," + Label2 + "))";

        public static void Export(string employer, SaveFileDialog saveFileDialogReport)
        {
            Export(employer, DateTime.MinValue, saveFileDialogReport);
        }

        public static void Export(string employer, DateTime since, SaveFileDialog saveFileDialogReport)
        {
            Export(employer, since, DateTime.Now, saveFileDialogReport);
        }

        public static void Export(string employer, DateTime since, DateTime until, SaveFileDialog saveFileDialogReport)
        {
            List<string> timeEntries = new List<string>();

            timeEntries.Add("Project,Task,Venue,Date,Start,Stop,\"=\"\"Comments  (Month to date: \"\"&ROUND(SUM($K:$K),2)&\"\")\"\"\",Time,H+MM,Gap,DailyTime,=P5,Also Exclude:");

            CurrentSettings.settings.TimeEntries.Reverse();

            foreach (TimeEntry entry in CurrentSettings.settings.TimeEntries.Where(item => item.Employer == employer && item.Start >= since && item.Stop <= until && item.Stop != TimeEntry.MIN_DATE))
            {
                string timeEnrty;

                if (entry.Stop != TimeEntry.MIN_DATE)
                {
                    timeEnrty = Quote(entry.Project) + "," + Quote(entry.Task) + ",," + Quote(entry.Start.Date.ToString("d-MMM")) + "," +
                                  Quote(entry.Start.ToString("hh:mm tt")) + "," + Quote(entry.Stop.ToString("hh:mm tt")) + "," + Quote(entry.Comments);
                }
                else
                {
                    timeEnrty = Quote(entry.Project) + "," + Quote(entry.Task) + ",," + Quote(entry.Start.Date.ToString("d-MMM")) + "," +
                                  Quote(entry.Start.ToString("hh:mm tt")) + ",," + Quote(entry.Comments);
                }

                string time = "=" + Time;
                string hmm = "=" + HMM;
                string gap = "=" + Gap;
                string dailyTime = "=" + DailyTime;

                timeEntries.Add(timeEnrty + "," + Quote(time) + "," + Quote(hmm) + "," + Quote(gap) + "," + Quote(dailyTime));
            }

            CurrentSettings.settings.TimeEntries.Reverse();

            string dailyHours = "=" + DailyHours;
            string yesterdaysHours = "=" + YesterdaysHours;
            string weeksHours = "=" + WeeklyHours;
            string monthsHours = "=" + MonthlyHours;

            string tableRow = "," + Quote(dailyHours) + "," + Quote(yesterdaysHours) + "," + Quote(weeksHours) + "," + Quote(monthsHours) + "";

            List<string> totalsChart = new List<string>();
            totalsChart.Add(",Week Starts on:,Sat");
            totalsChart.Add("Total" + tableRow);
            totalsChart.Add(",Today,Yesterday,Weekly,Monthly");

            foreach (string project in CurrentSettings.ProjectsFor(employer))
            {
                totalsChart.Add(project + tableRow);
            }

            for (int i = 0; i < 4 - timeEntries.Count; i++)
            {
                timeEntries.Add("");
            }

            for (int i = 0; i < totalsChart.Count; i++)
            {
                if (timeEntries.Count > (i + 3))
                {
                    timeEntries[i + 3] += ",," + totalsChart[i];
                }
                else
                {
                    timeEntries.Add(",,,,,,,,,,,," + totalsChart[i]);
                }
            }

            StringBuilder result = new StringBuilder();

            foreach (string line in timeEntries)
            {
                result.AppendLine(line);
            }

            if (saveFileDialogReport.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialogReport.FileName, result.ToString());
                    System.Diagnostics.Process.Start(saveFileDialogReport.FileName);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static string Quote(string input)
        {
            return "\"" + input + "\"";
        }
    }
}
