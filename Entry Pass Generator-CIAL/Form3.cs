using Microsoft.Data.SqlClient;
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

namespace Entry_Pass_Generator_CIAL
{
    public partial class Form3 : Form
    {
        private ApiClient apiClient;

        public Form3()
        {
            InitializeComponent();
            apiClient = new ApiClient("http://localhost:5135");
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

        private async void loginbox_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                loginbox.Enabled = false;
                loginbox.Text = "Logging in...";

                var response = await apiClient.LoginAsync(username, password);

                if (response.Success)
                {
                    MessageBox.Show($"Welcome {response.Username}!", "Login Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form4 form4 = new Form4();
                    form4.Show();
                    this.Hide();
                }
                else
                {
                    DialogResult result = MessageBox.Show(
                        "User with the credentials not found. Would you like to register yourself as a new account?",
                        "User Not Found",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        await RegisterNewUser(username, password);
                    }
                    else
                    {
                        MessageBox.Show("Login failed.", "Login Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Cannot connect to authentication server!\n\n" +
                    "Make sure the API is running on http://localhost:5135\n\n" +
                    "Error: " + ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                loginbox.Enabled = true;
                loginbox.Text = "Login";
            }
        }

        private async Task RegisterNewUser(string username, string password)
        {
            try
            {
                var response = await apiClient.RegisterAsync(username, password);

                if (response.Success)
                {
                    MessageBox.Show("User registered successfully!", "Registration Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form4 form4 = new Form4();
                    form4.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error: User registration failed.\n\n" + response.Message,
                        "Registration Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Registration Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}