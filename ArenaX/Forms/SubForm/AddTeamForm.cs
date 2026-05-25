using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArenaX.Forms.SubForm
{
    public partial class AddTeamForm : Form
    {
        private Color defaultBorderColor = Color.FromArgb(41, 46, 73); // Default border color, update if different
        private Color focusedBorderColor = Color.FromArgb(114, 102, 255);

        public AddTeamForm()
        {
            InitializeComponent();
            AttachFocusEvents();
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
            DrawTextBoxBorder(g, tbxName);
            DrawTextBoxBorder(g, tbxCaptain);
            DrawTextBoxBorder(g, tbxContact);
            DrawTextBoxBorder(g, tbxDescription);
        }

        private void DrawTextBoxBorder(Graphics g, TextBox txt)
        {
            Color borderColor = txt.Focused ? focusedBorderColor : defaultBorderColor;

          
            using (Pen pen = new Pen(borderColor, 1))
            {
                Rectangle rect = new Rectangle(
                    txt.Location.X - 1, 
                    txt.Location.Y - 1, 
                    txt.Width + 1, 
                    txt.Height + 1);
                
                g.DrawRectangle(pen, rect);
            }
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
