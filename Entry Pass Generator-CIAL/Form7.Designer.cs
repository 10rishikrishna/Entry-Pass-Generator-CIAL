
using Microsoft.Data.SqlClient;

namespace Entry_Pass_Generator_CIAL
{
    partial class Form7
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form7));
            pictureBox4 = new PictureBox();
            textBox1 = new TextBox();
            label1 = new Label();
            panel4 = new Panel();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox13 = new PictureBox();
            label22 = new Label();
            label14 = new Label();
            label15 = new Label();
            pictureBox7 = new PictureBox();
            pictureBox9 = new PictureBox();
            label18 = new Label();
            label20 = new Label();
            pictureBox10 = new PictureBox();
            label19 = new Label();
            pictureBox8 = new PictureBox();
            label16 = new Label();
            panel5 = new Panel();
            pictureBox5 = new PictureBox();
            btnblacklist = new PictureBox();
            textBox2 = new TextBox();
            label2 = new Label();
            pictureBox2 = new PictureBox();
            label5 = new Label();
            pictureBox11 = new PictureBox();
            label17 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnblacklist).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            SuspendLayout();
            // 
            // pictureBox4
            // 
            pictureBox4.Cursor = Cursors.Hand;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(742, 45);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(122, 50);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 2;
            pictureBox4.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.WhiteSmoke;
            textBox1.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(12, 45);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Enter Aadhaar Number";
            textBox1.Size = new Size(552, 50);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 13);
            label1.Name = "label1";
            label1.Size = new Size(358, 23);
            label1.TabIndex = 0;
            label1.Text = "Enter the Aadhaar Number of the worker";
            // 
            // panel4
            // 
            panel4.BackColor = Color.White;
            panel4.Controls.Add(pictureBox4);
            panel4.Controls.Add(pictureBox3);
            panel4.Controls.Add(textBox1);
            panel4.Controls.Add(label1);
            panel4.Location = new Point(292, 96);
            panel4.Name = "panel4";
            panel4.Size = new Size(918, 103);
            panel4.TabIndex = 62;
            // 
            // pictureBox3
            // 
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(594, 45);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(123, 50);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, -10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(240, 92);
            pictureBox1.TabIndex = 59;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDarkDark;
            panel1.Controls.Add(pictureBox11);
            panel1.Controls.Add(pictureBox13);
            panel1.Controls.Add(label17);
            panel1.Controls.Add(label22);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(pictureBox7);
            panel1.Controls.Add(pictureBox9);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(label20);
            panel1.Controls.Add(pictureBox10);
            panel1.Controls.Add(label19);
            panel1.Controls.Add(pictureBox8);
            panel1.Controls.Add(label16);
            panel1.Location = new Point(0, 80);
            panel1.Name = "panel1";
            panel1.Size = new Size(240, 754);
            panel1.TabIndex = 58;
            // 
            // pictureBox13
            // 
            pictureBox13.Image = (Image)resources.GetObject("pictureBox13.Image");
            pictureBox13.Location = new Point(49, 682);
            pictureBox13.Name = "pictureBox13";
            pictureBox13.Size = new Size(44, 44);
            pictureBox13.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox13.TabIndex = 80;
            pictureBox13.TabStop = false;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Cursor = Cursors.Hand;
            label22.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label22.Location = new Point(96, 693);
            label22.Name = "label22";
            label22.Size = new Size(70, 23);
            label22.TabIndex = 79;
            label22.Text = "Logout";
            label22.Click += label22_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ButtonFace;
            label14.Location = new Point(9, 41);
            label14.Name = "label14";
            label14.Size = new Size(127, 23);
            label14.TabIndex = 65;
            label14.Tag = "MAIN MENU";
            label14.Text = "MAIN MENU";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Cursor = Cursors.Hand;
            label15.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(54, 96);
            label15.Name = "label15";
            label15.Size = new Size(180, 23);
            label15.TabIndex = 66;
            label15.Text = "Labour Registration";
            label15.Click += label15_Click;
            // 
            // pictureBox7
            // 
            pictureBox7.Cursor = Cursors.Hand;
            pictureBox7.Image = (Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new Point(11, 83);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(44, 44);
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.TabIndex = 67;
            pictureBox7.TabStop = false;
            pictureBox7.Click += pictureBox7_Click;
            // 
            // pictureBox9
            // 
            pictureBox9.Cursor = Cursors.Hand;
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(11, 460);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(44, 44);
            pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox9.TabIndex = 76;
            pictureBox9.TabStop = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Cursor = Cursors.Hand;
            label18.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.Location = new Point(59, 163);
            label18.Name = "label18";
            label18.Size = new Size(129, 23);
            label18.TabIndex = 68;
            label18.Text = "Renew Passes";
            label18.Click += label18_Click;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Cursor = Cursors.Hand;
            label20.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label20.Location = new Point(54, 472);
            label20.Name = "label20";
            label20.Size = new Size(153, 23);
            label20.TabIndex = 75;
            label20.Text = "Approved Passes";
            // 
            // pictureBox10
            // 
            pictureBox10.Cursor = Cursors.Hand;
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(14, 151);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(44, 44);
            pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox10.TabIndex = 69;
            pictureBox10.TabStop = false;
            pictureBox10.Click += pictureBox10_Click;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label19.ForeColor = SystemColors.ButtonFace;
            label19.Location = new Point(9, 420);
            label19.Name = "label19";
            label19.Size = new Size(157, 23);
            label19.TabIndex = 74;
            label19.Tag = "MAIN MENU";
            label19.Text = "MANAGEMENT";
            // 
            // pictureBox8
            // 
            pictureBox8.Cursor = Cursors.Hand;
            pictureBox8.Image = (Image)resources.GetObject("pictureBox8.Image");
            pictureBox8.Location = new Point(14, 211);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(40, 43);
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox8.TabIndex = 73;
            pictureBox8.TabStop = false;
            pictureBox8.Click += pictureBox8_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Cursor = Cursors.Hand;
            label16.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(60, 224);
            label16.Name = "label16";
            label16.Size = new Size(145, 23);
            label16.TabIndex = 72;
            label16.Text = "Blacklist People";
            label16.Click += label16_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.Controls.Add(pictureBox5);
            panel5.Controls.Add(btnblacklist);
            panel5.Controls.Add(textBox2);
            panel5.Controls.Add(label2);
            panel5.Location = new Point(292, 412);
            panel5.Name = "panel5";
            panel5.Size = new Size(918, 141);
            panel5.TabIndex = 63;
            panel5.Paint += panel5_Paint;
            // 
            // pictureBox5
            // 
            pictureBox5.Cursor = Cursors.Hand;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(731, 61);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(109, 49);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 2;
            pictureBox5.TabStop = false;
            // 
            // btnblacklist
            // 
            btnblacklist.Cursor = Cursors.Hand;
            btnblacklist.Image = (Image)resources.GetObject("btnblacklist.Image");
            btnblacklist.Location = new Point(594, 61);
            btnblacklist.Name = "btnblacklist";
            btnblacklist.Size = new Size(111, 50);
            btnblacklist.SizeMode = PictureBoxSizeMode.StretchImage;
            btnblacklist.TabIndex = 0;
            btnblacklist.TabStop = false;
            btnblacklist.Click += btnblacklist_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.WhiteSmoke;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(12, 45);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Submit Reason";
            textBox2.Size = new Size(560, 83);
            textBox2.TabIndex = 1;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 13);
            label2.Name = "label2";
            label2.Size = new Size(355, 23);
            label2.TabIndex = 0;
            label2.Text = "Enter the Reason to Blacklist the worker";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(238, -6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1055, 91);
            pictureBox2.TabIndex = 64;
            pictureBox2.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(1247, 9);
            label5.Name = "label5";
            label5.Size = new Size(34, 38);
            label5.TabIndex = 65;
            label5.Text = "X";
            label5.Click += label5_Click;
            // 
            // pictureBox11
            // 
            pictureBox11.Cursor = Cursors.Hand;
            pictureBox11.Image = (Image)resources.GetObject("pictureBox11.Image");
            pictureBox11.Location = new Point(10, 274);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(44, 44);
            pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox11.TabIndex = 73;
            pictureBox11.TabStop = false;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Cursor = Cursors.Hand;
            label17.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(60, 287);
            label17.Name = "label17";
            label17.Size = new Size(129, 23);
            label17.TabIndex = 72;
            label17.Text = "Search Passes";
            // 
            // Form7
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1303, 832);
            Controls.Add(label5);
            Controls.Add(pictureBox2);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form7";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form7";
            Load += Form7_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnblacklist).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }




        #endregion

        private PictureBox pictureBox4;
        private TextBox textBox1;
        private Label label1;
        private Panel panel4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Panel panel5;
        private PictureBox pictureBox5;
        private PictureBox btnblacklist;
        private TextBox textBox2;
        private Label label2;
        private PictureBox pictureBox2;
        private PictureBox pictureBox13;
        private Label label22;
        private Label label14;
        private Label label15;
        private PictureBox pictureBox7;
        private PictureBox pictureBox9;
        private Label label18;
        private Label label20;
        private PictureBox pictureBox10;
        private Label label19;
        private PictureBox pictureBox8;
        private Label label16;
        private Label label5;
        private PictureBox pictureBox11;
        private Label label17;
    }
}