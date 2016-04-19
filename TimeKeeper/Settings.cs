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
        public List<TimeEntry> TimeEntries { get; set; }

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
                foreach (TimeEntry entry in this.TimeEntries)
                    if (entry.Stop.Equals(TimeEntry.MIN_DATE))
                        return entry;

                return null;
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
    }

}
