using Intercept_Intervals.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnviosColombiaGold
{
    public partial class frmSplash : Form
    {
        private string user = String.Empty;
        public frmSplash(string loggedUser)
        {
            InitializeComponent();
            this.Hide();
            user = loggedUser;
        }

        public frmSplash()
        {
            InitializeComponent();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            frm_Intercept_Intervals oPpal = new frm_Intercept_Intervals(this.user);
            oPpal.Show();
            this.Hide();
            timer1.Enabled = false;
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }
    }
}
