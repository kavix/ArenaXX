using ArenaX.Forms.SubForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArenaX.Data;
using ArenaX.Models;

namespace ArenaX.Forms
{
    public partial class TeamForm : Form
    {
        public TeamForm()
        {
            InitializeComponent();
            btnStatus.Visible = false;
            LoadTeams();
        }

        private void LoadTeams()
        {
            try
            {
                var teams = DatabaseHelper.GetAllTeams();
                flowLayoutPanel1.Controls.Clear();

                foreach (var team in teams)
                {
                    Panel card = CreateTeamCard(team);
                    flowLayoutPanel1.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teams: " + ex.Message);
            }
        }

        private Panel CreateTeamCard(Team team)
        {
            // Create the main panel
            Panel panel = new Panel();
            panel.BackColor = Color.FromArgb(8, 12, 37);
            panel.BorderStyle = BorderStyle.None;
            panel.Size = new Size(516, 261);
            panel.Margin = new Padding(20);

            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, Color.FromArgb(41, 46, 73), ButtonBorderStyle.Solid);
            };

            // Team Name Label
            Label lblName = new Label();
            lblName.AutoSize = true;
            lblName.Font = new Font("Candara", 13.8F, FontStyle.Bold);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(24, 17);
            lblName.Text = team.TeamName;
            panel.Controls.Add(lblName);

            // Captain Label Header
            Label lblCapHeader = new Label();
            lblCapHeader.AutoSize = true;
            lblCapHeader.ForeColor = Color.White;
            lblCapHeader.Location = new Point(33, 59);
            lblCapHeader.Text = "Captain:";
            panel.Controls.Add(lblCapHeader);

            // Captain Name Value Label
            Label lblCapName = new Label();
            lblCapName.AutoSize = true;
            lblCapName.ForeColor = Color.WhiteSmoke;
            lblCapName.Location = new Point(103, 59);
            lblCapName.Text = team.CaptainName;
            panel.Controls.Add(lblCapName);

            // Email Label
            Label lblTeamEmail = new Label();
            lblTeamEmail.AutoSize = true;
            lblTeamEmail.ForeColor = Color.White;
            lblTeamEmail.Location = new Point(33, 92);
            lblTeamEmail.Text = team.CaptainEmail;
            panel.Controls.Add(lblTeamEmail);

            // Description Label
            Label lblDesc = new Label();
            lblDesc.AutoSize = true;
            lblDesc.ForeColor = Color.White;
            lblDesc.Location = new Point(33, 124);
            lblDesc.Text = team.TeamDescription;
            panel.Controls.Add(lblDesc);

            // Status Button
            Button btnTeamStatus = new Button();
            btnTeamStatus.BackColor = Color.Black;
            btnTeamStatus.FlatAppearance.BorderSize = 0;
            btnTeamStatus.FlatStyle = FlatStyle.Flat;
            btnTeamStatus.ForeColor = Color.White;
            btnTeamStatus.Location = new Point(390, 19);
            btnTeamStatus.Size = new Size(94, 29);
            btnTeamStatus.Text = team.ApprovalStatus;
            btnTeamStatus.Visible = team.ApprovalStatus != "Pending";
            if (team.ApprovalStatus == "Approved") btnTeamStatus.BackColor = Color.FromArgb(114, 102, 255);
            else if (team.ApprovalStatus == "Rejected") btnTeamStatus.BackColor = Color.FromArgb(249, 5, 43);
            panel.Controls.Add(btnTeamStatus);

            // Approve Button
            Button btnApproveTeam = new Button();
            Button btnRejectTeam = new Button();

            btnApproveTeam.BackColor = Color.FromArgb(0, 183, 100);
            btnApproveTeam.FlatAppearance.BorderSize = 0;
            btnApproveTeam.FlatStyle = FlatStyle.Flat;
            btnApproveTeam.ForeColor = Color.White;
            btnApproveTeam.Location = new Point(24, 195);
            btnApproveTeam.Size = new Size(134, 41);
            btnApproveTeam.Text = "Approve";
            btnApproveTeam.Visible = team.ApprovalStatus == "Pending";
            btnApproveTeam.Click += (s, e) =>
            {
                // TODO: Update database status
                btnApproveTeam.Visible = false;
                btnRejectTeam.Visible = false;
                btnTeamStatus.Visible = true;
                btnTeamStatus.Text = "Approved";
                btnTeamStatus.BackColor = Color.FromArgb(114, 102, 255);
            };
            panel.Controls.Add(btnApproveTeam);

            // Reject Button
            btnRejectTeam.BackColor = Color.FromArgb(3, 6, 23);
            btnRejectTeam.FlatAppearance.BorderColor = Color.DimGray;
            btnRejectTeam.FlatStyle = FlatStyle.Flat;
            btnRejectTeam.ForeColor = Color.White;
            btnRejectTeam.Location = new Point(172, 195);
            btnRejectTeam.Size = new Size(134, 41);
            btnRejectTeam.Text = "Reject";
            btnRejectTeam.Visible = team.ApprovalStatus == "Pending";
            btnRejectTeam.Click += (s, e) =>
            {
                // TODO: Update database status
                btnApproveTeam.Visible = false;
                btnRejectTeam.Visible = false;
                btnTeamStatus.Visible = true;
                btnTeamStatus.Text = "Rejected";
                btnTeamStatus.BackColor = Color.FromArgb(249, 5, 43);
            };
            panel.Controls.Add(btnRejectTeam);

            return panel;
        }

        private void btnAddTeam_Click(object sender, EventArgs e) 
        {
            using (var form = new AddTeamForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadTeams();
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
