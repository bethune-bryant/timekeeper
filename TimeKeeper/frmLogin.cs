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
            this.txtURL.Text = JiraInterface.JiraURL;
            this.txtUser.Text = JiraInterface.Username;
            this.txtPass.Text = JiraInterface.Password;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            JiraInterface.JiraURL = this.txtURL.Text;
            JiraInterface.Username = this.txtUser.Text;
            JiraInterface.Password = this.txtPass.Text;

            this.Close();
        }
    }
}
