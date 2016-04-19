using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlobalKeyboardHook;

namespace TimeKeeper
{
    public partial class frmMain : Form
    {

        const string FILE_LOCATION = "settings.tkf";

        public static Settings settings = new Settings(FILE_LOCATION);

        static globalKeyboardHook KeyboardHook = new globalKeyboardHook();

        public frmMain()
        {
            InitializeComponent();

            KeyboardHook.HookedKeys.Add(Keys.C);
            KeyboardHook.HookedKeys.Add(Keys.A);
            KeyboardHook.HookedKeys.Add(Keys.S);
            KeyboardHook.KeyUp += KeyboardHook_KeyUp;
        }

        static Keys pressed;

        void KeyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == (Keys.Control | Keys.Shift | Keys.Alt))
            {
                e.Handled = true;
                pressed = e.KeyCode;
                timerKeyHooks.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshEntries();
            timerSave.Enabled = true;
            timerWorking.Interval = settings.StillWorkingTime * 60 * 1000;
            timerWorking.Enabled = true;
        }

        public static List<string> Projects
        {
            get
            {
                return frmMain.settings.TimeEntries.Select(entry => entry.Project).Distinct().ToList();
            }
        }

        public static List<string> ProjectsFor(string Employer)
        {
            return frmMain.settings.TimeEntries.Where(entry => entry.Employer == Employer).Select(entry => entry.Project).Distinct().ToList();
        }

        public static List<string> Tasks
        {
            get
            {
                return frmMain.settings.TimeEntries.Select(entry => entry.Task).Distinct().ToList();
            }
        }

        public static List<string> MonthEmployers(DateTime Month)
        {
            return frmMain.settings.TimeEntries.Where(entry => entry.Start.Month == Month.Month && entry.Start.Year == Month.Year).Select(entry => entry.Employer).Distinct().ToList();
        }

        public static List<string> Employers
        {
            get
            {
                return frmMain.settings.TimeEntries.Select(entry => entry.Employer).Distinct().ToList();
            }
        }

        private static TimeSpan TodayTime(string employer)
        {
            TimeSpan retval = new TimeSpan(0);

            foreach (TimeEntry entry in settings.TimeEntries.Where(item => item.Employer == employer && item.Start.Day == DateTime.Now.Day && item.Start.Month == DateTime.Now.Month && item.Start.Year == DateTime.Now.Year))
            {
                if (entry.Stop == TimeEntry.MIN_DATE)
                {
                    retval += DateTime.Now - entry.Start;
                }
                else
                {
                    retval += entry.Stop - entry.Start;
                }
            }

            return retval;
        }

        private static TimeSpan MonthTime(DateTime Month, string employer)
        {
            TimeSpan retval = new TimeSpan(0);

            foreach (TimeEntry entry in settings.TimeEntries.Where(item => item.Employer == employer && (new DateTime(item.Start.Year, item.Start.Month, 1)) == Month))
            {
                if (entry.Stop == TimeEntry.MIN_DATE)
                {
                    retval += DateTime.Now - entry.Start;
                }
                else
                {
                    retval += entry.Stop - entry.Start;
                }
            }

            return retval;
        }

