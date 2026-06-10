using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArenaX.Data;
using ArenaX.Models;

namespace ArenaX.Forms.SubForm
{
    public partial class AddTeamForm : Form
    {
        FocusBoarder focusBoarder = new FocusBoarder();
        public AddTeamForm()
        {
            InitializeComponent();
            AttachFocusEvents();
            btnCreate.Click += btnCreate_Click;
        }

        private void btnCreate_Click(object? sender, EventArgs e)
        {
            string name = tbxName.Text.Trim();
            string captain = tbxCaptain.Text.Trim();
            string email = tbxContact.Text.Trim();
            string description = tbxDescription.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(captain) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in Name, Captain, and Contact Email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var team = new Team
            {
                TeamName = name,
                CaptainName = captain,
                CaptainEmail = email,
                TeamDescription = description
            };

            if (DatabaseHelper.AddTeam(team))
            {
                MessageBox.Show("Team added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add team.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AttachFocusEvents()
        {
           
            this.Paint += AddTeamForm_Paint;

            tbxName.Enter += TextBox_StateChange;
            tbxName.Leave += TextBox_StateChange;
            
            tbxCaptain.Enter += TextBox_StateChange;
            tbxCaptain.Leave += TextBox_StateChange;
            
            tbxContact.Enter += TextBox_StateChange;
            tbxContact.Leave += TextBox_StateChange;
            
            tbxDescription.Enter += TextBox_StateChange;
            tbxDescription.Leave += TextBox_StateChange;
        }

        private void TextBox_StateChange(object sender, EventArgs e)
        {
      
            this.Invalidate();
        }

        private void AddTeamForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            focusBoarder.DrawTextBoxBorder(g, tbxName);
            focusBoarder.DrawTextBoxBorder(g, tbxCaptain);
            focusBoarder.DrawTextBoxBorder(g, tbxContact);
            focusBoarder.DrawTextBoxBorder(g, tbxDescription);
        }

     

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tbxName_MouseClick(object sender, MouseEventArgs e)
        {
            tbxName.Focus();

        }

        private void tbxCaptain_MouseClick(object sender, MouseEventArgs e)
        {

        }

    }
}
