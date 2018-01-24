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

    [Serializable]
    public class Settings
    {
        [System.Xml.Serialization.XmlIgnore]
        public static Settings CurrentSettings { get; set; }

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

        public string JiraURL { get; set; }

        public string JiraUsername { get; set; }

        public string EncryptedJiraPassword { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public string JiraPassword
        {
            get
            {
                try { return Encryption.Decrypt(this.EncryptedJiraPassword); }
                catch { return ""; }
            }
            set
            {
                this.EncryptedJiraPassword = Encryption.Encrypt(value);
            }
        }

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
            this.JiraURL = "";
            this.JiraUsername = "";
            this.JiraPassword = "";
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
                this.JiraURL = setting.JiraURL;
                this.JiraPassword = setting.JiraPassword;
                this.JiraUsername = setting.JiraUsername;
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

        [System.Xml.Serialization.XmlIgnore]
        public List<string> Projects
        {
            get
            {
                return TimeEntries.Select(entry => entry.Project)
                                                   .Distinct()
                                                   .ToList();
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public List<string> Tasks
        {
            get
            {
                return TimeEntries.Select(entry => entry.Task)
                                           .Distinct()
                                           .ToList();
            }
        }

        public List<string> MonthEmployers(DateTime Month)
        {
            return TimeEntries.Where(entry => entry.Start.Month == Month.Month && entry.Start.Year == Month.Year)
                                               .Select(entry => entry.Employer)
                                               .Distinct()
                                               .ToList();
        }

        [System.Xml.Serialization.XmlIgnore]
        public List<string> Employers
        {
            get
            {
                return TimeEntries.Select(entry => entry.Employer)
                                                   .Distinct()
                                                   .ToList();
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public List<DateTime> RecordedMonths
        {
            get
            {
                return TimeEntries.Select(item => new DateTime(item.Start.Year, item.Start.Month, 1)).Distinct().ToList();
            }
        }

        public string MonthSummary(DateTime Month)
        {
            string retval = "";

            foreach (string employer in MonthEmployers(Month))
            {
                retval += employer + " " + Math.Max(Math.Round(MonthTime(Month, employer).TotalHours, 2), 0).ToString() + " | ";
            }

            retval = retval.Remove(retval.Length - 3);

            return retval;
        }

        public TimeSpan TodayTime(string employer)
        {
            return Utilities.GetTime(TimeEntries.Where(item => item.Employer == employer && item.Start.Day == DateTime.Now.Day && item.Start.Month == DateTime.Now.Month && item.Start.Year == DateTime.Now.Year));
        }

        public TimeSpan MonthTime(DateTime Month, string employer)
        {
            return Utilities.GetTime(TimeEntries.Where(item => item.Employer == employer && (new DateTime(item.Start.Year, item.Start.Month, 1)) == Month));
        }

        public TimeSpan ThisMonthTime(string employer)
        {
            return MonthTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), employer);
        }

        public TimeSpan ThisWeekTime(string employer)
        {
            return Utilities.GetTime(TimeEntries.Where(item => item.Employer == employer && Utilities.GetWeek(item.Start) == Utilities.GetWeek(DateTime.Now) && item.Start.Year == DateTime.Now.Year));
        }

        public TimeSpan LastWeekTime(string employer)
        {
            return Utilities.GetTime(TimeEntries.Where(item => item.Employer == employer && Utilities.GetWeek(item.Start) == Utilities.GetWeek(DateTime.Now.AddDays(-7)) && item.Start.Year == DateTime.Now.AddDays(-7).Year));
        }
    }
}
