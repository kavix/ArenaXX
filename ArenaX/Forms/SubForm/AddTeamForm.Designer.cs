namespace ArenaX.Forms.SubForm
{
    partial class AddTeamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTeamForm));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            lblName = new Label();
            tbxName = new TextBox();
            tbxCaptain = new TextBox();
            label2 = new Label();
            tbxContact = new TextBox();
            label3 = new Label();
            tbxDescription = new TextBox();
            label4 = new Label();
            btnCreate = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(609, 20);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(25, 25);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bahnschrift Condensed", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(22, 17);
            label1.Name = "label1";
            label1.Size = new Size(88, 28);
            label1.TabIndex = 1;
            label1.Text = "New Team";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Bahnschrift Condensed", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(22, 73);
            lblName.Name = "lblName";
            lblName.Size = new Size(43, 22);
            lblName.TabIndex = 2;
            lblName.Text = "Name";
            // 
            // tbxName
            // 
            tbxName.BackColor = Color.FromArgb(3, 6, 23);
            tbxName.BorderStyle = BorderStyle.FixedSingle;
            tbxName.ForeColor = Color.White;
            tbxName.Location = new Point(22, 108);
            tbxName.Multiline = true;
            tbxName.Name = "tbxName";
            tbxName.Size = new Size(612, 42);
            tbxName.TabIndex = 3;
            tbxName.MouseClick += tbxName_MouseClick;
            // 
            // tbxCaptain
            // 
            tbxCaptain.BackColor = Color.FromArgb(3, 6, 23);
            tbxCaptain.BorderStyle = BorderStyle.FixedSingle;
            tbxCaptain.ForeColor = Color.White;
            tbxCaptain.Location = new Point(22, 206);
            tbxCaptain.Multiline = true;
            tbxCaptain.Name = "tbxCaptain";
            tbxCaptain.Size = new Size(612, 42);
            tbxCaptain.TabIndex = 5;
            tbxCaptain.MouseClick += tbxCaptain_MouseClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Bahnschrift Condensed", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(22, 171);
            label2.Name = "label2";
            label2.Size = new Size(56, 22);
            label2.TabIndex = 4;
            label2.Text = "Captain";
            // 
            // tbxContact
            // 
            tbxContact.BackColor = Color.FromArgb(3, 6, 23);
            tbxContact.BorderStyle = BorderStyle.FixedSingle;
            tbxContact.ForeColor = Color.White;
            tbxContact.Location = new Point(22, 303);
            tbxContact.Multiline = true;
            tbxContact.Name = "tbxContact";
            tbxContact.Size = new Size(612, 42);
            tbxContact.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Bahnschrift Condensed", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(22, 268);
            label3.Name = "label3";
            label3.Size = new Size(94, 22);
            label3.TabIndex = 6;
            label3.Text = "Contact email";
            // 
            // tbxDescription
            // 
            tbxDescription.BackColor = Color.FromArgb(3, 6, 23);
            tbxDescription.BorderStyle = BorderStyle.FixedSingle;
            tbxDescription.ForeColor = Color.White;
            tbxDescription.Location = new Point(22, 404);
            tbxDescription.Multiline = true;
            tbxDescription.Name = "tbxDescription";
            tbxDescription.Size = new Size(612, 42);
            tbxDescription.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Bahnschrift Condensed", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(22, 369);
            label4.Name = "label4";
            label4.Size = new Size(79, 22);
            label4.TabIndex = 8;
            label4.Text = "Description";
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(130, 111, 255);
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(22, 502);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(612, 43);
            btnCreate.TabIndex = 10;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = false;
            // 
            // AddTeamForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(3, 6, 23);
            ClientSize = new Size(662, 574);
            Controls.Add(btnCreate);
            Controls.Add(tbxDescription);
            Controls.Add(label4);
            Controls.Add(tbxContact);
            Controls.Add(label3);
            Controls.Add(tbxCaptain);
            Controls.Add(label2);
            Controls.Add(tbxName);
            Controls.Add(lblName);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            MinimizeBox = false;
            Name = "AddTeamForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "AddTeamForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label lblName;
        private TextBox tbxName;
        private TextBox tbxCaptain;
        private Label label2;
        private TextBox tbxContact;
        private Label label3;
        private TextBox tbxDescription;
        private Label label4;
        private Button btnCreate;
    }
}