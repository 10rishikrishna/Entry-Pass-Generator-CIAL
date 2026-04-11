using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace Entry_Pass_Generator_CIAL
{
    public partial class Form8 : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\RISHI\Documents\Eyegate.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        private byte[] aadharImageData = null;
        private string currentLaborID = "";
        private string currentFullName = "";
        private string currentAadharNumber = "";
        private string currentContactNumber = "";
        private bool isBlacklisted = false;

        public Form8()
        {
            InitializeComponent();
            picturesearch.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // pictureBox3 click - Search button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string searchAadharNumber = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchAadharNumber))
            {
                MessageBox.Show("Please enter an Aadhar number to search!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            searchAadharNumber = new string(searchAadharNumber.Where(char.IsDigit).ToArray());

            if (searchAadharNumber.Length != 12)
            {
                MessageBox.Show("Aadhar number must be exactly 12 digits!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            SearchAndPopulateByAadhar(searchAadharNumber);
        }

        private void SearchAndPopulateByAadhar(string aadharNumber)
        {
            ClearAllFields();

            // First, check if this Aadhar number is blacklisted
            if (CheckIfBlacklisted(aadharNumber))
            {
                return; // Stop here if blacklisted
            }

            // If not blacklisted, proceed with normal search
            string query = @"SELECT LaborID, AadharNumber, FirstName, LastName, ContractorName, 
                            FatherMotherName, StateName, AddressName, ContactNumber, 
                            LaborImage, AadharImage
                            FROM LaborRegistration 
                            WHERE AadharNumber = @AadharNumber";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AadharNumber", aadharNumber);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Store current employee details
                                currentLaborID = reader["LaborID"]?.ToString() ?? "";
                                currentAadharNumber = reader["AadharNumber"]?.ToString() ?? "";
                                string firstName = reader["FirstName"]?.ToString() ?? "";
                                string lastName = reader["LastName"]?.ToString() ?? "";
                                currentFullName = $"{firstName} {lastName}".Trim();
                                currentContactNumber = reader["ContactNumber"]?.ToString() ?? "";

                                // Populate form fields
                                employeesearch.Text = currentLaborID;
                                aadharnosearch.Text = currentAadharNumber;
                                fnamesearch.Text = firstName;
                                lnamesearch.Text = lastName;
                                contractorsearch.Text = reader["ContractorName"]?.ToString() ?? "";
                                fathernamesearch.Text = reader["FatherMotherName"]?.ToString() ?? "";
                                statesearch.Text = reader["StateName"]?.ToString() ?? "";
                                addresssearch.Text = reader["AddressName"]?.ToString() ?? "";
                                contactsearch.Text = currentContactNumber;

                                // Load employee photo
                                byte[] laborImageData = reader["LaborImage"] as byte[];
                                if (laborImageData != null && laborImageData.Length > 0)
                                {
                                    using (MemoryStream ms = new MemoryStream(laborImageData))
                                    {
                                        if (picturesearch.Image != null)
                                        {
                                            picturesearch.Image.Dispose();
                                        }

                                        picturesearch.Image = Image.FromStream(ms);
                                        picturesearch.SizeMode = PictureBoxSizeMode.StretchImage;
                                    }
                                }

                                // Store Aadhar image
                                aadharImageData = reader["AadharImage"] as byte[];

                                // Clear search box
                                textBox1.Clear();

                                // Show success message
                                MessageBox.Show(
                                    $"✅ Employee Found\n\n" +
                                    $"Labor ID: {currentLaborID}\n" +
                                    $"Name: {currentFullName}\n" +
                                    $"Aadhar: {currentAadharNumber}\n" +
                                    $"Contact: {currentContactNumber}\n\n" +
                                    $"Status: Active (Not Blacklisted)",
                                    "Search Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(
                                    $"No employee found with Aadhar number: {aadharNumber}",
                                    "Not Found",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                ClearAllFields();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Check if employee is blacklisted
        private bool CheckIfBlacklisted(string aadharNumber)
        {
            string query = @"SELECT LaborID, FullName, Reason, BlacklistedBy, BlacklistedDate, ContactNumber
                            FROM LaborBlacklist 
                            WHERE AadharNumber = @AadharNumber";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AadharNumber", aadharNumber);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Employee is blacklisted
                                string laborID = reader["LaborID"]?.ToString() ?? "";
                                string fullName = reader["FullName"]?.ToString() ?? "";
                                string reason = reader["Reason"]?.ToString() ?? "";
                                string blacklistedBy = reader["BlacklistedBy"]?.ToString() ?? "";
                                DateTime blacklistedDate = reader["BlacklistedDate"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["BlacklistedDate"])
                                    : DateTime.MinValue;

                                // Show blacklist warning
                                ShowBlacklistWarning(laborID, fullName, aadharNumber, reason, blacklistedBy, blacklistedDate);

                                textBox1.Clear();
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking blacklist: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        // Show professional blacklist warning
        private void ShowBlacklistWarning(string laborID, string fullName, string aadhar,
                                         string reason, string blacklistedBy, DateTime blacklistedDate)
        {
            Form warningForm = new Form
            {
                Text = "",
                Size = new Size(600, 500),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(41, 41, 41)
            };

            // Red header
            Panel headerPanel = new Panel
            {
                Size = new Size(600, 100),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(192, 57, 43)
            };

            Label iconLabel = new Label
            {
                Text = "⛔",
                Font = new Font("Segoe UI", 48, FontStyle.Bold),
                Size = new Size(100, 90),
                Location = new Point(10, 5),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label titleLabel = new Label
            {
                Text = "BLACKLISTED EMPLOYEE",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                Size = new Size(480, 40),
                Location = new Point(110, 20),
                ForeColor = Color.White
            };

            Label subtitleLabel = new Label
            {
                Text = "⚠️ ACCESS DENIED - DO NOT ISSUE PASS",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(480, 30),
                Location = new Point(110, 60),
                ForeColor = Color.FromArgb(255, 220, 220)
            };

            headerPanel.Controls.Add(iconLabel);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(subtitleLabel);

            // Content panel
            Panel contentPanel = new Panel
            {
                Size = new Size(560, 300),
                Location = new Point(20, 120),
                BackColor = Color.White
            };

            Label detailsLabel = new Label
            {
                Text = $"Labor ID: {laborID}\n" +
                       $"Name: {fullName}\n" +
                       $"Aadhar: {aadhar}\n\n" +
                       $"Blacklisted By: {blacklistedBy}\n" +
                       $"Date: {blacklistedDate:dd-MMM-yyyy HH:mm:ss}",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Size = new Size(520, 120),
                Location = new Point(20, 20),
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            Label reasonHeaderLabel = new Label
            {
                Text = "REASON FOR BLACKLIST:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(520, 25),
                Location = new Point(20, 150),
                ForeColor = Color.FromArgb(192, 57, 43)
            };

            TextBox reasonTextBox = new TextBox
            {
                Text = reason,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Size = new Size(520, 100),
                Location = new Point(20, 180),
                Multiline = true,
                ReadOnly = true,
                BackColor = Color.FromArgb(255, 235, 235),
                ForeColor = Color.FromArgb(192, 57, 43),
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Vertical
            };

            contentPanel.Controls.Add(detailsLabel);
            contentPanel.Controls.Add(reasonHeaderLabel);
            contentPanel.Controls.Add(reasonTextBox);

            // Close button
            Button closeButton = new Button
            {
                Text = "CLOSE",
                Size = new Size(200, 45),
                Location = new Point(200, 440),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, args) => warningForm.Close();

            warningForm.Controls.Add(headerPanel);
            warningForm.Controls.Add(contentPanel);
            warningForm.Controls.Add(closeButton);

            // Sound alert
            System.Media.SystemSounds.Hand.Play();

            warningForm.ShowDialog(this);
        }

        // Blacklist button click - Professional popup
        private void blacklister_Click(object sender, EventArgs e)
        {
            // Validate that an employee is loaded
            if (string.IsNullOrWhiteSpace(currentLaborID))
            {
                MessageBox.Show(
                    "Please search for an employee first before blacklisting!",
                    "No Employee Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Check if already blacklisted - USING BEAUTIFUL DIALOG
            if (IsEmployeeBlacklisted(currentLaborID))
            {
                ShowBeautifulBlacklistStatusDialog(currentLaborID, currentFullName);
                return;
            }

            // Show confirmation
            DialogResult confirmResult = MessageBox.Show(
                $"⚠️ BLACKLIST CONFIRMATION ⚠️\n\n" +
                $"Are you sure you want to BLACKLIST this employee?\n\n" +
                $"Labor ID: {currentLaborID}\n" +
                $"Name: {currentFullName}\n" +
                $"Aadhar: {currentAadharNumber}\n\n" +
                $"This action will:\n" +
                $"• Permanently ban them from entry\n" +
                $"• Prevent any pass generation\n" +
                $"• Require supervisor approval to remove\n\n" +
                $"Do you want to proceed?",
                "⛔ Confirm Blacklist",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                ShowBlacklistReasonDialog();
            }
        }

        // ============================================
        // BEAUTIFUL BLACKLIST STATUS DIALOG
        // ============================================
        private void ShowBeautifulBlacklistStatusDialog(string laborId, string fullName)
        {
            Form dialog = new Form
            {
                Size = new Size(500, 380),
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White
            };

            // Add shadow effect border
            dialog.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, dialog.Width - 1, dialog.Height - 1);
                }
            };

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(220, 53, 69)
            };

            // Warning Icon
            PictureBox warningIcon = new PictureBox
            {
                Size = new Size(60, 60),
                Location = new Point(220, 20),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Create warning icon
            Bitmap warningBitmap = new Bitmap(60, 60);
            using (Graphics g = Graphics.FromImage(warningBitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(Brushes.White, 5, 5, 50, 50);
                using (Pen redPen = new Pen(Color.FromArgb(220, 53, 69), 4))
                {
                    redPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    redPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawLine(redPen, 20, 20, 40, 40);
                    g.DrawLine(redPen, 40, 20, 20, 40);
                }
            }
            warningIcon.Image = warningBitmap;
            headerPanel.Controls.Add(warningIcon);

            // Title Label
            Label titleLabel = new Label
            {
                Text = "EMPLOYEE BLACKLISTED",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 35,
                Padding = new Padding(0, 0, 0, 10)
            };
            headerPanel.Controls.Add(titleLabel);

            // Content Panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(40, 30, 40, 20)
            };

            // Status Label
            Label statusLabel = new Label
            {
                Text = "⚠️ This employee is already on the blacklist",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = false,
                Size = new Size(420, 30),
                Location = new Point(40, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            contentPanel.Controls.Add(statusLabel);

            // Info Box Panel
            Panel infoBox = new Panel
            {
                Location = new Point(40, 60),
                Size = new Size(420, 120),
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20)
            };

            infoBox.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(220, 53, 69), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, infoBox.Width - 1, infoBox.Height - 1);
                }
            };

            Label laborIdLabel = new Label
            {
                Text = "Labor ID:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(20, 15),
                AutoSize = true
            };
            infoBox.Controls.Add(laborIdLabel);

            Label laborIdValue = new Label
            {
                Text = laborId,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(120, 15),
                AutoSize = true
            };
            infoBox.Controls.Add(laborIdValue);

            Label nameLabel = new Label
            {
                Text = "Full Name:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(20, 50),
                AutoSize = true
            };
            infoBox.Controls.Add(nameLabel);

            Label nameValue = new Label
            {
                Text = fullName,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(120, 50),
                AutoSize = true
            };
            infoBox.Controls.Add(nameValue);

            Label statusBadge = new Label
            {
                Text = "🚫 BLACKLISTED",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(220, 53, 69),
                AutoSize = true,
                Padding = new Padding(15, 5, 15, 5),
                Location = new Point(20, 85)
            };
            infoBox.Controls.Add(statusBadge);

            contentPanel.Controls.Add(infoBox);

            // Footer Panel
            Panel footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(40, 15, 40, 15)
            };

            // OK Button
            Button okButton = new Button
            {
                Text = "OK, I Understand",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Size = new Size(420, 50),
                Location = new Point(40, 15),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            okButton.FlatAppearance.BorderSize = 0;
            okButton.Click += (s, e) => dialog.Close();

            // Hover effects
            okButton.MouseEnter += (s, e) => okButton.BackColor = Color.FromArgb(200, 35, 51);
            okButton.MouseLeave += (s, e) => okButton.BackColor = Color.FromArgb(220, 53, 69);

            footerPanel.Controls.Add(okButton);

            // Add all panels to form
            dialog.Controls.Add(contentPanel);
            dialog.Controls.Add(headerPanel);
            dialog.Controls.Add(footerPanel);

            // Rounded corners
            dialog.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, dialog.Width, dialog.Height, 10, 10));

            dialog.ShowDialog(this);
        }

        // Professional blacklist reason dialog
        private void ShowBlacklistReasonDialog()
        {
            Form reasonForm = new Form
            {
                Text = "",
                Size = new Size(650, 550),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(236, 240, 241)
            };

            // Header
            Panel headerPanel = new Panel
            {
                Size = new Size(650, 80),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(231, 76, 60)
            };

            Label headerIcon = new Label
            {
                Text = "⛔",
                Font = new Font("Segoe UI", 36, FontStyle.Bold),
                Size = new Size(70, 70),
                Location = new Point(15, 5),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label headerTitle = new Label
            {
                Text = "BLACKLIST EMPLOYEE",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Size = new Size(550, 80),
                Location = new Point(90, 0),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            headerPanel.Controls.Add(headerIcon);
            headerPanel.Controls.Add(headerTitle);

            // Employee details panel
            Panel detailsPanel = new Panel
            {
                Size = new Size(610, 100),
                Location = new Point(20, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label detailsLabel = new Label
            {
                Text = $"Labor ID: {currentLaborID}\n" +
                       $"Name: {currentFullName}\n" +
                       $"Aadhar: {currentAadharNumber}\n" +
                       $"Contact: {currentContactNumber}",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Size = new Size(580, 90),
                Location = new Point(15, 5),
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            detailsPanel.Controls.Add(detailsLabel);

            // Reason section
            Label reasonLabel = new Label
            {
                Text = "Reason for Blacklisting: *",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(610, 25),
                Location = new Point(20, 220),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            Label instructionLabel = new Label
            {
                Text = "Please provide a detailed reason (minimum 20 characters). This will be permanently recorded.",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                Size = new Size(610, 30),
                Location = new Point(20, 245),
                ForeColor = Color.FromArgb(127, 140, 141)
            };

            TextBox reasonTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(610, 120),
                Location = new Point(20, 280),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                MaxLength = 500
            };

            Label charCountLabel = new Label
            {
                Text = "0 / 500 characters",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Size = new Size(610, 20),
                Location = new Point(20, 405),
                ForeColor = Color.FromArgb(149, 165, 166),
                TextAlign = ContentAlignment.MiddleRight
            };

            reasonTextBox.TextChanged += (s, e) =>
            {
                int length = reasonTextBox.Text.Length;
                charCountLabel.Text = $"{length} / 500 characters";
                charCountLabel.ForeColor = length < 20 ? Color.FromArgb(231, 76, 60) : Color.FromArgb(39, 174, 96);
            };

            // Buttons
            Button submitButton = new Button
            {
                Text = "⛔ BLACKLIST EMPLOYEE",
                Size = new Size(280, 50),
                Location = new Point(70, 450),
                BackColor = Color.FromArgb(192, 57, 43),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            submitButton.FlatAppearance.BorderSize = 0;

            Button cancelButton = new Button
            {
                Text = "CANCEL",
                Size = new Size(230, 50),
                Location = new Point(360, 450),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            cancelButton.FlatAppearance.BorderSize = 0;
            cancelButton.Click += (s, args) => reasonForm.Close();

            submitButton.Click += (s, args) =>
            {
                string reason = reasonTextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(reason))
                {
                    MessageBox.Show("Please provide a reason for blacklisting!",
                        "Reason Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    reasonTextBox.Focus();
                    return;
                }

                if (reason.Length < 20)
                {
                    MessageBox.Show("Reason must be at least 20 characters long!\n\nPlease provide more details.",
                        "Reason Too Short", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    reasonTextBox.Focus();
                    return;
                }

                // Add to blacklist
                if (AddToBlacklist(reason))
                {
                    reasonForm.Close();
                }
            };

            reasonForm.Controls.Add(headerPanel);
            reasonForm.Controls.Add(detailsPanel);
            reasonForm.Controls.Add(reasonLabel);
            reasonForm.Controls.Add(instructionLabel);
            reasonForm.Controls.Add(reasonTextBox);
            reasonForm.Controls.Add(charCountLabel);
            reasonForm.Controls.Add(submitButton);
            reasonForm.Controls.Add(cancelButton);

            reasonForm.ShowDialog(this);
        }

        // Check if employee is already blacklisted
        private bool IsEmployeeBlacklisted(string laborID)
        {
            string query = "SELECT COUNT(*) FROM LaborBlacklist WHERE LaborID = @LaborID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LaborID", laborID);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking blacklist status: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Add employee to blacklist
        private bool AddToBlacklist(string reason)
        {
            string insertQuery = @"INSERT INTO LaborBlacklist 
                                  (LaborID, FullName, Reason, BlacklistedBy, BlacklistedDate, AadharNumber, ContactNumber)
                                  VALUES (@LaborID, @FullName, @Reason, @BlacklistedBy, GETDATE(), @AadharNumber, @ContactNumber)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@LaborID", currentLaborID);
                        cmd.Parameters.AddWithValue("@FullName", currentFullName);
                        cmd.Parameters.AddWithValue("@Reason", reason);
                        cmd.Parameters.AddWithValue("@BlacklistedBy", "Security Admin");
                        cmd.Parameters.AddWithValue("@AadharNumber", currentAadharNumber);
                        cmd.Parameters.AddWithValue("@ContactNumber", currentContactNumber);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            $"✅ EMPLOYEE BLACKLISTED SUCCESSFULLY\n\n" +
                            $"Labor ID: {currentLaborID}\n" +
                            $"Name: {currentFullName}\n\n" +
                            $"Reason: {reason}\n\n" +
                            $"⛔ This employee is now permanently banned from entry.\n" +
                            $"All pass generation requests will be automatically denied.",
                            "Blacklist Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        ClearAllFields();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding to blacklist: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // button1 click - Aadhar preview
        private void button1_Click(object sender, EventArgs e)
        {
            if (aadharImageData == null || aadharImageData.Length == 0)
            {
                MessageBox.Show("No Aadhar image available.\n\nPlease search for an employee first.",
                    "No Image", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Form previewForm = new Form
            {
                Text = "",
                Size = new Size(1000, 750),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(236, 240, 241)
            };

            Point dragCursorPoint = Point.Empty;
            Point dragFormPoint = Point.Empty;

            previewForm.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, previewForm.Width, previewForm.Height, 20, 20));

            Panel headerPanel = new Panel
            {
                Size = new Size(1000, 80),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(44, 62, 80)
            };

            Label govLabel = new Label
            {
                Text = "🇮🇳",
                Font = new Font("Segoe UI", 32, FontStyle.Regular),
                Size = new Size(80, 70),
                Location = new Point(20, 5),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label titleLabel = new Label
            {
                Text = "AADHAAR CARD",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                Size = new Size(700, 35),
                Location = new Point(110, 5),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label subtitleLabel = new Label
            {
                Text = $"Name: {fnamesearch.Text} {lnamesearch.Text}  |  Aadhaar: {aadharnosearch.Text}",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Size = new Size(700, 25),
                Location = new Point(110, 40),
                ForeColor = Color.FromArgb(189, 195, 199),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Button closeHeaderBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Location = new Point(940, 20),
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            closeHeaderBtn.FlatAppearance.BorderSize = 0;
            closeHeaderBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            closeHeaderBtn.Click += (s, args) => previewForm.Close();

            headerPanel.Controls.Add(govLabel);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(subtitleLabel);
            headerPanel.Controls.Add(closeHeaderBtn);

            Panel contentPanel = new Panel
            {
                Size = new Size(960, 580),
                Location = new Point(20, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            contentPanel.Paint += (s, pe) =>
            {
                ControlPaint.DrawBorder(pe.Graphics, contentPanel.ClientRectangle,
                    Color.FromArgb(189, 195, 199), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(189, 195, 199), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(189, 195, 199), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(189, 195, 199), 1, ButtonBorderStyle.Solid);
            };

            Panel imageContainer = new Panel
            {
                Size = new Size(920, 480),
                Location = new Point(20, 20),
                BackColor = Color.FromArgb(245, 246, 247),
                BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox previewPictureBox = new PictureBox
            {
                Size = new Size(900, 460),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            using (MemoryStream ms = new MemoryStream(aadharImageData))
            {
                previewPictureBox.Image = Image.FromStream(ms);
            }

            imageContainer.Controls.Add(previewPictureBox);
            contentPanel.Controls.Add(imageContainer);

            Panel buttonPanel = new Panel
            {
                Size = new Size(920, 60),
                Location = new Point(20, 510),
                BackColor = Color.Transparent
            };

            Button downloadButton = new Button
            {
                Text = "📥  DOWNLOAD AADHAAR",
                Size = new Size(280, 50),
                Location = new Point(200, 5),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            downloadButton.FlatAppearance.BorderSize = 0;
            downloadButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96);
            downloadButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(34, 153, 84);
            downloadButton.Click += (s, args) => SaveAadharImage();

            Button printButton = new Button
            {
                Text = "🖨️  PRINT",
                Size = new Size(150, 50),
                Location = new Point(500, 5),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            printButton.FlatAppearance.BorderSize = 0;
            printButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            printButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 113, 163);
            printButton.Click += (s, args) =>
            {
                MessageBox.Show("Print functionality can be added here!", "Print",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            Button closeButton = new Button
            {
                Text = "CLOSE",
                Size = new Size(150, 50),
                Location = new Point(670, 5),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(127, 140, 141);
            closeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(108, 122, 137);
            closeButton.Click += (s, args) => previewForm.Close();

            buttonPanel.Controls.Add(downloadButton);
            buttonPanel.Controls.Add(printButton);
            buttonPanel.Controls.Add(closeButton);
            contentPanel.Controls.Add(buttonPanel);

            Label footerLabel = new Label
            {
                Text = "This is a digitally stored copy of the Aadhaar card. Handle with care and maintain confidentiality.",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                Size = new Size(960, 30),
                Location = new Point(20, 700),
                ForeColor = Color.FromArgb(127, 140, 141),
                TextAlign = ContentAlignment.MiddleCenter
            };

            previewForm.Controls.Add(headerPanel);
            previewForm.Controls.Add(contentPanel);
            previewForm.Controls.Add(footerLabel);

            bool dragging = false;

            headerPanel.MouseDown += (s, e) =>
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = previewForm.Location;
            };

            headerPanel.MouseMove += (s, e) =>
            {
                if (dragging)
                {
                    Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    previewForm.Location = Point.Add(dragFormPoint, new Size(diff));
                }
            };

            headerPanel.MouseUp += (s, e) => { dragging = false; };

            previewForm.KeyPreview = true;
            previewForm.KeyDown += (s, ke) =>
            {
                if (ke.KeyCode == Keys.Escape)
                    previewForm.Close();
            };

            previewForm.ShowDialog(this);
        }

        // Save Aadhar image
        private void SaveAadharImage()
        {
            if (aadharImageData == null || aadharImageData.Length == 0)
            {
                MessageBox.Show("No Aadhar image to save!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png|JPEG Image|*.jpg|All Files|*.*",
                FileName = $"Aadhar_{aadharnosearch.Text}_{fnamesearch.Text}_{lnamesearch.Text}_{DateTime.Now:yyyyMMdd_HHmmss}",
                DefaultExt = "png",
                Title = "Save Aadhar Image"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(aadharImageData))
                    using (Image image = Image.FromStream(ms))
                    {
                        string extension = Path.GetExtension(saveDialog.FileName).ToLower();

                        if (extension == ".png")
                        {
                            image.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        }
                        else if (extension == ".jpg" || extension == ".jpeg")
                        {
                            image.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        else
                        {
                            image.Save(saveDialog.FileName);
                        }
                    }

                    MessageBox.Show(
                        $"Aadhar image saved successfully!\n\nLocation:\n{saveDialog.FileName}",
                        "Download Complete",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{saveDialog.FileName}\"");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving image: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Clear all fields
        private void ClearAllFields()
        {
            textBox1.Clear();
            employeesearch.Clear();
            aadharnosearch.Clear();
            fnamesearch.Clear();
            lnamesearch.Clear();
            contractorsearch.Clear();
            fathernamesearch.Clear();
            statesearch.Clear();
            addresssearch.Clear();
            contactsearch.Clear();

            if (picturesearch.Image != null)
            {
                picturesearch.Image.Dispose();
                picturesearch.Image = null;
            }

            aadharImageData = null;
            currentLaborID = "";
            currentFullName = "";
            currentAadharNumber = "";
            currentContactNumber = "";
            isBlacklisted = false;

            employeesearch.BackColor = Color.White;
            fnamesearch.BackColor = Color.White;
            lnamesearch.BackColor = Color.White;
        }

        // Validation helpers
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = new string(textBox1.Text.Where(char.IsDigit).ToArray());

            if (text.Length > 12)
            {
                text = text.Substring(0, 12);
            }

            if (textBox1.Text != text)
            {
                int cursorPosition = textBox1.SelectionStart;
                textBox1.Text = text;
                textBox1.SelectionStart = Math.Min(cursorPosition, text.Length);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                pictureBox3_Click(sender, e);
            }
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        private void blacklister_Click_1(object sender, EventArgs e)
        {
            // Validate that an employee is loaded
            if (string.IsNullOrWhiteSpace(currentLaborID))
            {
                MessageBox.Show(
                    "Please search for an employee first before blacklisting!",
                    "No Employee Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Check if already blacklisted - USING BEAUTIFUL DIALOG
            if (IsEmployeeBlacklisted(currentLaborID))
            {
                ShowBeautifulBlacklistStatusDialog(currentLaborID, currentFullName);
                return;
            }

            // Show confirmation
            DialogResult confirmResult = MessageBox.Show(
                $"⚠️ BLACKLIST CONFIRMATION ⚠️\n\n" +
                $"Are you sure you want to BLACKLIST this employee?\n\n" +
                $"Labor ID: {currentLaborID}\n" +
                $"Name: {currentFullName}\n" +
                $"Aadhar: {currentAadharNumber}\n\n" +
                $"This action will:\n" +
                $"• Permanently ban them from entry\n" +
                $"• Prevent any pass generation\n" +
                $"• Require supervisor approval to remove\n\n" +
                $"Do you want to proceed?",
                "⛔ Confirm Blacklist",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                ShowBlacklistReasonDialog();
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void contactsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lnamesearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validate that an employee is loaded
            if (string.IsNullOrWhiteSpace(currentLaborID))
            {
                MessageBox.Show(
                    "Please search for an employee first before saving changes!",
                    "No Employee Loaded",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(fnamesearch.Text))
            {
                MessageBox.Show("First Name is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fnamesearch.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(lnamesearch.Text))
            {
                MessageBox.Show("Last Name is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lnamesearch.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(contactsearch.Text))
            {
                MessageBox.Show("Contact Number is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                contactsearch.Focus();
                return;
            }

            if (contactsearch.Text.Length != 10)
            {
                MessageBox.Show("Contact number must be exactly 10 digits!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                contactsearch.Focus();
                return;
            }

            // Confirm save
            DialogResult confirmResult = MessageBox.Show(
                $"Save changes for this employee?\n\n" +
                $"Labor ID: {currentLaborID}\n" +
                $"Name: {fnamesearch.Text} {lnamesearch.Text}\n\n" +
                $"This will update the employee information in the database.",
                "Confirm Save",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            // Update query
            string updateQuery = @"UPDATE LaborRegistration 
                          SET FirstName = @FirstName,
                              LastName = @LastName,
                              ContractorName = @ContractorName,
                              FatherMotherName = @FatherMotherName,
                              StateName = @StateName,
                              AddressName = @AddressName,
                              ContactNumber = @ContactNumber,
                              UpdatedAt = GETDATE()
                          WHERE LaborID = @LaborID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@LaborID", currentLaborID);
                        cmd.Parameters.AddWithValue("@FirstName", fnamesearch.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastName", lnamesearch.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContractorName", contractorsearch.Text.Trim());
                        cmd.Parameters.AddWithValue("@FatherMotherName", fathernamesearch.Text.Trim());
                        cmd.Parameters.AddWithValue("@StateName", statesearch.Text.Trim());
                        cmd.Parameters.AddWithValue("@AddressName", addresssearch.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactNumber", contactsearch.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update current stored values
                            currentFullName = $"{fnamesearch.Text.Trim()} {lnamesearch.Text.Trim()}";
                            currentContactNumber = contactsearch.Text.Trim();

                            MessageBox.Show(
                                $"✅ Employee information updated successfully!\n\n" +
                                $"Labor ID: {currentLaborID}\n" +
                                $"Name: {currentFullName}\n" +
                                $"Contact: {currentContactNumber}\n\n" +
                                $"All changes have been saved to the database.",
                                "Save Successful",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                                "No records were updated. Please try again.",
                                "Update Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error saving changes to database:\n\n{ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void textBox1_TextChanged_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Stop the key from being entered
            }

            // Limit to 12 characters
            if (char.IsDigit(e.KeyChar) && textBox1.Text.Length >= 12)
            {
                e.Handled = true; // Stop entering more digits
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string digitsOnly = new string(textBox1.Text.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length > 12)
            {
                digitsOnly = digitsOnly.Substring(0, 12);
            }

            if (textBox1.Text != digitsOnly)
            {
                int cursorPosition = textBox1.SelectionStart;
                textBox1.Text = digitsOnly;

                textBox1.SelectionStart = Math.Min(cursorPosition, digitsOnly.Length);
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

      
    }
}