using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeKeeper
{
    public partial class frmTimeEntry : Form
    {
        private bool commentFocus = false;
        private JiraInfo workLog = new JiraInfo();

        public frmTimeEntry()
            : this(new TimeEntry())
        {
        }

        public frmTimeEntry(TimeEntry input, bool comment)
            : this(input)
        {
            this.commentFocus = comment;
        }

        public frmTimeEntry(TimeEntry input)
        {
            InitializeComponent();
            this.comboProject.Text = input.Project;
            this.comboTask.Text = input.Task;
            this.comboEmployer.Text = input.Employer;
            this.dateTimePickerStart.Value = new DateTime(input.Start.Year, input.Start.Month, input.Start.Day, input.Start.Hour, input.Start.Minute, 0);
            this.dateTimePickerStop.Value = new DateTime(input.Stop.Year, input.Stop.Month, input.Stop.Day, input.Stop.Hour, input.Stop.Minute, 0);
            this.txtComments.Text = input.Comments;
            this.workLog = input.WorkLog;
            this.comboJiraTask.Text = input.WorkLog.TaskID;

            this.btnStopAtNow.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        public DateTime Start
        {
            get
            {
                return this.dateTimePickerStart.Value;
            }
            set
            {
                this.dateTimePickerStart.Value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
            }
        }

        public TimeEntry Value
        {
            get
            {
                if (this.workLog.TaskID != comboJiraTask.Text)
                {
                    this.workLog = new JiraInfo(comboJiraTask.Text);
                }
                return new TimeEntry(this.comboProject.Text.Trim(), this.comboTask.Text.Trim(), this.comboEmployer.Text.Trim(),
                                     new DateTime(this.dateTimePickerStart.Value.Year, this.dateTimePickerStart.Value.Month, this.dateTimePickerStart.Value.Day, this.dateTimePickerStart.Value.Hour, this.dateTimePickerStart.Value.Minute, 0),
                                     new DateTime(this.dateTimePickerStop.Value.Year, this.dateTimePickerStop.Value.Month, this.dateTimePickerStop.Value.Day, this.dateTimePickerStop.Value.Hour, this.dateTimePickerStop.Value.Minute, 0), 
                                     this.txtComments.Text.Trim(), this.workLog);
            }
            set
            {
                this.comboProject.Text = value.Project;
                this.comboTask.Text = value.Task;
                this.comboEmployer.Text = value.Employer;
                this.dateTimePickerStart.Value = new DateTime(value.Start.Year, value.Start.Month, value.Start.Day, value.Start.Hour, value.Start.Minute, 0);
                this.dateTimePickerStop.Value = new DateTime(value.Stop.Year, value.Stop.Month, value.Stop.Day, value.Stop.Hour, value.Stop.Minute, 0);
                this.txtComments.Text = value.Comments;
                this.workLog = value.WorkLog;
                this.comboJiraTask.Text = value.WorkLog.TaskID;
            }
        }

        private void frmTimeEntry_Load(object sender, EventArgs e)
        {
            comboProject.Items.AddRange(Settings.CurrentSettings.Projects.ToArray());
            if (comboProject.Text.Trim().Length == 0 && comboProject.Items.Count > 0)
                comboProject.SelectedItem = comboProject.Items[0];

            comboTask.Items.AddRange(Settings.CurrentSettings.Tasks.ToArray()); 
            if (comboTask.Text.Trim().Length == 0 && comboTask.Items.Count > 0)
                comboTask.SelectedItem = comboTask.Items[0];

            comboEmployer.Items.AddRange(Settings.CurrentSettings.Employers.ToArray());
            if (comboEmployer.Text.Trim().Length == 0 && comboEmployer.Items.Count > 0)
                comboEmployer.SelectedItem = comboEmployer.Items[0];

            comboJiraTask.Items.AddRange(JiraInterface.GetJiraTaskIDs().ToArray());
            //if (comboJiraTask.Text.Trim().Length == 0 && comboJiraTask.Items.Count > 0)
            //    comboJiraTask.SelectedItem = comboJiraTask.Items[0];
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ValidateInput())
            {
                if (this.workLog.TaskID.Length > 0 && this.workLog.WorkLogID.Length > 0 && this.workLog.TaskID != this.comboJiraTask.Text)
                {
                    JiraInterface.DeleteWorkLog(this.workLog.TaskID, this.workLog.WorkLogID);
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void frmTimeEntry_Shown(object sender, EventArgs e)
        {
            if (commentFocus)
            {
                txtComments.Focus();
                commentFocus = false;
            }
        }

        private void comboJiraTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboJiraTask.Items.Contains((sender as ComboBox).Text))
            {
                this.txtComments.Text = JiraInterface.GetJiraInfo((sender as ComboBox).Text).Summary;
            }
        }

        private bool ValidateInput()
        {
            if ((dateTimePickerStop.Value - dateTimePickerStart.Value).TotalSeconds <= 0 && dateTimePickerStop.Value != TimeEntry.MIN_DATE)
            {
                MessageBox.Show("Stop Time must be greater than Start Time!", "Error: Start >= Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if ((dateTimePickerStop.Value - dateTimePickerStart.Value).TotalHours > 12 && dateTimePickerStop.Value != TimeEntry.MIN_DATE)
            {
                MessageBox.Show("Task time cannot be greater than 12 hours. If this was not made in error, please split the task in 2.", "Error: Task Time > 12 Hours!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            /*
            if(Settings.CurrentSettings.TimeEntries.Where(entry => (dateTimePickerStart.Value >= entry.Start && dateTimePickerStart.Value < entry.Stop) ||
                                                           (dateTimePickerStop.Value != TimeEntry.MIN_DATE && 
                                                               ((dateTimePickerStop.Value > entry.Start && dateTimePickerStop.Value < entry.Stop) ||
                                                                (entry.Start >= dateTimePickerStart.Value && entry.Start < dateTimePickerStop.Value) ||
                                                                (entry.Stop > dateTimePickerStart.Value && entry.Stop < dateTimePickerStop.Value)))).Count() > 0)
            {
                MessageBox.Show("Tasks cannot overlap with each other.", "Error: Task Overlap!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            */
            return true;
        }

        private void btnStartAtLast_Click(object sender, EventArgs e)
        {
            try
            {
                this.dateTimePickerStart.Value = Settings.CurrentSettings.LastClosedTask.Stop;
            }
            catch
            {
                this.dateTimePickerStart.Value = DateTime.Now;
            }
        }

        private void btnStopAtNow_Click(object sender, EventArgs e)
        {
            this.dateTimePickerStop.Value = DateTime.Now;
        }

        private void all_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOK_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
