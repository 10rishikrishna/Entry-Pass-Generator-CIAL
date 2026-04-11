using Emgu.CV.CvEnum;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Entry_Pass_Generator_CIAL
{
    public partial class Form7 : Form
    {
        private object textBoxPersonName;
        private object textBoxContractorName;

        public Form7()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        

    

        private void label16_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\RISHI\\Documents\\Eyegate.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        private void btnblacklist_Click(object sender, EventArgs e)
        {
            string aadhaarNumber = textBox1.Text.Trim();

            // Validate if Aadhaar number is 12 digits and numeric
            if (string.IsNullOrEmpty(aadhaarNumber) || aadhaarNumber.Length != 12 || !aadhaarNumber.All(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid 12-digit Aadhaar number!",
                                 "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the reason for blacklisting
            string banReason = textBox2.Text.Trim();

            // Validate that the reason is provided
            if (string.IsNullOrEmpty(banReason))
            {
                MessageBox.Show("Please enter a reason for blacklisting!",
                                 "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Call method to add to blacklist
            AddToBlacklist(aadhaarNumber, banReason);
        }

        private void AddToBlacklist(string aadhaarNumber, string banReason)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if already blacklisted
                    string checkQuery = "SELECT COUNT(*) FROM BlacklistedAadhaar WHERE AadhaarNumber = @AadhaarNumber";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@AadhaarNumber", aadhaarNumber);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("This Aadhaar number is already blacklisted!",
                                           "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insert with all required columns
                    string insertQuery = @"INSERT INTO BlacklistedAadhaar 
                                 (AadhaarNumber, PersonName, ContractorName, BanReason, BlacklistedDate, BlacklistedBy) 
                                 VALUES (@AadhaarNumber, @PersonName, @ContractorName, @BanReason, @BlacklistedDate, @BlacklistedBy)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@AadhaarNumber", aadhaarNumber);
                        cmd.Parameters.AddWithValue("@PersonName", "Unknown");  // Default value
                        cmd.Parameters.AddWithValue("@ContractorName", "N/A");  // Default value
                        cmd.Parameters.AddWithValue("@BanReason", banReason);
                        cmd.Parameters.AddWithValue("@BlacklistedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@BlacklistedBy", "Admin");  // Or use current logged-in user

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Aadhaar number successfully added to the blacklist!",
                                       "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear input fields
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Focus();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}",
                               "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}