using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper
{
    [Serializable]
    public class TimeEntry : IComparable
    {
        public static DateTime MIN_DATE = new DateTime(1753, 1, 1, 0, 0, 0);

        public string Project { get; set; }
        public string Task { get; set; }
        public string Employer { get; set; }

        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public string Comments { get; set; }

        //Serialize the comments in binary to preserve \r\n's.
        [System.Xml.Serialization.XmlElement(ElementName = "Comments", DataType = "base64Binary")]
        public byte[] BinaryComments
        {
            get
            {
                return Encoding.Unicode.GetBytes(Comments);
            }
            set
            {
                Comments = Encoding.Unicode.GetString(value);
            }
        }

        public JiraInfo WorkLog { get; set; }

        public TimeEntry(TimeEntry toCopy)
            : this(toCopy.Project, toCopy.Task, toCopy.Employer, toCopy.Start, toCopy.Stop, toCopy.Comments, toCopy.WorkLog)
        {
        }

        public TimeEntry()
            : this("", "", "")
        {
        }

        public TimeEntry(DateTime Start)
            : this("", "", "", Start)
        {
        }

        public TimeEntry(string Project, string Task, string Employer)
            : this(Project, Task, Employer, DateTime.Now)
        {
        }

        public TimeEntry(string Project, string Task, string Employer, DateTime Start)
            : this(Project, Task, Employer, Start, MIN_DATE, "", new JiraInfo())
        {
        }

        public TimeEntry(string Project, string Task, string Employer, DateTime Start, DateTime Stop, string Comments)
            : this(Project, Task, Employer, Start, Stop, Comments, new JiraInfo())
        {
        }

        public TimeEntry(string Project, string Task, string Employer, DateTime Start, DateTime Stop, string Comments, string JiraID)
            : this(Project, Task, Employer, Start, MIN_DATE, Comments, JiraInterface.GetJiraInfo(JiraID))
        {
        }

        public TimeEntry(string Project, string Task, string Employer, DateTime Start, DateTime Stop, string Comments, JiraInfo worklog)
        {
            this.Project = Project;
            this.Task = Task;
            this.Employer = Employer;
            this.Start = Start;
            this.Stop = Stop;
            this.Comments = Comments;
            this.WorkLog = worklog;
        }

        public void UpdateJira()
        {
            if (this.WorkLog.TaskID.Length > 0 && this.Stop != MIN_DATE)
            {
                this.WorkLog = JiraInterface.UpdateWork(this.WorkLog, this.Start, this.Stop);
                this.Comments = this.WorkLog.Summary;
            }
            else if (this.WorkLog.TaskID.Length > 0)
            {
                this.WorkLog = JiraInterface.UpdateJiraInfo(this.WorkLog);
            }

            if (this.WorkLog.Summary.Trim().Length > 0)
            {
                this.Comments = this.WorkLog.Summary;
            }
        }

        public void DeleteJira()
        {
            if (this.WorkLog.WorkLogID.Length > 0)
            {
                try
                {
                    JiraInterface.DeleteWorkLog(this.WorkLog.TaskID, this.WorkLog.WorkLogID);
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("There was an error when deleteing the work log. Continuing anyway.", "Error Deleting Work Log!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
        
        public object AsRow
        {
            get
            {
                return new { this.Project, this.Task, this.Employer, this.Start.DayOfWeek, TaskTime = Utilities.ElapsedTimeString(this.Start, this.Stop), this.Start, this.Stop, this.Comments, JiraID = this.WorkLog };
            }
        }

        #region Overrides

        public override bool Equals(object obj)
        {
            TimeEntry input = obj as TimeEntry;

            if (object.ReferenceEquals(input, null))
            {
                return false;
            }
            else
            {
                return this.Project == input.Project && this.Task == input.Task && this.Employer == input.Employer && this.Start.Equals(input.Start) && this.Stop.Equals(input.Stop) && this.Comments == input.Comments;
            }
        }

        public override int GetHashCode()
        {
            return (this.Project + this.Task + this.Employer + this.Start.ToString() + this.Stop.ToString() + this.Comments).GetHashCode();
        }

        public override string ToString()
        {
            return this.Task + " task on " + this.Project + " for " + this.Employer;
        }

        #endregion

        #region Interfaces

        int IComparable.CompareTo(object obj)
        {
            TimeEntry input = obj as TimeEntry;

            if (obj == null)
            {
                throw new ArgumentException();
            }
            else
            {
                int retval = this.Start.CompareTo(input.Start);
                if (retval != 0)
                {
                    return retval;
                }
                else
                {
                    return this.Stop.CompareTo(input.Stop);
                }
            }
        }

        #endregion

        public static List<string> ProjectsFor(string Employer, List<TimeEntry> TimeEntries)
        {
            return TimeEntries.Where(entry => entry.Employer == Employer)
                              .Select(entry => entry.Project)
                              .Distinct()
                              .ToList();
        }
    }
}
