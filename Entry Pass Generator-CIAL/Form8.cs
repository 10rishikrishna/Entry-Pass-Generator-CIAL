using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Entry_Pass_Generator_CIAL
{
    public partial class Form8 : Form  // Replace with your actual form name
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\RISHI\Documents\Eyegate.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        private byte[] aadharImageData = null;

        public Form8()
        {
            InitializeComponent();

            // Set PictureBox to stretch image to fit the box
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

            // Remove any non-digit characters
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
                                // Populate Labor ID (Employee ID)
                                employeesearch.Text = reader["LaborID"]?.ToString() ?? "";

                                // Populate Aadhar Number
                                aadharnosearch.Text = reader["AadharNumber"]?.ToString() ?? "";

                                // Populate First Name
                                fnamesearch.Text = reader["FirstName"]?.ToString() ?? "";

                                // Populate Last Name
                                lnamesearch.Text = reader["LastName"]?.ToString() ?? "";

                                // Populate Contractor
                                contractorsearch.Text = reader["ContractorName"]?.ToString() ?? "";

                                // Populate Father/Mother Name
                                fathernamesearch.Text = reader["FatherMotherName"]?.ToString() ?? "";

                                // Populate State
                                statesearch.Text = reader["StateName"]?.ToString() ?? "";

                                // Populate Address
                                addresssearch.Text = reader["AddressName"]?.ToString() ?? "";

                                // Populate Contact Number
                                contactsearch.Text = reader["ContactNumber"]?.ToString() ?? "";

                                // Load Labor Photo into picturesearch with StretchImage mode
                                byte[] laborImageData = reader["LaborImage"] as byte[];
                                if (laborImageData != null && laborImageData.Length > 0)
                                {
                                    using (MemoryStream ms = new MemoryStream(laborImageData))
                                    {
                                        // Dispose previous image if exists
                                        if (picturesearch.Image != null)
                                        {
                                            picturesearch.Image.Dispose();
                                        }

                                        picturesearch.Image = Image.FromStream(ms);
                                        picturesearch.SizeMode = PictureBoxSizeMode.StretchImage; // Ensure stretch mode
                                    }
                                }
                                else
                                {
                                    picturesearch.Image = null;
                                }

                                // Store Aadhar Image for button1 preview
                                aadharImageData = reader["AadharImage"] as byte[];

                                // Clear textBox1 after successful search
                                textBox1.Clear();

                                MessageBox.Show(
                                    $"Employee found!\n\n" +
                                    $"Labor ID: {employeesearch.Text}\n" +
                                    $"Name: {fnamesearch.Text} {lnamesearch.Text}\n" +
                                    $"Aadhar: {aadharnosearch.Text}\n" +
                                    $"Contact: {contactsearch.Text}",
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
                MessageBox.Show($"Database error: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // button1 click - Show Aadhar Card Preview
        // button1 click - Show Aadhar Card Preview with Enhanced UI
        private void button1_Click(object sender, EventArgs e)
        {
            if (aadharImageData == null || aadharImageData.Length == 0)
            {
                MessageBox.Show("No Aadhar image available for this employee.\n\nPlease search for an employee first.",
                    "No Image", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Create preview form with modern design
            Form previewForm = new Form
            {
                Text = "",
                Size = new Size(1000, 750),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(236, 240, 241),
                Padding = new Padding(0)
            };

            // Add drop shadow effect
            previewForm.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, previewForm.Width, previewForm.Height, 20, 20));

            // Top header panel with gradient
            Panel headerPanel = new Panel
            {
                Size = new Size(1000, 80),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(44, 62, 80)
            };

            // Government of India logo placeholder (you can add actual image)
            Label govLabel = new Label
            {
                Text = "🇮🇳",
                Font = new Font("Segoe UI", 32, FontStyle.Regular),
                Size = new Size(80, 70),
                Location = new Point(20, 5),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Title section
            Label titleLabel = new Label
            {
                Text = "AADHAAR CARD",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                Size = new Size(700, 35),
                Location = new Point(110, 5),  // Changed from 10 to 5
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label subtitleLabel = new Label
            {
                Text = $"Name: {fnamesearch.Text} {lnamesearch.Text}  |  Aadhaar: {aadharnosearch.Text}",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Size = new Size(700, 25),
                Location = new Point(110, 47),  // Changed from 45 to 40
                ForeColor = Color.FromArgb(189, 195, 199),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Close button (X) in header
            System.Windows.Forms.Button closeHeaderBtn = new System.Windows.Forms.Button
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

            // Main content panel
            Panel contentPanel = new Panel
            {
                Size = new Size(960, 580),
                Location = new Point(20, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Add subtle shadow to content panel
            contentPanel.Paint += (s, pe) =>
            {
                ControlPaint.DrawBorder(pe.Graphics, contentPanel.ClientRectangle,
                    Color.FromArgb(189, 195, 199), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(189, 195, 199), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(189, 195, 199), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(189, 195, 199), 1, ButtonBorderStyle.Solid);
            };

            // Aadhaar image container
            Panel imageContainer = new Panel
            {
                Size = new Size(920, 480),
                Location = new Point(20, 20),
                BackColor = Color.FromArgb(245, 246, 247),
                BorderStyle = BorderStyle.FixedSingle
            };

            // PictureBox for Aadhar image
            PictureBox previewPictureBox = new PictureBox
            {
                Size = new Size(900, 460),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            // Load image from byte array
            using (MemoryStream ms = new MemoryStream(aadharImageData))
            {
                previewPictureBox.Image = Image.FromStream(ms);
            }

            imageContainer.Controls.Add(previewPictureBox);
            contentPanel.Controls.Add(imageContainer);

            // Action buttons panel
            Panel buttonPanel = new Panel
            {
                Size = new Size(920, 60),
                Location = new Point(20, 510),
                BackColor = Color.Transparent
            };

            // Download button with icon
            System.Windows.Forms.Button downloadButton = new System.Windows.Forms.Button
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

            // Print button (optional)
            System.Windows.Forms.Button printButton = new System.Windows.Forms.Button
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

            // Close button
            System.Windows.Forms.Button closeButton = new System.Windows.Forms.Button
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

            // Footer with info
            Label footerLabel = new Label
            {
                Text = "This is a digitally stored copy of the Aadhaar card. Handle with care and maintain confidentiality.",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                Size = new Size(960, 30),
                Location = new Point(20, 700),
                ForeColor = Color.FromArgb(127, 140, 141),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Add all controls to form
            previewForm.Controls.Add(headerPanel);
            previewForm.Controls.Add(contentPanel);
            previewForm.Controls.Add(footerLabel);

            // Allow form dragging by clicking header
            bool dragging = false;
            Point dragCursorPoint = Point.Empty;
            Point dragFormPoint = Point.Empty;

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

            // Add keyboard shortcut (ESC to close)
            previewForm.KeyPreview = true;
            previewForm.KeyDown += (s, ke) =>
            {
                if (ke.KeyCode == Keys.Escape)
                    previewForm.Close();
            };

            previewForm.ShowDialog(this);
        }

        // Required for rounded corners
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        // Save Aadhar image to disk
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

                    // Open folder where file was saved
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
        }

        // Optional: Add Aadhar number validation on TextChanged for textBox1
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Remove any non-digit characters
            string text = new string(textBox1.Text.Where(char.IsDigit).ToArray());

            // Limit to 12 digits
            if (text.Length > 12)
            {
                text = text.Substring(0, 12);
            }

            // Update textbox if changed
            if (textBox1.Text != text)
            {
                int cursorPosition = textBox1.SelectionStart;
                textBox1.Text = text;
                textBox1.SelectionStart = Math.Min(cursorPosition, text.Length);
            }
        }

        // Optional: Add Aadhar number KeyPress validation for textBox1
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (backspace, delete, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Allow Enter key to trigger search
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                pictureBox3_Click(sender, e);
            }
        }

        // Optional: Clear button functionality
        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }
    }
}