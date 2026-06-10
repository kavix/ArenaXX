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
    public partial class TournamentsForm : Form
    {
        public TournamentsForm()
        {
            InitializeComponent();
            LoadTournaments();
        }

        private void LoadTournaments()
        {
            try
            {
                var tournaments = DatabaseHelper.GetAllTournaments();
                flwTournamentCard.Controls.Clear();
                
                foreach (var tournament in tournaments)
                {
                    Panel card = CreateTournamentCard(tournament);
                    flwTournamentCard.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tournaments: " + ex.Message);
            }
        }

        private Panel CreateTournamentCard(Tournament tournament)
        {
            Panel panel = new Panel();
            panel.BackColor = Color.FromArgb(7, 11, 34);
            panel.Size = new Size(600, 130);
            panel.Margin = new Padding(20);

            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, Color.FromArgb(41, 46, 73), ButtonBorderStyle.Solid);
            };

            Panel pnlImage = new Panel();
            pnlImage.BackColor = Color.Transparent;
            pnlImage.Size = new Size(50, 50);
            pnlImage.Location = new Point(25, 25);
            
            string imagePath = System.IO.Path.Combine(Application.StartupPath, "Assets", "trophy.png");
            if (!System.IO.File.Exists(imagePath))
            {
                 imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets", "trophy.png");
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

            Label lblName = new Label();
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(90, 20);
            lblName.Text = tournament.TournamentName;
            panel.Controls.Add(lblName);

            Button btnType = new Button();
            btnType.FlatStyle = FlatStyle.Flat;
            btnType.FlatAppearance.BorderSize = 1;
            btnType.FlatAppearance.BorderColor = Color.FromArgb(41, 46, 73);
            btnType.BackColor = Color.FromArgb(12, 17, 43);
            btnType.ForeColor = Color.White;
            btnType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnType.Text = tournament.TournamentFormat;
            btnType.Size = new Size(TextRenderer.MeasureText(tournament.TournamentFormat, btnType.Font).Width + 20, 30);
            btnType.Location = new Point(95, 60);
            panel.Controls.Add(btnType);

            Button btnStatus = new Button();
            btnStatus.FlatStyle = FlatStyle.Flat;
            btnStatus.FlatAppearance.BorderSize = 0;
            btnStatus.BackColor = Color.FromArgb(134, 114, 255);
            btnStatus.ForeColor = Color.White;
            btnStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStatus.Text = tournament.Status;
            btnStatus.Size = new Size(TextRenderer.MeasureText(tournament.Status, btnStatus.Font).Width + 20, 30);
            btnStatus.Location = new Point(btnType.Right + 10, 60);
            panel.Controls.Add(btnStatus);

            Button btnGenerate = new Button();
            btnGenerate.FlatStyle = FlatStyle.Flat;
            btnGenerate.FlatAppearance.BorderSize = 0;
            btnGenerate.BackColor = Color.FromArgb(134, 114, 255);
            btnGenerate.ForeColor = Color.White;
            btnGenerate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGenerate.Text = "🔀  Generate";
            btnGenerate.Size = new Size(130, 45);
            btnGenerate.Location = new Point(440, 28);
            panel.Controls.Add(btnGenerate);

            return panel;
        }
    }
}
