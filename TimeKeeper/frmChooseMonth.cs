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
    public partial class frmChooseMonth : Form
    {
        public frmChooseMonth()
        {
            InitializeComponent();
        }

        private void frmChooseDay_Load(object sender, EventArgs e)
        {
            this.dateTimePicker.MinDate = Settings.CurrentSettings.TimeEntries.Select(entry => entry.Start).Min();
            this.dateTimePicker.MaxDate = Settings.CurrentSettings.TimeEntries.Select(entry => entry.Start).Max();
            this.dateTimePicker.Value = this.dateTimePicker.MaxDate;
        }

        public DateTime SelectedMonth
        {
            get
            {
                return dateTimePicker.Value; ;
            }
        }

        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = Title;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
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
