using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;



namespace Entry_Pass_Generator_CIAL
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                textBox2.UseSystemPasswordChar = true;
                
            }
            else
            {
                // Hide password
                textBox2.UseSystemPasswordChar = false;
                checkBox1.Text = "Show Password";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string userInput = textBox1.Text;
        }

        private void loginbox_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;  // Get username from textBox1
            string password = textBox2.Text;  // Get password from textBox2

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\RISHI\\Documents\\Eyegate.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
            string query = "SELECT COUNT(*) FROM userslogin WHERE username = @username AND password = @password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int userCount = (int)cmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            Form4 form4 = new Form4();
                            form4.Show();
                            this.Hide();
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("User with the credentials not found. Would you like to register yourself as a new account?", "User Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (result == DialogResult.Yes)
                            {
                                RegisterNewUser(username, password);
                            }
                            else
                            {
                                MessageBox.Show("Login failed.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RegisterNewUser(string username, string password)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\RISHI\\Documents\\Eyegate.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
            string insertQuery = "INSERT INTO userslogin (username, password) VALUES (@username, @password)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User registered successfully!", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Form4 form4 = new Form4();
                            form4.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Error: User registration failed.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
