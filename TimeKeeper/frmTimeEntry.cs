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
            this.dateTimePickerStart.Value = input.Start;
            this.dateTimePickerStop.Value = input.Stop;
            this.txtComments.Text = input.Comments;
            this.workLog = input.WorkLog;
            this.comboJiraTask.Text = input.WorkLog.TaskID;

            this.btnStopAtNow.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        public TimeEntry Value
        {
            get
            {
                if (this.workLog.TaskID != comboJiraTask.Text)
                {
                    this.workLog = new JiraInfo(comboJiraTask.Text);
                }
                return new TimeEntry(this.comboProject.Text.Trim(), this.comboTask.Text.Trim(), 
                                     this.comboEmployer.Text.Trim(), this.dateTimePickerStart.Value,
                                     this.dateTimePickerStop.Value, this.txtComments.Text.Trim(),
                                     this.workLog);
            }
            set
            {
                this.comboProject.Text = value.Project;
                this.comboTask.Text = value.Task;
                this.comboEmployer.Text = value.Employer;
                this.dateTimePickerStart.Value = value.Start;
                this.dateTimePickerStop.Value = value.Stop;
                this.txtComments.Text = value.Comments;
                this.workLog = value.WorkLog;
                this.comboJiraTask.Text = value.WorkLog.TaskID;
            }
        }

        private void frmTimeEntry_Load(object sender, EventArgs e)
        {
            comboProject.Items.AddRange(frmMain.Projects.ToArray());
            if (comboProject.Text.Trim().Length == 0 && comboProject.Items.Count > 0)
                comboProject.SelectedItem = comboProject.Items[0];

            comboTask.Items.AddRange(frmMain.Tasks.ToArray()); 
            if (comboTask.Text.Trim().Length == 0 && comboTask.Items.Count > 0)
                comboTask.SelectedItem = comboTask.Items[0];

            comboEmployer.Items.AddRange(frmMain.Employers.ToArray());
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
            
            return true;
        }

        private void btnStartAtLast_Click(object sender, EventArgs e)
        {
            this.dateTimePickerStart.Value = frmMain.settings.LastClosedTask.Stop;
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
