using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArenaX.Forms
{
    public partial class TeamForm : Form
    {
        public TeamForm()
        {
            InitializeComponent();
            btnStatus.Visible = false;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            btnStatus.Visible = true;
            btnStatus.Text = "Approved";
            btnStatus.BackColor = Color.FromArgb(114, 102, 255); 
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            btnStatus.Visible = true;
            btnStatus.Text = "Rejected";
            btnStatus.BackColor = Color.FromArgb(249, 5, 43);
        }
    }
}
