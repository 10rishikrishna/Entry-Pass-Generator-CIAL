using Microsoft.Data.SqlClient;
using System;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Entry_Pass_Generator_CIAL
{
    public partial class Form5 : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\RISHI\Documents\Eyegate.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        public Form5()
        {
            InitializeComponent();
            SetupLabourIDTextBox();
        }

        private void SetupLabourIDTextBox()
        {
            // Remove any existing handlers first
            labourrenew.KeyDown -= labourrenew_KeyDown;
            labourrenew.KeyPress -= labourrenew_KeyPress;

            // Add handlers
            labourrenew.KeyDown += labourrenew_KeyDown;
            labourrenew.KeyPress += labourrenew_KeyPress;
        }

        private void labourrenew_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                ProcessLabourID();
            }
        }

        private void labourrenew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                ProcessLabourID();
            }
        }

        private void ProcessLabourID()
        {
            string labourID = labourrenew.Text.Trim();

            if (string.IsNullOrEmpty(labourID))
            {
                MessageBox.Show("Please enter a Labour ID.");
                return;
            }

            LoadLabourDetails(labourID);
        }

        private void LoadLabourDetails(string labourID)
        {
            // Clear previous data first
            ClearAllFieldsExceptID();

            // First check LaborRenewal table (most recent data)
            bool foundInRenewal = LoadFromRenewalTable(labourID);

            if (!foundInRenewal)
            {
                // If not found in renewal, check registration table
                bool foundInRegistration = LoadFromRegistrationTable(labourID);

                if (!foundInRegistration)
                {
                    MessageBox.Show($"Labour ID '{labourID}' not found in database.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearAllFieldsExceptID();
                }
            }
        }

        private bool LoadFromRenewalTable(string labourID)
        {
            string query = @"SELECT TOP 1 ContractorName, GateAccess, EntryDate, ExitDate, FullName, DOB, Area, EntryTime, CheckoutTime
                             FROM LaborRenewal 
                             WHERE LaborID = @LaborID
                             ORDER BY EntryDate DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LaborID", labourID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                contractorrenew.Text = reader["ContractorName"]?.ToString() ?? "";
                                gaterenew.Text = reader["GateAccess"]?.ToString() ?? "";
                                fullnamerenew.Text = reader["FullName"]?.ToString() ?? "";
                                arearenew.Text = reader["Area"]?.ToString() ?? "";

                                // Populating DateTime pickers
                                if (reader["EntryDate"] != DBNull.Value)
                                    indate.Value = Convert.ToDateTime(reader["EntryDate"]);

                                if (reader["ExitDate"] != DBNull.Value)
                                    outdaterenew.Value = Convert.ToDateTime(reader["ExitDate"]);

                                if (reader["DOB"] != DBNull.Value)
                                    dobrenew.Value = Convert.ToDateTime(reader["DOB"]);

                                // Populating time fields
                                if (reader["EntryTime"] != DBNull.Value && reader["CheckoutTime"] != DBNull.Value)
                                {
                                    TimeSpan entryTime = (TimeSpan)reader["EntryTime"];
                                    TimeSpan checkoutTime = (TimeSpan)reader["CheckoutTime"];

                                    // Create proper DateTime with time component
                                    DateTime entryDateTime = DateTime.Today.Add(entryTime);
                                    DateTime checkoutDateTime = DateTime.Today.Add(checkoutTime);

                                    entrytime.Value = entryDateTime;
                                    outdatetime.Value = checkoutDateTime;
                                }

                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadFromRenewalTable: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool LoadFromRegistrationTable(string labourID)
        {
            string query = @"SELECT ContractorName, FirstName, LastName 
                             FROM LaborRegistration 
                             WHERE LaborID = @LaborID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LaborID", labourID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                contractorrenew.Text = reader["ContractorName"]?.ToString() ?? "";
                                string firstName = reader["FirstName"]?.ToString() ?? "";
                                string lastName = reader["LastName"]?.ToString() ?? "";
                                fullnamerenew.Text = (firstName + " " + lastName).Trim();

                                // Reset fields that are not part of the LaborRegistration table
                                gaterenew.Clear();
                                arearenew.Clear();
                                indate.Value = DateTime.Today;
                                outdaterenew.Value = DateTime.Today;
                                entrytime.Value = DateTime.Now;
                                outdatetime.Value = DateTime.Now;
                                dobrenew.Value = DateTime.Today;

                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadFromRegistrationTable: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void ClearAllFieldsExceptID()
        {
            // Do not clear the labourrenew textbox
            contractorrenew.Clear();
            gaterenew.Clear();
            fullnamerenew.Clear();
            arearenew.Clear();
            indate.Value = DateTime.Today;
            outdaterenew.Value = DateTime.Today;
            entrytime.Value = DateTime.Now;
            outdatetime.Value = DateTime.Now;
            dobrenew.Value = DateTime.Today;
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            // Get Labour ID and other values
            string laborID = labourrenew.Text.Trim();
            string contractorName = contractorrenew.Text.Trim();
            string gateAccess = gaterenew.Text.Trim();
            string fullName = fullnamerenew.Text.Trim();
            string area = arearenew.Text.Trim();
            DateTime entryDate = indate.Value;
            DateTime exitDate = outdaterenew.Value;
            DateTime dob = dobrenew.Value;
            TimeSpan entryTime = entrytime.Value.TimeOfDay;
            TimeSpan checkoutTime = outdatetime.Value.TimeOfDay;

            // Validate required fields
            if (string.IsNullOrEmpty(laborID))
            {
                MessageBox.Show("Please enter a Labour ID.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(contractorName) || string.IsNullOrEmpty(gateAccess) || string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please fill all the mandatory fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ensure DateTimes are valid (not default values)
            if (entryDate == DateTime.MinValue || exitDate == DateTime.MinValue || dob == DateTime.MinValue)
            {
                MessageBox.Show("Please select valid dates.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ensure Times are valid (not default times)
            if (entryTime == TimeSpan.Zero || checkoutTime == TimeSpan.Zero)
            {
                MessageBox.Show("Please select valid times.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL Insert Query
            string insertQuery = @"
        INSERT INTO LaborRenewal (LaborID, ContractorName, GateAccess, FullName, Area, EntryDate, ExitDate, DOB, EntryTime, CheckoutTime)
        VALUES (@LaborID, @ContractorName, @GateAccess, @FullName, @Area, @EntryDate, @ExitDate, @DOB, @EntryTime, @CheckoutTime)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        // Add parameters with values
                        cmd.Parameters.AddWithValue("@LaborID", laborID);
                        cmd.Parameters.AddWithValue("@ContractorName", contractorName);
                        cmd.Parameters.AddWithValue("@GateAccess", gateAccess);
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@Area", area);
                        cmd.Parameters.AddWithValue("@EntryDate", entryDate);
                        cmd.Parameters.AddWithValue("@ExitDate", exitDate);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        cmd.Parameters.AddWithValue("@EntryTime", entryTime);
                        cmd.Parameters.AddWithValue("@CheckoutTime", checkoutTime);

                        // Execute the query
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void clearbtn_Click(object sender, EventArgs e)
        {
            contractorrenew.Clear();
            gaterenew.Clear();
            fullnamerenew.Clear();
            arearenew.Clear();

            indate.Value = DateTime.Today;
            outdaterenew.Value = DateTime.Today;
            dobrenew.Value = DateTime.Today;
            entrytime.Value = DateTime.Now;
            outdatetime.Value = DateTime.Now;
        }

        private void watchpreview_Click(object sender, EventArgs e)
        {
            string labourID = labourrenew.Text.Trim(); // Assuming labourrenew is the textbox for Labour ID

            if (string.IsNullOrEmpty(labourID))
            {
                MessageBox.Show("Please enter a valid Labour ID to preview the pass.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL query to get the latest entry from LaborRenewal table based on LabourID
            string query = @"
    SELECT TOP 1 ContractorName, GateAccess, FullName, Area, EntryDate, ExitDate, DOB, EntryTime, CheckoutTime
    FROM LaborRenewal
    WHERE LaborID = @LaborID
    ORDER BY EntryDate DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Retrieve the Labour Renewal data first
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LaborID", labourID);  // Correct parameter name here

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the textboxes with the retrieved data
                                namepass.Text = reader["FullName"]?.ToString() ?? "";
                                labouridpass.Text = labourID;
                                contractorpass.Text = reader["ContractorName"]?.ToString() ?? "";
                                areaspass.Text = reader["Area"]?.ToString() ?? "";
                                gatespass.Text = reader["GateAccess"]?.ToString() ?? "";

                                // Format and populate the date fields
                                if (reader["DOB"] != DBNull.Value)
                                    dobpass.Text = Convert.ToDateTime(reader["DOB"]).ToString("dd-MM-yyyy");

                                if (reader["EntryDate"] != DBNull.Value)
                                    fromdaypass.Text = Convert.ToDateTime(reader["EntryDate"]).ToString("dd-MM-yyyy");

                                if (reader["ExitDate"] != DBNull.Value)
                                    todaypass.Text = Convert.ToDateTime(reader["ExitDate"]).ToString("dd-MM-yyyy");

                                // Format and populate the time fields
                                if (reader["EntryTime"] != DBNull.Value)
                                {
                                    // Convert TimeSpan to string in HH:mm format
                                    TimeSpan entryTime = (TimeSpan)reader["EntryTime"];
                                    textBox1.Text = entryTime.ToString(@"hh\:mm"); // Correct way to format TimeSpan
                                }

                                if (reader["CheckoutTime"] != DBNull.Value)
                                {
                                    // Convert TimeSpan to string in HH:mm format
                                    TimeSpan checkoutTime = (TimeSpan)reader["CheckoutTime"];
                                    totimepasscial.Text = checkoutTime.ToString(@"hh\:mm"); // Correct way to format TimeSpan
                                }
                            }
                            else
                            {
                                MessageBox.Show("Labour ID not found in the database.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    // Fetch the photo (LaborImage) from the LaborRegistration table using the LabourID
                    string photoQuery = @"
            SELECT LaborImage
            FROM LaborRegistration
            WHERE LaborID = @LaborID";  // Correct column name: LaborImage

                    using (SqlCommand photoCmd = new SqlCommand(photoQuery, conn))
                    {
                        photoCmd.Parameters.AddWithValue("@LaborID", labourID);

                        byte[] photoData = photoCmd.ExecuteScalar() as byte[];

                        if (photoData != null && photoData.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(photoData))
                            {
                                photopass.Image = Image.FromStream(ms); // Load the image into the PictureBox
                            }
                        }
                        else
                        {
                            photopass.Image = null; // Set to null if no photo is found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void namepass_TextChanged(object sender, EventArgs e)
        {

        }

        private void labouridpass_TextChanged(object sender, EventArgs e)
        {

        }
        private string ImageToBase64(Image image)
        {
            if (image == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // or .Jpeg if preferred
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contractorpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void dobpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void fromdaypass_TextChanged(object sender, EventArgs e)
        {

        }

        private void todaypass_TextChanged(object sender, EventArgs e)
        {

        }

        private void areaspass_TextChanged(object sender, EventArgs e)
        {

        }

        private void gatespass_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void totimepasscial_TextChanged(object sender, EventArgs e)
        {

        }

        private void photopass_Click(object sender, EventArgs e)
        {

        }

        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                // Create PassModel from your textbox values
                var passModel = new PassModel
                {
                    LaborID = labouridpass.Text.Trim(),
                    FullName = fullnamerenew.Text.Trim(),
                    DOB = dobpass.Text.Trim(),
                    ContractorName = contractorpass.Text.Trim(),
                    Area = areaspass.Text.Trim(),
                    GateAccess = gatespass.Text.Trim(),
                    EntryDate = fromdaypass.Text.Trim(),
                    ExitDate = todaypass.Text.Trim(),
                    EntryTime = textBox1.Text.Trim(),
                    CheckoutTime = totimepasscial.Text.Trim(),
                    LabourImageBase64 = ConvertImageToBase64()
                };

                // Validate required fields
                if (string.IsNullOrWhiteSpace(passModel.LaborID))
                {
                    MessageBox.Show("Labor ID is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    labouridpass.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(passModel.FullName))
                {
                    MessageBox.Show("Full Name is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fullnamerenew.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(passModel.ContractorName))
                {
                    MessageBox.Show("Contractor Name is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    contractorpass.Focus();
                    return;
                }

                // Visual state feedback
                pictureBox3.Enabled = false;
                Cursor = Cursors.WaitCursor;

                bool success = await PassSender.SendPassForApproval(passModel);

                if (success)
                {
                    MessageBox.Show("Pass successfully sent for approval!", "Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // ClearForm(); // Uncomment if you want to clear input fields
                }
                else
                {
                    MessageBox.Show("Failed to send pass for approval. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pictureBox3.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        // FIX: Create a clone of the image before saving to avoid GDI+ errors
        private string ConvertImageToBase64()
        {
            if (photopass.Image != null)
            {
                try
                {
                    // Clone the image to avoid locking issues
                    using (Bitmap clonedImage = new Bitmap(photopass.Image))
                    using (MemoryStream ms = new MemoryStream())
                    {
                        clonedImage.Save(ms, ImageFormat.Jpeg);
                        byte[] imageBytes = ms.ToArray();
                        return Convert.ToBase64String(imageBytes);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error converting image to Base64: {ex.Message}");
                    MessageBox.Show($"Error processing image: {ex.Message}", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        private void ClearForm()
        {
            labouridpass.Clear();
            fullnamerenew.Clear();
            dobpass.Clear();
            contractorpass.Clear();
            areaspass.Clear();
            gatespass.Clear();
            fromdaypass.Clear();
            todaypass.Clear();
            textBox1.Clear();
            totimepasscial.Clear();

            // Dispose image before clearing
            if (photopass.Image != null)
            {
                photopass.Image.Dispose();
                photopass.Image = null;
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9();
            f9.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show(); this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show(); this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show(); this.Hide();
        }
    }
}