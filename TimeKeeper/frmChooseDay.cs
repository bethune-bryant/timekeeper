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
    public partial class frmChooseDay : Form
    {
        public frmChooseDay()
        {
            InitializeComponent();
            this.monthCalendar1.MaxSelectionCount = 5;
        }

        private void frmChooseDay_Load(object sender, EventArgs e)
        {
            this.monthCalendar1.MinDate = DateTime.Now.Subtract(new TimeSpan(365, 0, 0, 0));
            this.monthCalendar1.MaxDate = DateTime.Now.Add(new TimeSpan(365, 0, 0, 0));
            this.monthCalendar1.SelectionRange = new SelectionRange(DateTime.Now, DateTime.Now);
        }

        public SelectionRange SelectedDates
        {
            get
            {
                return monthCalendar1.SelectionRange;
            }
        }

        public int SelectionCount
        {
            get
            {
                return this.monthCalendar1.MaxSelectionCount;
            }
            set
            {
                this.monthCalendar1.MaxSelectionCount = value;
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
