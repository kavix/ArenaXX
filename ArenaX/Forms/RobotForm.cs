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
    public partial class RobotForm : Form
    {
        public RobotForm()
        {
            InitializeComponent();
            LoadRobots();
        }

        private void LoadRobots()
        {
            try
            {
                var robots = DatabaseHelper.GetAllRobots();
                flwRobotCard.Controls.Clear();

                foreach (var robot in robots)
                {
                    Panel card = CreateRobotCard(robot);
                    flwRobotCard.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading robots: " + ex.Message);
            }
        }

        private Panel CreateRobotCard(RobotDetail robot)
        {
            // Create the main panel based on the image layout
            Panel panel = new Panel();
            panel.BackColor = Color.FromArgb(7, 11, 34);
            panel.Size = new Size(500, 200);
            panel.Margin = new Padding(20);

            // Custom border color
            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, Color.FromArgb(41, 46, 73), ButtonBorderStyle.Solid);
            };

            // Image Placeholder Panel 
            Panel pnlImage = new Panel();
            pnlImage.BackColor = Color.FromArgb(7, 11, 34);
            pnlImage.Size = new Size(50, 50);
            pnlImage.Location = new Point(25, 25);


            string imagePath = System.IO.Path.Combine(Application.StartupPath, "Assets", "robotIcon.png");
            // If running from bin/Debug/net10.0-windows, Assets might be up a few levels or copied to output
            if (!System.IO.File.Exists(imagePath))
            {
                 // Fallback for development environment
                 imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets", "robotIcon.png");
            }

            if (System.IO.File.Exists(imagePath))
            {
                Image icon = Image.FromFile(imagePath);
                PictureBox pictureBox = new PictureBox();
                pictureBox.BackColor = Color.FromArgb(7, 11, 34);
                pictureBox.Image = icon;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Height = 50;
                pictureBox.Width = 50;
                pictureBox.Location = new Point(0, 0);
                pnlImage.Controls.Add(pictureBox);
            }

            panel.Controls.Add(pnlImage);

            // Robot Name Label
            Label lblName = new Label();
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(130, 20);
            lblName.Text = robot.RobotName;
            panel.Controls.Add(lblName);

            // Team Name Label
            Label lblTeam = new Label();
            lblTeam.AutoSize = true;
            lblTeam.Font = new Font("Segoe UI", 9F);
            lblTeam.ForeColor = Color.FromArgb(151, 159, 181);
            lblTeam.Location = new Point(134, 60);
            lblTeam.Text = robot.TeamName;
            panel.Controls.Add(lblTeam);

            // Category Button/Badge
            Button btnCategory = new Button();
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.FlatAppearance.BorderSize = 1;
            btnCategory.FlatAppearance.BorderColor = Color.FromArgb(41, 46, 73);
            btnCategory.BackColor = Color.FromArgb(12, 17, 43);
            btnCategory.ForeColor = Color.White;
            btnCategory.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            btnCategory.Text = robot.Category;
            btnCategory.Size = new Size(TextRenderer.MeasureText(robot.Category, btnCategory.Font).Width + 20, 35);
            btnCategory.Location = new Point(134, 105);
            panel.Controls.Add(btnCategory);

            // Weight Button/Badge
            string weightText = robot.Weight.ToString("0.##") + "kg";
            Button btnWeight = new Button();
            btnWeight.FlatStyle = FlatStyle.Flat;
            btnWeight.FlatAppearance.BorderSize = 0;
            btnWeight.BackColor = Color.FromArgb(31, 38, 76);
            btnWeight.ForeColor = Color.White;
            btnWeight.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            btnWeight.Text = weightText;
            btnWeight.Size = new Size(TextRenderer.MeasureText(weightText, btnWeight.Font).Width + 20, 35);
            btnWeight.Location = new Point(btnCategory.Right + 10, 105);

            panel.Controls.Add(btnWeight);

            // Description Label
            Label lblDesc = new Label();
            lblDesc.AutoSize = true;
            lblDesc.Font = new Font("Segoe UI", 9F);
            lblDesc.ForeColor = Color.FromArgb(125, 136, 161);
            lblDesc.Location = new Point(25, 155);
            lblDesc.Text = robot.Description;
            panel.Controls.Add(lblDesc);

            return panel;
        }

        private void btnRegisterTeam_Click(object sender, EventArgs e)
        {
            using (var form = new RegisterRobot())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadRobots();
                }
            }
        }
    }
}
