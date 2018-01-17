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
        bool startMinimized = false;

        public static Settings settings = new Settings(Settings.FILE_LOCATION);

        #region Constructors

        public frmMain(bool startMinimized)
        {
            InitializeComponent();

            KeyboardHook.HookedKeys.Add(Keys.C);
            KeyboardHook.HookedKeys.Add(Keys.A);
            KeyboardHook.HookedKeys.Add(Keys.S);
            KeyboardHook.KeyUp += KeyboardHook_KeyUp;

            this.startMinimized = startMinimized;
        }

        public frmMain() : this(false) { }

        #endregion

        #region KeyboardHooks

        static globalKeyboardHook KeyboardHook = new globalKeyboardHook();

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

        #endregion

        #region frmMain Events

        private void frmMain_Load(object sender, EventArgs e)
        {
            RefreshEntries();
            RefreshCommonTasks();
            timerSave.Enabled = true;
            timerWorking.Interval = settings.StillWorkingTime * 60 * 1000;
            timerWorking.Enabled = true;
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (startMinimized)
            {
                showHideToolStripMenuItem_Click(sender, e);
                startMinimized = false;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(Settings.FILE_LOCATION, settings.ToString());
        }

        #endregion

        #region Summaries

        public static List<string> Projects
        {
            get
            {
                return frmMain.settings.TimeEntries.Select(entry => entry.Project)
                                                   .Distinct()
                                                   .ToList();
            }
        }

        public static List<string> ProjectsFor(string Employer)
        {
            return frmMain.settings.TimeEntries.Where(entry => entry.Employer == Employer)
                                               .Select(entry => entry.Project)
                                               .Distinct()
                                               .ToList();
        }

        public static List<string> Tasks
        {
            get
            {
                return frmMain.settings.TimeEntries.Select(entry => entry.Task)
                                                   .Distinct()
                                                   .ToList();
            }
        }

        public static List<string> MonthEmployers(DateTime Month)
        {
            return frmMain.settings.TimeEntries.Where(entry => entry.Start.Month == Month.Month && entry.Start.Year == Month.Year)
                                               .Select(entry => entry.Employer)
                                               .Distinct()
                                               .ToList();
        }

        public static List<string> Employers
        {
            get
            {
                return frmMain.settings.TimeEntries.Select(entry => entry.Employer)
                                                   .Distinct()
                                                   .ToList();
            }
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

        #endregion

        #region Times

        private static TimeSpan TodayTime(string employer)
        {
            return Utilities.GetTime(settings.TimeEntries.Where(item => item.Employer == employer && item.Start.Day == DateTime.Now.Day && item.Start.Month == DateTime.Now.Month && item.Start.Year == DateTime.Now.Year));
        }

        private static TimeSpan MonthTime(DateTime Month, string employer)
        {
            return Utilities.GetTime(settings.TimeEntries.Where(item => item.Employer == employer && (new DateTime(item.Start.Year, item.Start.Month, 1)) == Month));
        }

        private static TimeSpan ThisMonthTime(string employer)
        {
            return MonthTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), employer);
        }

        private static TimeSpan ThisWeekTime(string employer)
        {
            return Utilities.GetTime(settings.TimeEntries.Where(item => item.Employer == employer && Utilities.GetWeek(item.Start) == Utilities.GetWeek(DateTime.Now) && item.Start.Year == DateTime.Now.Year));
        }

        private static TimeSpan LastWeekTime(string employer)
        {
            return Utilities.GetTime(settings.TimeEntries.Where(item => item.Employer == employer && Utilities.GetWeek(item.Start) == Utilities.GetWeek(DateTime.Now.AddDays(-7)) && item.Start.Year == DateTime.Now.AddDays(-7).Year));
        }

        #endregion
        
        #region RefreshEntries

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
                                                                                                       .Select(x => x.AsRow)
                                                                                                       .ToList());
            }
        }

        private void RefreshEntries()
        {
            int scrollingIndex = dataThisWeek.FirstDisplayedScrollingRowIndex;
            int selected;
            try
            {
                selected = dataThisWeek.SelectedRows[0].Index;
            }
            catch
            {
                selected = 0;
            }

            int sortedColumn = dataThisWeek.SortedColumn is null ? 5 : dataThisWeek.SortedColumn.Index;
            SortOrder sortedOrder = dataThisWeek.SortedColumn is null ? SortOrder.Descending : dataThisWeek.SortOrder;
            

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
                menu.Click += (s, e) => Reports.ReportDailyTask(recent, saveFileDialogReport);
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

            dataThisWeek.DataSource = new SortableBinding.SortableBindingList<Object>(settings.TimeEntries.Where(entry => Utilities.GetWeek(entry.Start) == Utilities.GetWeek(DateTime.Now) && entry.Start.Year == DateTime.Now.Year).Select(x => x.AsRow).ToList());
            dataLastWeek.DataSource = new SortableBinding.SortableBindingList<Object>(settings.TimeEntries.Where(entry => Utilities.GetWeek(entry.Start) == Utilities.GetWeek(DateTime.Now.AddDays(-7)) && entry.Start.Year == DateTime.Now.AddDays(-7).Year).Select(x => x.AsRow).ToList());
            //dataPrevious.DataSource = new SortableBinding.SortableBindingList<Object>(settings.TimeEntries.Where(entry => Utilities.GetWeek(entry.Start) < Utilities.GetWeek(DateTime.Now.AddDays(-7)) || entry.Start.Year < DateTime.Now.AddDays(-7).Year).Select(entry => new { entry.Project, entry.Task, entry.Employer, entry.Start.DayOfWeek, entry.Start, entry.Stop, entry.Comments }).ToList());

            RefreshPreviousEntries();

            try
            {
                dataThisWeek.Sort(dataThisWeek.Columns[sortedColumn], sortedOrder == SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending);
                dataThisWeek.FirstDisplayedScrollingRowIndex = scrollingIndex;
                dataThisWeek.Rows[selected].Selected = true;
                //dataLastWeek.FirstDisplayedScrollingRowIndex = temp;
                //dataLastWeek.Rows[selected].Selected = true;
                //dataPrevious.FirstDisplayedScrollingRowIndex = temp;
                //dataPrevious.Rows[selected].Selected = true;
            }
            catch
            {

            }

            ColorGaps();
        }

        private void ColorGaps()
        {
            List<TimeEntry> tocolor = new List<TimeEntry>();
            for (int i = 1; i < settings.TimeEntries.Count; i++)
            {
                if (Math.Abs((settings.TimeEntries[i - 1].Start - settings.TimeEntries[i].Stop).TotalMinutes) > 1 &&
                    settings.TimeEntries[i].Stop.Hour < 16)
                {
                    tocolor.Add(settings.TimeEntries[i]);
                }
            }

            foreach (DataGridViewRow row in dataThisWeek.Rows)
            {
                if (tocolor.Contains(new TimeEntry(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), (DateTime)row.Cells[5].Value, (DateTime)row.Cells[6].Value, row.Cells[7].Value.ToString(), (JiraInfo)row.Cells[8].Value)))
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
        }

        #endregion

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

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.showHideToolStripMenuItem_Click(sender, e);
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            SetWindowFocus();
            try
            {
                WindowManipulation.BringForwardWindow(stillWorking.Text);
                stillWorking.Focus();
            }
            catch
            {

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerSave_Tick(object sender, EventArgs e)
        {
            string toSave = settings.ToString();
            if (!File.Exists(Settings.FILE_LOCATION))
            {
                File.WriteAllText(Settings.FILE_LOCATION, toSave);
            }
            else if (File.ReadAllText(Settings.FILE_LOCATION).Trim() != toSave.Trim())
            {
                File.WriteAllText(Settings.FILE_LOCATION, toSave);
            }
        }

        #region Reporting
        
        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.Export((sender as ToolStripMenuItem).Text, saveFileDialogReport);
        }

        private void weeklyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime weekStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            while (weekStart.DayOfWeek != DayOfWeek.Saturday)
            {
                weekStart = weekStart.AddDays(-1);
            }

            Reports.Export((sender as ToolStripMenuItem).Text, weekStart, saveFileDialogReport);
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

            Reports.Export((sender as ToolStripMenuItem).Text, weekStart, weekEnd, saveFileDialogReport);
        }

        private void monthlyEmployerReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChooseMonth monthChooser = new frmChooseMonth();
            monthChooser.Title = "Choose which month to generate a report for.";

            if (monthChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DateTime monthStart = new DateTime(monthChooser.SelectedMonth.Year, monthChooser.SelectedMonth.Month, 1, 0, 0, 0, 0);

                DateTime monthEnd = monthStart.AddMonths(1).AddMilliseconds(-1);

                Reports.Export((sender as ToolStripMenuItem).Text, monthStart, monthEnd, saveFileDialogReport);
            }
        }

        #endregion

        frmWorking stillWorking;
        private void timerWorking_Tick(object sender, EventArgs e)
        {
            //If there is not an active task and there is an already closed task which includes 'NOW', then don't ask about working.
            if(object.ReferenceEquals(frmMain.settings.LastUnclosedTask, null)
                && frmMain.settings.TimeEntries.Count(entry => entry.Start <= DateTime.Now && entry.Stop >= DateTime.Now) > 0)
            {
                return;
            }

            timerWorking.Enabled = false;

            stillWorking = new frmWorking();
            
            notifyIcon1.ShowBalloonTip(10000, "Working Check", "Are you still working/not working?", ToolTipIcon.Info);

            if (stillWorking.ShowDialog(this) == System.Windows.Forms.DialogResult.No)
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

        private void settingsFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerSave.Enabled = false;
            timerWorking.Enabled = false;
            timerSave_Tick(sender, e);

            saveFileDialogSettings.FileName = Settings.FILE_LOCATION;

            if (saveFileDialogSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Settings.FILE_LOCATION = Path.GetFullPath(saveFileDialogSettings.FileName);
                if(File.Exists(Settings.FILE_LOCATION))
                {
                    settings = new Settings(Settings.FILE_LOCATION);
                    this.frmMain_Load(sender, e);
                }
            }
            
            timerSave_Tick(sender, e);
            timerSave.Enabled = true;
            timerWorking.Enabled = true;
        }

        #region CommonTasks

        private void addCommonTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeEntry task = (sender as ToolStripMenuItem).Tag as TimeEntry;
            
            frmChooseDay dayChooser = new frmChooseDay();

            if (dayChooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (DateTime day = dayChooser.SelectedDates.Start; day <= dayChooser.SelectedDates.End; day = day.AddDays(1))
                {
                    AddTimeEntry(new TimeEntry(task.Project, task.Task, task.Employer, new DateTime(day.Year, day.Month, day.Day, task.Start.Hour, task.Start.Minute, 0),
                                                            new DateTime(day.Year, day.Month, day.Day, task.Stop.Hour, task.Stop.Minute, 0), task.Comments));
                }
                RefreshEntries();
            }
        }

        private void newCommonTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimeEntry form = new frmTimeEntry(settings.NextCommonTask);

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                settings.CommonTasks.Add(form.Value);
            }
            RefreshCommonTasks();
        }

        private void deleteCommonTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeEntry task = (sender as ToolStripMenuItem).Tag as TimeEntry;

            if (MessageBox.Show("Do you really want to delete the common task " + task.Task + "?", "Are you sure?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                settings.CommonTasks.Remove(task);
                RefreshCommonTasks();
            }
        }

        private void editCommonTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeEntry task = (sender as ToolStripMenuItem).Tag as TimeEntry;

            frmTimeEntry form = new frmTimeEntry(task);

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                settings.CommonTasks.Remove(task);
                settings.CommonTasks.Add(form.Value);
            }
            RefreshCommonTasks();
        }

        private void RefreshCommonTasks()
        {
            commonTasksToolStripMenuItem.DropDownItems.Clear();
            deleteACommonTaskToolStripMenuItem.DropDownItems.Clear();
            editACommonTaskToolStripMenuItem.DropDownItems.Clear();

            foreach (TimeEntry commonTask in settings.CommonTasks)
            {
                ToolStripMenuItem newCommonTask = new ToolStripMenuItem();
                newCommonTask.Text = "Add " + commonTask.Task;
                newCommonTask.Click += addCommonTaskToolStripMenuItem_Click;
                newCommonTask.Tag = commonTask;
                commonTasksToolStripMenuItem.DropDownItems.Add(newCommonTask);


                ToolStripMenuItem deleteCommonTask = new ToolStripMenuItem();
                deleteCommonTask.Text = "Delete " + commonTask.Task;
                deleteCommonTask.Click += deleteCommonTaskToolStripMenuItem_Click;
                deleteCommonTask.Tag = commonTask;
                deleteACommonTaskToolStripMenuItem.DropDownItems.Add(deleteCommonTask);


                ToolStripMenuItem editCommonTask = new ToolStripMenuItem();
                editCommonTask.Text = "Edit " + commonTask.Task;
                editCommonTask.Click += editCommonTaskToolStripMenuItem_Click;
                editCommonTask.Tag = commonTask;
                editACommonTaskToolStripMenuItem.DropDownItems.Add(editCommonTask);
            }
        }

        #endregion
    }
}
