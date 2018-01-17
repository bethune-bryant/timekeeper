using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlobalKeyboardHook;

namespace TimeKeeper
{

    public static class CurrentSettings
    {
        public static Settings settings { get; set; }

        public static List<string> Projects
        {
            get
            {
                return settings.TimeEntries.Select(entry => entry.Project)
                                                   .Distinct()
                                                   .ToList();
            }
        }

        public static List<string> ProjectsFor(string Employer)
        {
            return settings.TimeEntries.Where(entry => entry.Employer == Employer)
                                               .Select(entry => entry.Project)
                                               .Distinct()
                                               .ToList();
        }

        public static List<string> Tasks
        {
            get
            {
                return settings.TimeEntries.Select(entry => entry.Task)
                                                   .Distinct()
                                                   .ToList();
            }
        }

        public static List<string> MonthEmployers(DateTime Month)
        {
            return settings.TimeEntries.Where(entry => entry.Start.Month == Month.Month && entry.Start.Year == Month.Year)
                                               .Select(entry => entry.Employer)
                                               .Distinct()
                                               .ToList();
        }

        public static List<string> Employers
        {
            get
            {
                return settings.TimeEntries.Select(entry => entry.Employer)
                                                   .Distinct()
                                                   .ToList();
            }
        }

        public static List<DateTime> RecordedMonths
        {
            get
            {
                return settings.TimeEntries.Select(item => new DateTime(item.Start.Year, item.Start.Month, 1)).Distinct().ToList();
            }
        }

        public static string MonthSummary(DateTime Month)
        {
            string retval = "";

            foreach (string employer in MonthEmployers(Month))
            {
                retval += employer + " " + Math.Max(Math.Round(MonthTime(Month, employer).TotalHours, 2), 0).ToString() + " | ";
            }

            retval = retval.Remove(retval.Length - 3);

            return retval;
        }

        public static TimeSpan TodayTime(string employer)
        {
            return Utilities.GetTime(settings.TimeEntries.Where(item => item.Employer == employer && item.Start.Day == DateTime.Now.Day && item.Start.Month == DateTime.Now.Month && item.Start.Year == DateTime.Now.Year));
        }

        public static TimeSpan MonthTime(DateTime Month, string employer)
        {
            return Utilities.GetTime(settings.TimeEntries.Where(item => item.Employer == employer && (new DateTime(item.Start.Year, item.Start.Month, 1)) == Month));
        }

        public static TimeSpan ThisMonthTime(string employer)
        {
            return MonthTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), employer);
        }

        public static TimeSpan ThisWeekTime(string employer)
        {
            return Utilities.GetTime(settings.TimeEntries.Where(item => item.Employer == employer && Utilities.GetWeek(item.Start) == Utilities.GetWeek(DateTime.Now) && item.Start.Year == DateTime.Now.Year));
        }

        public static TimeSpan LastWeekTime(string employer)
        {
            return Utilities.GetTime(settings.TimeEntries.Where(item => item.Employer == employer && Utilities.GetWeek(item.Start) == Utilities.GetWeek(DateTime.Now.AddDays(-7)) && item.Start.Year == DateTime.Now.AddDays(-7).Year));
        }
    }

    [Serializable]
    public class Settings
    {
        [System.Xml.Serialization.XmlIgnore]
        public static string FILE_LOCATION
        {
            get
            {
                string retval = Utilities.ReadFromRegistry("settingsfile").Trim();
                if (retval.Length <= 0)
                {
                    return "settings.tkf";
                }
                else
                {
                    return retval;
                }
            }
            set
            {
                Utilities.WriteToRegistry("settingsfile", value);
            }
        }

        public List<TimeEntry> TimeEntries { get; set; }

        public List<TimeEntry> CommonTasks { get; set; }

        public int StillWorkingTime { get; set; }

        public override string ToString()
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());

            StringBuilder retval = new StringBuilder("");

            System.IO.StringWriter s = new System.IO.StringWriter(retval);

            serializer.Serialize(s, this);

#if DEBUG
            return retval.ToString();
#else
            return Encryption.Encrypt(retval.ToString());
#endif
        }

        public Settings()
        {
            this.TimeEntries = new List<TimeEntry>();
            this.CommonTasks = new List<TimeEntry>();
            this.StillWorkingTime = 15;
        }

        public Settings(string FileName)
            : this()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());

                string test = System.IO.File.ReadAllText(FileName);

                if (!test.StartsWith("<?xml"))
                {
                    test = Encryption.Decrypt(test);
                }

                System.IO.StringReader read = new System.IO.StringReader(test);

                Settings setting = serializer.Deserialize(read) as Settings;

                this.TimeEntries = setting.TimeEntries;
                this.CommonTasks = setting.CommonTasks;
                this.StillWorkingTime = setting.StillWorkingTime;
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message);
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public TimeEntry LastUnclosedTask
        {
            get
            {

                if (this.TimeEntries.Where(entry => entry.Stop.Equals(TimeEntry.MIN_DATE)).Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.TimeEntries.Where(entry => entry.Stop.Equals(TimeEntry.MIN_DATE))
                                           .Aggregate((i, j) => i.Start > j.Start ? i : j);
                }

            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public TimeEntry LastClosedTask
        {
            get
            {


                if (this.TimeEntries.Where(entry => !entry.Stop.Equals(TimeEntry.MIN_DATE)).Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.TimeEntries.Where(entry => !entry.Stop.Equals(TimeEntry.MIN_DATE))
                                           .Aggregate((i, j) => i.Start > j.Start ? i : j);
                }

            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public List<TimeEntry> RecentTasks
        {
            get
            {
                return this.TimeEntries.Select(entry => new TimeEntry(entry.Project, entry.Task, entry.Employer, TimeEntry.MIN_DATE, TimeEntry.MIN_DATE, "")).Distinct().ToList();
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public TimeEntry NextCommonTask
        {
            get
            {
                return this.TimeEntries.Where(entry => this.CommonTasks.Where(common => common.Task == entry.Task).Count() == 0)
                                       .GroupBy(entry => entry.Task)
                                       .Aggregate((i, j) => i.Count() > j.Count() ? i : j)
                                       .Select(entry => new TimeEntry(entry.Project, entry.Task, entry.Employer, entry.Start, entry.Stop, entry.Comments))
                                       .First();
            }
        }
    }
}
