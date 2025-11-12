using Microsoft.Data.SqlClient;

namespace Entry_Pass_Generator_CIAL
{
    partial class Form5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox11 = new PictureBox();
            pictureBox13 = new PictureBox();
            label17 = new Label();
            label16 = new Label();
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
            Labour_infobox = new GroupBox();
            arearenew = new TextBox();
            clearbtn = new Button();
            label11 = new Label();
            savebtn = new Button();
            button1 = new Button();
            dobrenew = new DateTimePicker();
            label6 = new Label();
            label8 = new Label();
            outdatetime = new DateTimePicker();
            entrytime = new DateTimePicker();
            label24 = new Label();
            label3 = new Label();
            label2 = new Label();
            outdaterenew = new DateTimePicker();
            indate = new DateTimePicker();
            contractorrenew = new TextBox();
            label7 = new Label();
            gaterenew = new TextBox();
            label5 = new Label();
            fullnamerenew = new TextBox();
            label4 = new Label();
            labourrenew = new TextBox();
            label1 = new Label();
            watchpreview = new Button();
            groupBox1 = new GroupBox();
            panel3 = new Panel();
            totimepasscial = new TextBox();
            textBox1 = new TextBox();
            gatespass = new TextBox();
            areaspass = new TextBox();
            label27 = new Label();
            label26 = new Label();
            textBox15 = new TextBox();
            label25 = new Label();
            label23 = new Label();
            todaypass = new TextBox();
            fromdaypass = new TextBox();
            textBox10 = new TextBox();
            textBox8 = new TextBox();
            label13 = new Label();
            dobpass = new TextBox();
            contractorpass = new TextBox();
            labouridpass = new TextBox();
            label12 = new Label();
            label9 = new Label();
            namepass = new TextBox();
            photopass = new PictureBox();
            textBox13 = new TextBox();
            pictureBox14 = new PictureBox();
            panel4 = new Panel();
            textBox12 = new TextBox();
            pictureBox6 = new PictureBox();
            panel2 = new Panel();
            pictureBox4 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            label10 = new Label();
            sqlCommand1 = new SqlCommand();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            Labour_infobox.SuspendLayout();
            groupBox1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)photopass).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-1, -5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(240, 92);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDarkDark;
            panel1.Controls.Add(pictureBox11);
            panel1.Controls.Add(pictureBox13);
            panel1.Controls.Add(label17);
            panel1.Controls.Add(label16);
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
            panel1.Location = new Point(-1, 79);
            panel1.Name = "panel1";
            panel1.Size = new Size(240, 754);
            panel1.TabIndex = 2;
            // 
            // pictureBox11
            // 
            pictureBox11.Cursor = Cursors.Hand;
            pictureBox11.Image = (Image)resources.GetObject("pictureBox11.Image");
            pictureBox11.Location = new Point(9, 266);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(44, 44);
            pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox11.TabIndex = 73;
            pictureBox11.TabStop = false;
            // 
            // pictureBox13
            // 
            pictureBox13.Image = (Image)resources.GetObject("pictureBox13.Image");
            pictureBox13.Location = new Point(49, 672);
            pictureBox13.Name = "pictureBox13";
            pictureBox13.Size = new Size(44, 44);
            pictureBox13.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox13.TabIndex = 70;
            pictureBox13.TabStop = false;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Cursor = Cursors.Hand;
            label17.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(59, 279);
            label17.Name = "label17";
            label17.Size = new Size(129, 23);
            label17.TabIndex = 72;
            label17.Text = "Search Passes";
            label17.Click += label17_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Cursor = Cursors.Hand;
            label16.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(59, 215);
            label16.Name = "label16";
            label16.Size = new Size(145, 23);
            label16.TabIndex = 62;
            label16.Text = "Blacklist People";
            label16.Click += label16_Click;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Cursor = Cursors.Hand;
            label22.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label22.Location = new Point(96, 683);
            label22.Name = "label22";
            label22.Size = new Size(70, 23);
            label22.TabIndex = 69;
            label22.Text = "Logout";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ButtonFace;
            label14.Location = new Point(9, 31);
            label14.Name = "label14";
            label14.Size = new Size(127, 23);
            label14.TabIndex = 55;
            label14.Tag = "MAIN MENU";
            label14.Text = "MAIN MENU";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Cursor = Cursors.Hand;
            label15.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(54, 86);
            label15.Name = "label15";
            label15.Size = new Size(180, 23);
            label15.TabIndex = 56;
            label15.Text = "Labour Registration";
            label15.Click += label15_Click;
            // 
            // pictureBox7
            // 
            pictureBox7.Cursor = Cursors.Hand;
            pictureBox7.Image = (Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new Point(11, 73);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(44, 44);
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.TabIndex = 57;
            pictureBox7.TabStop = false;
            // 
            // pictureBox9
            // 
            pictureBox9.Cursor = Cursors.Hand;
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(11, 450);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(44, 44);
            pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox9.TabIndex = 66;
            pictureBox9.TabStop = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Cursor = Cursors.Hand;
            label18.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.Location = new Point(59, 153);
            label18.Name = "label18";
            label18.Size = new Size(129, 23);
            label18.TabIndex = 58;
            label18.Text = "Renew Passes";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Cursor = Cursors.Hand;
            label20.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label20.Location = new Point(54, 462);
            label20.Name = "label20";
            label20.Size = new Size(153, 23);
            label20.TabIndex = 65;
            label20.Text = "Approved Passes";
            label20.Click += label20_Click;
            // 
            // pictureBox10
            // 
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(14, 141);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(44, 44);
            pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox10.TabIndex = 59;
            pictureBox10.TabStop = false;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label19.ForeColor = SystemColors.ButtonFace;
            label19.Location = new Point(9, 410);
            label19.Name = "label19";
            label19.Size = new Size(157, 23);
            label19.TabIndex = 64;
            label19.Tag = "MAIN MENU";
            label19.Text = "MANAGEMENT";
            // 
            // pictureBox8
            // 
            pictureBox8.Cursor = Cursors.Hand;
            pictureBox8.Image = (Image)resources.GetObject("pictureBox8.Image");
            pictureBox8.Location = new Point(12, 203);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(40, 43);
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox8.TabIndex = 63;
            pictureBox8.TabStop = false;
            // 
            // Labour_infobox
            // 
            Labour_infobox.BackColor = Color.White;
            Labour_infobox.Controls.Add(arearenew);
            Labour_infobox.Controls.Add(clearbtn);
            Labour_infobox.Controls.Add(label11);
            Labour_infobox.Controls.Add(savebtn);
            Labour_infobox.Controls.Add(button1);
            Labour_infobox.Controls.Add(dobrenew);
            Labour_infobox.Controls.Add(label6);
            Labour_infobox.Controls.Add(label8);
            Labour_infobox.Controls.Add(outdatetime);
            Labour_infobox.Controls.Add(entrytime);
            Labour_infobox.Controls.Add(label24);
            Labour_infobox.Controls.Add(label3);
            Labour_infobox.Controls.Add(label2);
            Labour_infobox.Controls.Add(outdaterenew);
            Labour_infobox.Controls.Add(indate);
            Labour_infobox.Controls.Add(contractorrenew);
            Labour_infobox.Controls.Add(label7);
            Labour_infobox.Controls.Add(gaterenew);
            Labour_infobox.Controls.Add(label5);
            Labour_infobox.Controls.Add(fullnamerenew);
            Labour_infobox.Controls.Add(label4);
            Labour_infobox.Controls.Add(labourrenew);
            Labour_infobox.Controls.Add(label1);
            Labour_infobox.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Labour_infobox.Location = new Point(257, 85);
            Labour_infobox.Name = "Labour_infobox";
            Labour_infobox.Size = new Size(505, 718);
            Labour_infobox.TabIndex = 4;
            Labour_infobox.TabStop = false;
            Labour_infobox.Text = "Worker Information";
            // 
            // arearenew
            // 
            arearenew.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            arearenew.Location = new Point(328, 627);
            arearenew.Multiline = true;
            arearenew.Name = "arearenew";
            arearenew.PlaceholderText = "Enter Area Access";
            arearenew.Size = new Size(158, 36);
            arearenew.TabIndex = 31;
            arearenew.TextChanged += arearenew_TextChanged;
            // 
            // clearbtn
            // 
            clearbtn.BackColor = Color.WhiteSmoke;
            clearbtn.Cursor = Cursors.Hand;
            clearbtn.Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clearbtn.Location = new Point(197, 673);
            clearbtn.Name = "clearbtn";
            clearbtn.Size = new Size(127, 33);
            clearbtn.TabIndex = 10;
            clearbtn.Text = "Clear";
            clearbtn.UseVisualStyleBackColor = false;
            clearbtn.Click += clearbtn_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(328, 596);
            label11.Name = "label11";
            label11.Size = new Size(97, 20);
            label11.TabIndex = 30;
            label11.Text = "Area Access";
            // 
            // savebtn
            // 
            savebtn.BackColor = Color.FromArgb(0, 192, 0);
            savebtn.Cursor = Cursors.Hand;
            savebtn.Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            savebtn.Location = new Point(349, 673);
            savebtn.Name = "savebtn";
            savebtn.Size = new Size(127, 33);
            savebtn.TabIndex = 9;
            savebtn.Text = "Save";
            savebtn.UseVisualStyleBackColor = false;
            savebtn.Click += savebtn_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlDark;
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(42, 673);
            button1.Name = "button1";
            button1.Size = new Size(127, 33);
            button1.TabIndex = 0;
            button1.Text = "Back";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dobrenew
            // 
            dobrenew.CustomFormat = "dd-mm-yyyy";
            dobrenew.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dobrenew.Format = DateTimePickerFormat.Short;
            dobrenew.Location = new Point(42, 629);
            dobrenew.Name = "dobrenew";
            dobrenew.Size = new Size(157, 28);
            dobrenew.TabIndex = 29;
            dobrenew.Value = new DateTime(2025, 10, 3, 0, 0, 0, 0);
            dobrenew.ValueChanged += dobrenew_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(42, 596);
            label6.Name = "label6";
            label6.Size = new Size(108, 20);
            label6.TabIndex = 28;
            label6.Text = "Date Of Birth";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(333, 401);
            label8.Name = "label8";
            label8.Size = new Size(130, 20);
            label8.TabIndex = 25;
            label8.Text = "Check-Out Time";
            // 
            // outdatetime
            // 
            outdatetime.CustomFormat = "00:00";
            outdatetime.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            outdatetime.Format = DateTimePickerFormat.Time;
            outdatetime.Location = new Point(333, 434);
            outdatetime.Name = "outdatetime";
            outdatetime.ShowUpDown = true;
            outdatetime.Size = new Size(157, 28);
            outdatetime.TabIndex = 24;
            outdatetime.Value = new DateTime(2025, 10, 3, 0, 0, 0, 0);
            outdatetime.ValueChanged += outdatetime_ValueChanged;
            // 
            // entrytime
            // 
            entrytime.CustomFormat = "00:00";
            entrytime.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            entrytime.Format = DateTimePickerFormat.Time;
            entrytime.Location = new Point(333, 342);
            entrytime.Name = "entrytime";
            entrytime.ShowUpDown = true;
            entrytime.Size = new Size(157, 28);
            entrytime.TabIndex = 23;
            entrytime.Value = new DateTime(2025, 10, 3, 0, 0, 0, 0);
            entrytime.ValueChanged += entrytime_ValueChanged;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label24.Location = new Point(42, 138);
            label24.Name = "label24";
            label24.Size = new Size(86, 20);
            label24.TabIndex = 38;
            label24.Text = "Contractor";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(333, 309);
            label3.Name = "label3";
            label3.Size = new Size(91, 20);
            label3.TabIndex = 22;
            label3.Text = "Entry Time";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(43, 401);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 21;
            label2.Text = "Out Date";
            // 
            // outdaterenew
            // 
            outdaterenew.CustomFormat = "dd-mm-yyyy";
            outdaterenew.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            outdaterenew.Format = DateTimePickerFormat.Short;
            outdaterenew.Location = new Point(43, 434);
            outdaterenew.Name = "outdaterenew";
            outdaterenew.Size = new Size(157, 28);
            outdaterenew.TabIndex = 20;
            outdaterenew.Value = new DateTime(2025, 10, 3, 0, 0, 0, 0);
            outdaterenew.ValueChanged += outdaterenew_ValueChanged;
            // 
            // indate
            // 
            indate.CustomFormat = "dd-mm-yyyy";
            indate.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            indate.Format = DateTimePickerFormat.Short;
            indate.Location = new Point(43, 342);
            indate.Name = "indate";
            indate.Size = new Size(157, 28);
            indate.TabIndex = 19;
            indate.Value = new DateTime(2025, 10, 3, 0, 0, 0, 0);
            indate.ValueChanged += indate_ValueChanged;
            // 
            // contractorrenew
            // 
            contractorrenew.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            contractorrenew.Location = new Point(42, 161);
            contractorrenew.Multiline = true;
            contractorrenew.Name = "contractorrenew";
            contractorrenew.PlaceholderText = "Enter Contractor's Name";
            contractorrenew.Size = new Size(250, 30);
            contractorrenew.TabIndex = 18;
            contractorrenew.TextChanged += contractorrenew_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(43, 309);
            label7.Name = "label7";
            label7.Size = new Size(63, 20);
            label7.TabIndex = 13;
            label7.Text = "In Date";
            // 
            // gaterenew
            // 
            gaterenew.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gaterenew.Location = new Point(42, 243);
            gaterenew.Multiline = true;
            gaterenew.Name = "gaterenew";
            gaterenew.PlaceholderText = "Enter Gate Names";
            gaterenew.Size = new Size(250, 36);
            gaterenew.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(42, 208);
            label5.Name = "label5";
            label5.Size = new Size(97, 20);
            label5.TabIndex = 9;
            label5.Text = "Gate Access";
            // 
            // fullnamerenew
            // 
            fullnamerenew.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fullnamerenew.Location = new Point(43, 532);
            fullnamerenew.Multiline = true;
            fullnamerenew.Name = "fullnamerenew";
            fullnamerenew.PlaceholderText = "FullName";
            fullnamerenew.Size = new Size(447, 36);
            fullnamerenew.TabIndex = 8;
            fullnamerenew.TextChanged += fullnamerenew_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(43, 497);
            label4.Name = "label4";
            label4.Size = new Size(84, 20);
            label4.TabIndex = 7;
            label4.Text = "Full Name";
            // 
            // labourrenew
            // 
            labourrenew.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labourrenew.Location = new Point(43, 74);
            labourrenew.Multiline = true;
            labourrenew.Name = "labourrenew";
            labourrenew.PlaceholderText = "Enter Labour Id";
            labourrenew.Size = new Size(250, 29);
            labourrenew.TabIndex = 2;
            labourrenew.TextChanged += labourrenew_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(43, 41);
            label1.Name = "label1";
            label1.Size = new Size(81, 20);
            label1.TabIndex = 0;
            label1.Text = "Labour Id";
            // 
            // watchpreview
            // 
            watchpreview.BackColor = SystemColors.ActiveBorder;
            watchpreview.Cursor = Cursors.Hand;
            watchpreview.Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            watchpreview.Location = new Point(877, 679);
            watchpreview.Name = "watchpreview";
            watchpreview.Size = new Size(316, 33);
            watchpreview.TabIndex = 55;
            watchpreview.Text = "Watch preview ";
            watchpreview.UseVisualStyleBackColor = false;
            watchpreview.Click += watchpreview_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(panel3);
            groupBox1.Controls.Add(panel2);
            groupBox1.Controls.Add(pictureBox4);
            groupBox1.Location = new Point(768, 85);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(508, 594);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Entry Pass Preview";
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.Controls.Add(totimepasscial);
            panel3.Controls.Add(textBox1);
            panel3.Controls.Add(gatespass);
            panel3.Controls.Add(areaspass);
            panel3.Controls.Add(label27);
            panel3.Controls.Add(label26);
            panel3.Controls.Add(textBox15);
            panel3.Controls.Add(label25);
            panel3.Controls.Add(label23);
            panel3.Controls.Add(todaypass);
            panel3.Controls.Add(fromdaypass);
            panel3.Controls.Add(textBox10);
            panel3.Controls.Add(textBox8);
            panel3.Controls.Add(label13);
            panel3.Controls.Add(dobpass);
            panel3.Controls.Add(contractorpass);
            panel3.Controls.Add(labouridpass);
            panel3.Controls.Add(label12);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(namepass);
            panel3.Controls.Add(photopass);
            panel3.Controls.Add(textBox13);
            panel3.Controls.Add(pictureBox14);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(pictureBox6);
            panel3.Location = new Point(21, 26);
            panel3.Name = "panel3";
            panel3.Size = new Size(472, 562);
            panel3.TabIndex = 0;
            panel3.Paint += panel3_Paint;
            // 
            // totimepasscial
            // 
            totimepasscial.BackColor = Color.WhiteSmoke;
            totimepasscial.BorderStyle = BorderStyle.None;
            totimepasscial.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            totimepasscial.Location = new Point(189, 442);
            totimepasscial.Name = "totimepasscial";
            totimepasscial.Size = new Size(58, 23);
            totimepasscial.TabIndex = 69;
            totimepasscial.TextChanged += totimepasscial_TextChanged;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.WhiteSmoke;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(189, 406);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(58, 23);
            textBox1.TabIndex = 68;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // gatespass
            // 
            gatespass.BackColor = Color.WhiteSmoke;
            gatespass.BorderStyle = BorderStyle.None;
            gatespass.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gatespass.Location = new Point(326, 439);
            gatespass.Multiline = true;
            gatespass.Name = "gatespass";
            gatespass.Size = new Size(131, 27);
            gatespass.TabIndex = 67;
            gatespass.TextChanged += gatespass_TextChanged;
            // 
            // areaspass
            // 
            areaspass.BackColor = Color.WhiteSmoke;
            areaspass.BorderStyle = BorderStyle.None;
            areaspass.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            areaspass.Location = new Point(326, 399);
            areaspass.Name = "areaspass";
            areaspass.Size = new Size(131, 23);
            areaspass.TabIndex = 66;
            areaspass.TextChanged += areaspass_TextChanged;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(271, 442);
            label27.Name = "label27";
            label27.Size = new Size(49, 20);
            label27.TabIndex = 65;
            label27.Text = "Gates:";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(271, 406);
            label26.Name = "label26";
            label26.Size = new Size(49, 20);
            label26.TabIndex = 64;
            label26.Text = "Areas:";
            // 
            // textBox15
            // 
            textBox15.BackColor = Color.WhiteSmoke;
            textBox15.BorderStyle = BorderStyle.None;
            textBox15.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox15.Location = new Point(-8, 373);
            textBox15.Multiline = true;
            textBox15.Name = "textBox15";
            textBox15.Size = new Size(106, 27);
            textBox15.TabIndex = 63;
            textBox15.Text = "Validity";
            textBox15.TextAlign = HorizontalAlignment.Center;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(19, 445);
            label25.Name = "label25";
            label25.Size = new Size(28, 20);
            label25.TabIndex = 62;
            label25.Text = "To:";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(17, 406);
            label23.Name = "label23";
            label23.Size = new Size(46, 20);
            label23.TabIndex = 61;
            label23.Text = "From:";
            // 
            // todaypass
            // 
            todaypass.BackColor = Color.WhiteSmoke;
            todaypass.BorderStyle = BorderStyle.None;
            todaypass.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            todaypass.Location = new Point(71, 442);
            todaypass.Name = "todaypass";
            todaypass.Size = new Size(108, 23);
            todaypass.TabIndex = 60;
            todaypass.TextChanged += todaypass_TextChanged;
            // 
            // fromdaypass
            // 
            fromdaypass.BackColor = Color.WhiteSmoke;
            fromdaypass.BorderStyle = BorderStyle.None;
            fromdaypass.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fromdaypass.Location = new Point(70, 406);
            fromdaypass.Name = "fromdaypass";
            fromdaypass.Size = new Size(109, 23);
            fromdaypass.TabIndex = 59;
            fromdaypass.TextChanged += fromdaypass_TextChanged;
            // 
            // textBox10
            // 
            textBox10.BackColor = Color.WhiteSmoke;
            textBox10.BorderStyle = BorderStyle.None;
            textBox10.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox10.Location = new Point(271, 372);
            textBox10.Multiline = true;
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(112, 27);
            textBox10.TabIndex = 58;
            textBox10.Text = "Access Gates:";
            textBox10.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            textBox8.BackColor = Color.WhiteSmoke;
            textBox8.BorderStyle = BorderStyle.None;
            textBox8.Font = new Font("Arial", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox8.Location = new Point(235, 264);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(17, 32);
            textBox8.TabIndex = 57;
            textBox8.Text = " |";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(256, 273);
            label13.Name = "label13";
            label13.Size = new Size(43, 20);
            label13.TabIndex = 56;
            label13.Text = "DOB:";
            // 
            // dobpass
            // 
            dobpass.BackColor = Color.WhiteSmoke;
            dobpass.BorderStyle = BorderStyle.None;
            dobpass.Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dobpass.Location = new Point(300, 275);
            dobpass.Name = "dobpass";
            dobpass.Size = new Size(157, 20);
            dobpass.TabIndex = 55;
            dobpass.TextAlign = HorizontalAlignment.Center;
            dobpass.TextChanged += dobpass_TextChanged;
            // 
            // contractorpass
            // 
            contractorpass.BackColor = Color.WhiteSmoke;
            contractorpass.BorderStyle = BorderStyle.None;
            contractorpass.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            contractorpass.Location = new Point(164, 304);
            contractorpass.Multiline = true;
            contractorpass.Name = "contractorpass";
            contractorpass.Size = new Size(212, 27);
            contractorpass.TabIndex = 54;
            contractorpass.TextAlign = HorizontalAlignment.Center;
            contractorpass.TextChanged += contractorpass_TextChanged;
            // 
            // labouridpass
            // 
            labouridpass.BackColor = Color.WhiteSmoke;
            labouridpass.BorderStyle = BorderStyle.FixedSingle;
            labouridpass.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labouridpass.Location = new Point(116, 271);
            labouridpass.Name = "labouridpass";
            labouridpass.Size = new Size(117, 27);
            labouridpass.TabIndex = 53;
            labouridpass.TextAlign = HorizontalAlignment.Center;
            labouridpass.TextChanged += labouridpass_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(38, 273);
            label12.Name = "label12";
            label12.Size = new Size(75, 20);
            label12.TabIndex = 52;
            label12.Text = "Labour Id:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(75, 307);
            label9.Name = "label9";
            label9.Size = new Size(82, 20);
            label9.TabIndex = 51;
            label9.Text = "Contractor:";
            // 
            // namepass
            // 
            namepass.BackColor = Color.WhiteSmoke;
            namepass.BorderStyle = BorderStyle.None;
            namepass.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            namepass.Location = new Point(78, 234);
            namepass.Name = "namepass";
            namepass.Size = new Size(348, 23);
            namepass.TabIndex = 49;
            namepass.TextAlign = HorizontalAlignment.Center;
            namepass.TextChanged += namepass_TextChanged;
            // 
            // photopass
            // 
            photopass.BorderStyle = BorderStyle.FixedSingle;
            photopass.Location = new Point(189, 83);
            photopass.Name = "photopass";
            photopass.Size = new Size(124, 144);
            photopass.SizeMode = PictureBoxSizeMode.StretchImage;
            photopass.TabIndex = 48;
            photopass.TabStop = false;
            photopass.Click += photopass_Click;
            // 
            // textBox13
            // 
            textBox13.BackColor = Color.FromArgb(77, 108, 172);
            textBox13.BorderStyle = BorderStyle.FixedSingle;
            textBox13.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox13.Location = new Point(0, 523);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(479, 39);
            textBox13.TabIndex = 47;
            textBox13.Text = "                TEMPORARY PASS";
            // 
            // pictureBox14
            // 
            pictureBox14.Image = (Image)resources.GetObject("pictureBox14.Image");
            pictureBox14.Location = new Point(-1, 204);
            pictureBox14.Name = "pictureBox14";
            pictureBox14.Size = new Size(32, 147);
            pictureBox14.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox14.TabIndex = 46;
            pictureBox14.TabStop = false;
            // 
            // panel4
            // 
            panel4.BackColor = Color.White;
            panel4.Controls.Add(textBox12);
            panel4.Location = new Point(88, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(391, 75);
            panel4.TabIndex = 45;
            // 
            // textBox12
            // 
            textBox12.BorderStyle = BorderStyle.None;
            textBox12.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox12.Location = new Point(38, 27);
            textBox12.Multiline = true;
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(288, 31);
            textBox12.TabIndex = 0;
            textBox12.Text = "AERODOME ENTRY PERMIT";
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(2, 3);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(86, 74);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 44;
            pictureBox6.TabStop = false;
            // 
            // panel2
            // 
            panel2.Location = new Point(51, 75);
            panel2.Name = "panel2";
            panel2.Size = new Size(250, 125);
            panel2.TabIndex = 1;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(43, 67);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(127, 84);
            pictureBox4.TabIndex = 0;
            pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(239, -1);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1052, 80);
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(868, 719);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(357, 95);
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.White;
            label10.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(1242, 9);
            label10.Name = "label10";
            label10.Size = new Size(34, 38);
            label10.TabIndex = 54;
            label10.Text = "X";
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1288, 833);
            Controls.Add(watchpreview);
            Controls.Add(label10);
            Controls.Add(pictureBox3);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox2);
            Controls.Add(Labour_infobox);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form5";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form5";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            Labour_infobox.ResumeLayout(false);
            Labour_infobox.PerformLayout();
            groupBox1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)photopass).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void labourrenew_TextChanged(object sender, EventArgs e)
        {
            string aadhaarNumber = labourrenew.Text.Trim();

            // Validate if the Aadhaar number is 12 digits and numeric
            if (aadhaarNumber.Length == 12 && aadhaarNumber.All(char.IsDigit))
            {
                // Check if the entered Aadhaar number is blacklisted
                CheckIfBlacklisted(aadhaarNumber);
            }
            else
            {
                // If the Aadhaar number is invalid, you can clear any previous alerts or messages
                // Optional: Clear message or reset if necessary
            }
        }
            private void CheckIfBlacklisted(string aadhaarNumber)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\RISHI\\Documents\\Eyegate.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"))
                {
                    string query = @"SELECT BanReason 
                             FROM BlacklistedAadhaar 
                             WHERE AadhaarNumber = @AadhaarNumber";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AadhaarNumber", aadhaarNumber);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // If the person is blacklisted, show the reason
                                string banReason = reader["BanReason"].ToString();

                                // Show the message with the reason
                                MessageBox.Show($"⚠️ BLACKLISTED PERSON ⚠️\n\n" +
                                                $"Aadhaar Number: {aadhaarNumber}\n" +
                                                $"Reason: {banReason}\n\n" +
                                                "This person cannot be renewed or registered.",
                                                "Blacklist Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                // If the person is not blacklisted, you can choose to do nothing or clear messages
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking blacklist: {ex.Message}",
                                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void arearenew_TextChanged(object sender, EventArgs e)
        {
        }

        private void dobrenew_ValueChanged(object sender, EventArgs e)
        {
        }

        private void outdatetime_ValueChanged(object sender, EventArgs e)
        {
        }

        private void entrytime_ValueChanged(object sender, EventArgs e)
        {
        }

        private void outdaterenew_ValueChanged(object sender, EventArgs e)
        {
        }

        private void indate_ValueChanged(object sender, EventArgs e)
        {
        }

        private void contractorrenew_TextChanged(object sender, EventArgs e)
        {
        }

        private void fullnamerenew_TextChanged(object sender, EventArgs e)
        {
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private GroupBox Labour_infobox;
        private Label label7;
        private TextBox gaterenew;
        private Label label5;
        private TextBox fullnamerenew;
        private Label label4;
        private Label label3;
        private Label label2;
        private DateTimePicker outdaterenew;
        private DateTimePicker dobrenew;
        private Label label6;
        private Label label8;
        private DateTimePicker outdatetime;
        private DateTimePicker entrytime;
        private GroupBox groupBox1;
        private PictureBox pictureBox2;
        private Button button1;
        private Button savebtn;
        private Button clearbtn;
        private PictureBox pictureBox3;
        private Label label10;
        private PictureBox pictureBox13;
        private Label label16;
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
        private TextBox arearenew;
        private Label label11;
        private Panel panel3;
        private Panel panel2;
        private PictureBox pictureBox4;
        private TextBox labourrenew;
        private Label label1;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private Label label24;
        private PictureBox pictureBox6;
        private Panel panel4;
        private TextBox textBox12;
        private PictureBox pictureBox14;
        private TextBox textBox13;
        private TextBox namepass;
        private PictureBox photopass;
        private TextBox contractorrenew;
        private Label label12;
        private Label label9;
        private TextBox labouridpass;
        private TextBox contractorpass;
        private TextBox textBox8;
        private Label label13;
        private TextBox dobpass;
        private TextBox textBox10;
        private Label label25;
        private Label label23;
        private TextBox todaypass;
        private TextBox gatespass;
        private TextBox areaspass;
        private Label label27;
        private Label label26;
        private TextBox textBox15;
        private Button watchpreview;
        private DateTimePicker indate;
        private TextBox fromdaypass;
        private TextBox totimepasscial;
        private TextBox textBox1;
        private PictureBox pictureBox11;
        private Label label17;

        public EventHandler gaterenew_TextChanged { get; private set; }
    }
}