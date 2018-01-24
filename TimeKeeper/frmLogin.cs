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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.txtURL.Text = Settings.CurrentSettings.JiraURL;
            this.txtUser.Text = Settings.CurrentSettings.JiraUsername;
            this.txtPass.Text = Settings.CurrentSettings.JiraPassword;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Settings.CurrentSettings.JiraURL = this.txtURL.Text;
            Settings.CurrentSettings.JiraUsername = this.txtUser.Text;
            Settings.CurrentSettings.JiraPassword = this.txtPass.Text;

            this.Close();
        }

        private void all_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOk_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
