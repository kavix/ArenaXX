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
    public partial class RegisterRobot : Form
    {
        FocusBoarder focusBoarder = new FocusBoarder();

        public RegisterRobot()
        {
            InitializeComponent();
            attachFocusEvents();
        }
        private void attachFocusEvents()
        {
            this.Paint += RegisterRobot_Paint;
            tbxRobotName.Enter += TextBox_StateChange;
            tbxRobotName.Leave += TextBox_StateChange;
            tbxWeight.Enter += TextBox_StateChange;
            tbxWeight.Leave += TextBox_StateChange;
            tbxSpecs.Enter += TextBox_StateChange;
            tbxSpecs.Leave += TextBox_StateChange;

            cbxTeam.Enter += TextBox_StateChange;
            cbxTeam.Leave += TextBox_StateChange;
            cbxCategory.Enter += TextBox_StateChange;
            cbxCategory.Leave += TextBox_StateChange;
        }

        private void RegisterRobot_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            focusBoarder.DrawTextBoxBorder(g, tbxRobotName);
            focusBoarder.DrawTextBoxBorder(g, tbxWeight);
            focusBoarder.DrawTextBoxBorder(g, tbxSpecs);

            focusBoarder.DrawComboBoxBorder(g, cbxTeam);
            focusBoarder.DrawComboBoxBorder(g, cbxCategory);
        }

        private void TextBox_StateChange(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        public void cbx_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox combo = sender as ComboBox;
            bool isHoveredOrSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Hover background color identical to the active selection cyan requested
            Color backColor = isHoveredOrSelected ? Color.FromArgb(0, 206, 201) : Color.FromArgb(3, 6, 23);
            Brush bgBrush = new SolidBrush(backColor);

            e.Graphics.FillRectangle(bgBrush, e.Bounds);

            // Draw text
            Brush textBrush = new SolidBrush(Color.White);
            e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds.X + 5, e.Bounds.Y + 8);
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (cbxTeam.SelectedItem == null)
            {
                MessageBox.Show("Please select a team.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = tbxRobotName.Text.Trim();
            string category = cbxCategory.SelectedItem?.ToString() ?? "";
            string weightStr = tbxWeight.Text.Trim();
            string specs = tbxSpecs.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(weightStr))
            {
                MessageBox.Show("Please fill in Robot Name and Weight.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(weightStr, out decimal weight))
            {
                MessageBox.Show("Please enter a valid weight.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var team = (Team)cbxTeam.SelectedItem;

            var robot = new Robot
            {
                TeamID = team.TeamID,
                RobotName = name,
                Category = category,
                Weight = weight,
                Description = specs
            };

            if (DatabaseHelper.AddRobot(robot))
            {
                MessageBox.Show("Robot registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to register robot.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RegisterRobot_Load(object sender, EventArgs e)
        {
            string[] category = { "featherweight (<= 1.5kg)", "lightweight ( <= 5.4kg)", "heavyweight (<= 10kg)" };
            cbxCategory.Items.AddRange(category);
            cbxCategory.SelectedIndex = 0;

            LoadTeams();
        }

        private void LoadTeams()
        {
            try
            {
                var teams = DatabaseHelper.GetAllTeams();
                cbxTeam.DataSource = teams;
                cbxTeam.DisplayMember = "TeamName";
                cbxTeam.ValueMember = "TeamID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teams: " + ex.Message);
            }
        }
    }
}
