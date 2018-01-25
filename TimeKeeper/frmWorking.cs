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
    public partial class frmWorking : Form
    {
        public frmWorking()
        {
            InitializeComponent();
        }

        private void frmWorking_Load(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(Settings.CurrentSettings.LastUnclosedTask, null))
            {
                this.radioYes.Text = "Yes, I'm still not working on anything.";
                this.Text = "Still not working";
                this.radioNo.Visible = false;
                this.radioNo.Enabled = false;
            }
            else
            {
                this.radioYes.Text = "Yes, I'm still working on \"" + Settings.CurrentSettings.LastUnclosedTask.ToString() + "\"";
            }

            comboNew.Items.Add("New Task");

            foreach (TimeEntry recent in Settings.CurrentSettings.RecentTasks)
            {
                comboNew.Items.Add(recent);
            }

            comboNew.SelectedIndex = 0;
            numAskAgain.Value = Settings.CurrentSettings.StillWorkingTime;
            this.radioYes.Checked = true;
        }

        public bool FinishedWorking
        {
            get
            {
                return radioNo.Checked;
            }
        }

        public TimeEntry NewTask
        {
            get
            {
                if (comboNew.SelectedIndex == 0)
                {
                    frmTimeEntry form = new frmTimeEntry();
                    form.Text = "Starting a new task...";
                    DialogResult result = form.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        return form.Value;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return comboNew.SelectedItem as TimeEntry;
                }
            }
        }

        public string Comment
        {
            get
            {
                if (radioYes.Checked)
                    return txtComment.Text.Trim();
                else
                    return "";
            }
            set
            {
                txtComment.Text = value.Trim();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (radioYes.Checked)
            {
                DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.No;
            }

            Settings.CurrentSettings.StillWorkingTime = (int)this.numAskAgain.Value;

            this.Close();
        }

        private void comboNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioNew.Checked = true;
        }

        private void all_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOk_Click(sender, e);
                e.Handled = true;
            }
        }

        private void radioYes_CheckedChanged(object sender, EventArgs e)
        {
            txtComment.Enabled = radioYes.Checked;
        }
    }
}