        private static TimeSpan ThisMonthTime(string employer)
        {
            return MonthTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), employer);
        }

        private static TimeSpan ThisWeekTime(string employer)
        {
            TimeSpan retval = new TimeSpan(0);

            foreach (TimeEntry entry in settings.TimeEntries.Where(item => item.Employer == employer && GetWeek(item.Start) == GetWeek(DateTime.Now) && item.Start.Year == DateTime.Now.Year))
            {
                if (entry.Stop == TimeEntry.MIN_DATE)
                {
                    retval += DateTime.Now - entry.Start;
                }
                else
                {
                    retval += entry.Stop - entry.Start;
                }
            }

            return retval;
        }

        private static TimeSpan LastWeekTime(string employer)
        {
            TimeSpan retval = new TimeSpan(0);

            foreach (TimeEntry entry in settings.TimeEntries.Where(item => item.Employer == employer && GetWeek(item.Start) == GetWeek(DateTime.Now.AddDays(-7)) && item.Start.Year == DateTime.Now.AddDays(-7).Year))
            {
                if (entry.Stop == TimeEntry.MIN_DATE)
                {
                    retval += DateTime.Now - entry.Start;
                }
                else
                {
                    retval += entry.Stop - entry.Start;
                }
            }

            return retval;
        }

        private List<DateTime> RecordedMonths
        {
            get
            {
                return settings.TimeEntries.Select(item => new DateTime(item.Start.Year, item.Start.Month, 1)).Distinct().ToList();
            }
        }

        private string MonthSummary(DateTime Month)
        {
            string retval = "";

            foreach (string employer in MonthEmployers(Month))
            {
                retval += employer + " " + Math.Max(Math.Round(MonthTime(Month, employer).TotalHours, 2), 0).ToString() + " | ";
            }

            retval = retval.Remove(retval.Length - 3);

            return retval;
        }

        private string ElapsedTimeString(DateTime start, DateTime stop)
        {
            return new TimeSpan(0, 0, (int)Math.Round((stop - start).Duration().TotalMinutes), 0).ToString("hh\\:mm");
        }

        private object TimeEntryToRow(TimeEntry entry)
        {
            return new { entry.Project, entry.Task, entry.Employer, entry.Start.DayOfWeek, TaskTime = ElapsedTimeString(entry.Start, entry.Stop), entry.Start, entry.Stop, entry.Comments, JiraID = entry.WorkLog };
        }

        private void RefreshPreviousEntries()
        {
            tabPrevious.TabPages.Clear();

            foreach (DateTime recordedMonth in RecordedMonths)
            {
                tabPrevious.TabPages.Add(recordedMonth.ToString("MMM-yy") + ": " + MonthSummary(recordedMonth));
                DataGridView newGV = new DataGridView();
                tabPrevious.TabPages[tabPrevious.TabPages.Count - 1].Controls.Add(newGV);
                newGV.Dock = DockStyle.Fill;
                newGV.MultiSelect = false;
                newGV.ReadOnly = true;
                newGV.AllowUserToAddRows = false;
                newGV.AllowUserToDeleteRows = false;
                newGV.AllowUserToOrderColumns = true;
                newGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                newGV.CellDoubleClick += dataPrevious_CellDoubleClick;
                newGV.KeyUp += dataPrevious_KeyUp;
                newGV.DataSource = new SortableBinding.SortableBindingList<Object>(settings.TimeEntries.Where(entry => (new DateTime(entry.Start.Year, entry.Start.Month, 1)) == recordedMonth)
                                                                                                       .Select(TimeEntryToRow)
                                                                                                       .ToList());
            }
        }

        private void RefreshEntries()
        {
            int temp = dataThisWeek.FirstDisplayedScrollingRowIndex;
            int selected;
            try
            {
                selected = dataThisWeek.SelectedRows[0].Index;
            }
            catch
            {
                selected = 0;
            }

            settings.TimeEntries.Sort();
            settings.TimeEntries.Reverse();
            //dataMembers.DataSource = settings.Members.Select(member => new { member.RNumber, member.FirstName, member.LastName, member.Major, member.PaidForCurrentSemester, member.PaidForNextSemester }).ToList();

            recentTasksToolStripMenuItem.DropDownItems.Clear();
            recentTasksToolStripMenuItem1.DropDownItems.Clear();
            dailyTaskReportToolStripMenuItem.DropDownItems.Clear();

            foreach (TimeEntry recent in settings.RecentTasks)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem(recent.ToString());
                menu.Click += (s, e) => StartNewTask(recent);
                recentTasksToolStripMenuItem.DropDownItems.Add(menu);

                menu = new ToolStripMenuItem(recent.ToString());
                menu.Click += (s, e) => StartNewTask(recent);
                recentTasksToolStripMenuItem1.DropDownItems.Add(menu);

                menu = new ToolStripMenuItem(recent.ToString());
                menu.Click += (s, e) => ReportDailyTask(recent);
                dailyTaskReportToolStripMenuItem.DropDownItems.Add(menu);
            }

            reportToolStripMenuItem.DropDownItems.Clear();
            foreach (string employer in Employers)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem(employer);
                menu.Click += reportToolStripMenuItem_Click;
                reportToolStripMenuItem.DropDownItems.Add(menu);
            }

            weeklyEmployerReportsToolStripMenuItem.DropDownItems.Clear();
            weeklyEmployerReportsToolStripMenuItem.DropDownItems.Add("This Week:");
            foreach (string employer in MonthEmployers(DateTime.Now))
            {
                ToolStripMenuItem menu = new ToolStripMenuItem(employer);
                menu.Click += weeklyReportToolStripMenuItem_Click;
                weeklyEmployerReportsToolStripMenuItem.DropDownItems.Add(menu);
            }

            weeklyEmployerReportsToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            weeklyEmployerReportsToolStripMenuItem.DropDownItems.Add("Last Week:");
            foreach (string employer in MonthEmployers(DateTime.Now))
            {
                ToolStripMenuItem menu = new ToolStripMenuItem(employer);
                menu.Click += lastWeeklyReportToolStripMenuItem_Click;
                weeklyEmployerReportsToolStripMenuItem.DropDownItems.Add(menu);
            }

            monthlyEmployerReportsToolStripMenuItem.DropDownItems.Clear();
            foreach (string employer in Employers)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem(employer);
                menu.Click += monthlyEmployerReportsToolStripMenuItem_Click;
                monthlyEmployerReportsToolStripMenuItem.DropDownItems.Add(menu);
            }

            lblStatus.Text = "Today's Time: ";

            foreach (string employer in Employers)
            {
                lblStatus.Text += employer + " " + Math.Max(Math.Round(TodayTime(employer).TotalHours, 2), 0).ToString() + " | ";
            }

            lblStatus.Text = lblStatus.Text.Remove(lblStatus.Text.Length - 3);

            lblStatus.Text += "          This Week's Time: ";
            groupThisWeek.Text = "This Week: ";

            foreach (string employer in Employers)
            {
                string current = employer + " " + Math.Max(Math.Round(ThisWeekTime(employer).TotalHours, 2), 0).ToString() + " | ";
                lblStatus.Text += current;
                groupThisWeek.Text += current;
            }

            groupLastWeek.Text = "Last Week: ";

            foreach (string employer in Employers)
            {
                string current = employer + " " + Math.Max(Math.Round(LastWeekTime(employer).TotalHours, 2), 0).ToString() + " | ";
                groupLastWeek.Text += current;
            }

            groupThisWeek.Text = groupThisWeek.Text.Remove(groupThisWeek.Text.Length - 3);

            groupLastWeek.Text = groupLastWeek.Text.Remove(groupLastWeek.Text.Length - 3);

            lblStatus.Text = lblStatus.Text.Remove(lblStatus.Text.Length - 3);

            lblStatus.Text += "          This Month's Time: ";

            foreach (string employer in MonthEmployers(DateTime.Now))
            {
                lblStatus.Text += employer + " " + Math.Max(Math.Round(ThisMonthTime(employer).TotalHours, 2), 0).ToString() + " | ";
            }

            lblStatus.Text = lblStatus.Text.Remove(lblStatus.Text.Length - 3);

            dataThisWeek.DataSource = new SortableBinding.SortableBindingList<Object>(settings.TimeEntries.Where(entry => GetWeek(entry.Start) == GetWeek(DateTime.Now) && entry.Start.Year == DateTime.Now.Year).Select(TimeEntryToRow).ToList());
            dataLastWeek.DataSource = new SortableBinding.SortableBindingList<Object>(settings.TimeEntries.Where(entry => GetWeek(entry.Start) == GetWeek(DateTime.Now.AddDays(-7)) && entry.Start.Year == DateTime.Now.AddDays(-7).Year).Select(TimeEntryToRow).ToList());
            //dataPrevious.DataSource = new SortableBinding.SortableBindingList<Object>(settings.TimeEntries.Where(entry => GetWeek(entry.Start) < GetWeek(DateTime.Now.AddDays(-7)) || entry.Start.Year < DateTime.Now.AddDays(-7).Year).Select(entry => new { entry.Project, entry.Task, entry.Employer, entry.Start.DayOfWeek, entry.Start, entry.Stop, entry.Comments }).ToList());

            RefreshPreviousEntries();

            try
            {
                dataThisWeek.FirstDisplayedScrollingRowIndex = temp;
                dataThisWeek.Rows[selected].Selected = true;
                //dataLastWeek.FirstDisplayedScrollingRowIndex = temp;
                //dataLastWeek.Rows[selected].Selected = true;
                //dataPrevious.FirstDisplayedScrollingRowIndex = temp;
                //dataPrevious.Rows[selected].Selected = true;
            }
            catch
            {

            }
        }

        private void StartNewTask(TimeEntry entry)
        {
            if (settings.TimeEntries.Count == 0 || CloseCurrentTask())
            {
                settings.TimeEntries.Add(new TimeEntry(entry.Project, entry.Task, entry.Employer));
                RefreshEntries();
                notifyIcon1.ShowBalloonTip(5000, "Task Started", entry.ToString() + " Started", ToolTipIcon.Info);
            }
        }

        private void AddTimeEntry(TimeEntry newValue)
        {
            if (newValue.Stop.DayOfYear == newValue.Start.AddDays(1).DayOfYear)
            {
                TimeEntry splitValue = new TimeEntry(newValue.Project, newValue.Task, newValue.Employer,
                                                     new DateTime(newValue.Stop.Year, newValue.Stop.Month, newValue.Stop.Day, 0, 0, 0, 1),
                                                     newValue.Stop, newValue.Comments, new JiraInfo(newValue.WorkLog.TaskID, "", newValue.WorkLog.Summary, newValue.WorkLog.Description));

                newValue.Stop = new DateTime(newValue.Start.Year, newValue.Start.Month, newValue.Start.Day, 23, 59, 59);

                newValue.UpdateJira();
                settings.TimeEntries.Add(newValue);
                splitValue.UpdateJira();
                settings.TimeEntries.Add(splitValue);
            }
            else
            {
                newValue.UpdateJira();
                settings.TimeEntries.Add(newValue);
            }
            RefreshEntries();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (settings.TimeEntries.Count == 0 || CloseCurrentTask())
            {
                frmTimeEntry form = new frmTimeEntry();
                form.Text = "Starting a new task...";
                DialogResult result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    TimeEntry newValue = form.Value;
                    AddTimeEntry(newValue);
                }
            }
        }

        private bool CloseCurrentTask()
        {
            TimeEntry lastTask = settings.LastUnclosedTask;

            if (settings.TimeEntries.Count > 0 && lastTask != null)
            {
                TimeEntry temp = new TimeEntry(lastTask);
                temp.Stop = DateTime.Now;
                frmTimeEntry form = new frmTimeEntry(temp, true);
                form.Text = "Closing " + temp.ToString();
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    settings.TimeEntries.Remove(lastTask);
                    TimeEntry newValue = form.Value;
                    AddTimeEntry(newValue);
                    return true;
                }
                return false;
            }
            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(FILE_LOCATION, settings.ToString());
        }

        private void dataEntries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditEntry(ThisWeekCurrentEntry);
        }

        private bool EditEntry(TimeEntry temp)
        {
            frmTimeEntry form = new frmTimeEntry(temp);

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                settings.TimeEntries.Remove(temp);
                TimeEntry newValue = form.Value;
                AddTimeEntry(newValue);
                return true;
            }
            return false;
        }

        private void dataEntries_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Do you really want to delete the entry?", "Are you sure?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ThisWeekCurrentEntry.DeleteJira();
                    settings.TimeEntries.Remove(ThisWeekCurrentEntry);
                    RefreshEntries();
                }
            }
        }

        private void dataLastWeek_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditEntry(LastWeekCurrentEntry);
        }

        private void dataLastWeek_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Do you really want to delete the entry?", "Are you sure?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    LastWeekCurrentEntry.DeleteJira();
                    settings.TimeEntries.Remove(LastWeekCurrentEntry);
                    RefreshEntries();
                }
            }
        }

        private void dataPrevious_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditEntry(PreviousCurrentEntry);
        }

        private void dataPrevious_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Do you really want to delete the entry?", "Are you sure?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    PreviousCurrentEntry.DeleteJira();
                    settings.TimeEntries.Remove(PreviousCurrentEntry);
                    RefreshEntries();
                }
            }
        }

        private TimeEntry ThisWeekCurrentEntry
        {
            get
            {
                return new TimeEntry(dataThisWeek.SelectedRows[0].Cells[0].Value.ToString(), dataThisWeek.SelectedRows[0].Cells[1].Value.ToString(), dataThisWeek.SelectedRows[0].Cells[2].Value.ToString(), (DateTime)dataThisWeek.SelectedRows[0].Cells[5].Value, (DateTime)dataThisWeek.SelectedRows[0].Cells[6].Value, dataThisWeek.SelectedRows[0].Cells[7].Value.ToString(), (JiraInfo)dataThisWeek.SelectedRows[0].Cells[8].Value);
            }
        }

        private TimeEntry LastWeekCurrentEntry
        {
            get
            {
                return new TimeEntry(dataLastWeek.SelectedRows[0].Cells[0].Value.ToString(), dataLastWeek.SelectedRows[0].Cells[1].Value.ToString(), dataLastWeek.SelectedRows[0].Cells[2].Value.ToString(), (DateTime)dataLastWeek.SelectedRows[0].Cells[5].Value, (DateTime)dataLastWeek.SelectedRows[0].Cells[6].Value, dataLastWeek.SelectedRows[0].Cells[7].Value.ToString(), (JiraInfo)dataLastWeek.SelectedRows[0].Cells[8].Value);
            }
        }

        private TimeEntry PreviousCurrentEntry
        {
            get
            {
                DataGridView dataPrevious = tabPrevious.TabPages[tabPrevious.SelectedIndex].Controls[0] as DataGridView;
                return new TimeEntry(dataPrevious.SelectedRows[0].Cells[0].Value.ToString(), dataPrevious.SelectedRows[0].Cells[1].Value.ToString(), dataPrevious.SelectedRows[0].Cells[2].Value.ToString(), (DateTime)dataPrevious.SelectedRows[0].Cells[5].Value, (DateTime)dataPrevious.SelectedRows[0].Cells[6].Value, dataPrevious.SelectedRows[0].Cells[7].Value.ToString(), (JiraInfo)dataPrevious.SelectedRows[0].Cells[8].Value);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseCurrentTask();
        }

        private void timerKeyHooks_Tick(object sender, EventArgs e)
        {
            timerKeyHooks.Enabled = false;
            SetWindowFocus();
            if (pressed == Keys.C)
            {
                closeToolStripMenuItem_Click(sender, e);
            }
            else if (pressed == Keys.A)
            {
                addToolStripMenuItem_Click(sender, e);
            }
            else if (pressed == Keys.S)
            {
                showHideToolStripMenuItem_Click(sender, e);
            }
        }

        private void SetWindowFocus()
        {
            WindowManipulation.BringForwardWindow(this.Text);
        }

        private void showHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
                SetWindowFocus();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerSave_Tick(object sender, EventArgs e)
        {
            string toSave = settings.ToString();
            if (File.ReadAllText(FILE_LOCATION).Trim() != toSave.Trim())
            {
                File.WriteAllText(FILE_LOCATION, toSave);
            }
        }

        #region Reporting

        private void ReportDailyTask(TimeEntry inputEntry)
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

                    foreach (TimeEntry entry in settings.TimeEntries.Where(e => e.Start.Date == day.Date && e.Project == inputEntry.Project && e.Task == inputEntry.Task && e.Employer == inputEntry.Employer))
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

        private void Export(string employer)
        {
            Export(employer, DateTime.MinValue);
        }

        private void Export(string employer, DateTime since)
        {
            Export(employer, since, DateTime.Now);
        }

        private void Export(string employer, DateTime since, DateTime until)
        {
            List<string> timeEntries = new List<string>();

            timeEntries.Add("Project,Task,Venue,Date,Start,Stop,\"=\"\"Comments  (Month to date: \"\"&ROUND(SUM($K:$K),2)&\"\")\"\"\",Time,H+MM,Gap,DailyTime,=P5,Also Exclude:");

            settings.TimeEntries.Reverse();

            foreach (TimeEntry entry in settings.TimeEntries.Where(item => item.Employer == employer && item.Start >= since && item.Stop <= until && item.Stop != TimeEntry.MIN_DATE))
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

            settings.TimeEntries.Reverse();

            string dailyHours = "=" + DailyHours;
            string yesterdaysHours = "=" + YesterdaysHours;
            string weeksHours = "=" + WeeklyHours;
            string monthsHours = "=" + MonthlyHours;

            string tableRow = "," + Quote(dailyHours) + "," + Quote(yesterdaysHours) + "," + Quote(weeksHours) + "," + Quote(monthsHours) + "";

            List<string> totalsChart = new List<string>();
            totalsChart.Add(",Week Starts on:,Sat");
            totalsChart.Add("Total" + tableRow);
            totalsChart.Add(",Today,Yesterday,Weekly,Monthly");

            foreach (string project in ProjectsFor(employer))
            {
                totalsChart.Add(project + tableRow);
            }

            for (int i = 0; i < totalsChart.Count; i++)
            {
                if (timeEntries.Count > (i + 3))
                {
                    timeEntries[i + 3] += ",," + totalsChart[i];
                }
                else
                {
                    timeEntries.Add(",,,,,,,,," + timeEntries[i]);
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

        private string Quote(string input)
        {
            return "\"" + input + "\"";
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export((sender as ToolStripMenuItem).Text);
        }

        private void weeklyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime weekStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            while (weekStart.DayOfWeek != DayOfWeek.Saturday)
            {
                weekStart = weekStart.AddDays(-1);
            }

            Export((sender as ToolStripMenuItem).Text, weekStart);
        }

        private void lastWeeklyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime weekStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            while (weekStart.DayOfWeek != DayOfWeek.Saturday)
            {
                weekStart = weekStart.AddDays(-1);
            }

            DateTime weekEnd = weekStart;

            weekStart = weekStart.AddDays(-1);

            while (weekStart.DayOfWeek != DayOfWeek.Saturday)
            {
                weekStart = weekStart.AddDays(-1);
            }

            Export((sender as ToolStripMenuItem).Text, weekStart, weekEnd);
        }

        private void monthlyEmployerReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChooseMonth monthChooser = new frmChooseMonth();
            monthChooser.Title = "Choose which month to generate a report for.";

            if (monthChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DateTime monthStart = new DateTime(monthChooser.SelectedMonth.Year, monthChooser.SelectedMonth.Month, 1, 0, 0, 0, 0);

                DateTime monthEnd = monthStart.AddMonths(1).AddMilliseconds(-1);

                Export((sender as ToolStripMenuItem).Text, monthStart, monthEnd);
            }
        }

        #endregion

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetWeek(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.showHideToolStripMenuItem_Click(sender, e);
        }

        private void addDailyScrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChooseDay dayChooser = new frmChooseDay();

            if (dayChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (DateTime day = dayChooser.SelectedDates.Start; day <= dayChooser.SelectedDates.End; day = day.AddDays(1))
                {
                    settings.TimeEntries.Add(new TimeEntry("Core Products", "Daily Scrum", "TMT", new DateTime(day.Year, day.Month, day.Day, 8, 45, 0),
                                                            new DateTime(day.Year, day.Month, day.Day, 9, 0, 0), ""));
                }
                RefreshEntries();
            }
        }

        private void timerWorking_Tick(object sender, EventArgs e)
        {
            timerWorking.Enabled = false;

            frmWorking stillWorking = new frmWorking();

            if (stillWorking.ShowDialog() == System.Windows.Forms.DialogResult.No)
            {
                CloseCurrentTask();
                if (!stillWorking.FinishedWorking)
                {
                    TimeEntry newEntry = stillWorking.NewTask;

                    if (newEntry != null)
                    {
                        StartNewTask(newEntry);
                    }
                }
            }

            timerWorking.Interval = settings.StillWorkingTime * 60 * 1000;

            timerWorking.Enabled = true;
        }

        private void jiraLoginSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();

            login.ShowDialog();
        }
    }
}
