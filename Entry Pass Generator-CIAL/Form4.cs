using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entry_Pass_Generator_CIAL
{
    public partial class Form4 : Form
    {
        private VideoCapture capture;
        private bool isCameraRunning = false;
        private bool isAadharCameraRunning = false;
        private CascadeClassifier faceCascade;
        private Image capturedImage = null;

        public Form4()
        {
            InitializeComponent();
            faceCascade = new CascadeClassifier("C:\\Users\\RISHI\\Downloads\\haarcascade_frontalface_default (1).xml");

            // Initialize state dropdown
            InitializeStateDropdown();
        }

        private void Labour_infobox_Enter(object sender, EventArgs e)
        {
            // Empty method to resolve external designer reference error.
        }

        private void InitializeStateDropdown()
        {
            // **CRITICAL:** 'statebox' MUST be a System.Windows.Forms.ComboBox for this code to work.

            // Clear existing items
            statebox.Items.Clear();

            // Add all Indian states and union territories
            statebox.Items.AddRange(new object[]
            {
                "Andhra Pradesh",
                "Arunachal Pradesh",
                "Assam",
                "Bihar",
                "Chhattisgarh",
                "Goa",
                "Gujarat",
                "Haryana",
                "Himachal Pradesh",
                "Jharkhand",
                "Karnataka",
                "Kerala",
                "Madhya Pradesh",
                "Maharashtra",
                "Manipur",
                "Meghalaya",
                "Mizoram",
                "Nagaland",
                "Odisha",
                "Punjab",
                "Rajasthan",
                "Sikkim",
                "Tamil Nadu",
                "Telangana",
                "Tripura",
                "Uttar Pradesh",
                "Uttarakhand",
                "West Bengal",
                "Andaman and Nicobar Islands",
                "Chandigarh",
                "Dadra and Nagar Haveli and Daman and Diu",
                "Delhi",
                "Jammu and Kashmir",
                "Ladakh",
                "Lakshadweep",
                "Puducherry"
            });

            // Set dropdown style
            statebox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Calculates the Euclidean distance between two System.Drawing.Point structs.
        /// </summary>
        private int Distance(Point p1, Point p2)
        {
            return (int)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

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

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }




        private void label16_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show(); this.Hide();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
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

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lastname_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void aadhar_TextChanged(object sender, EventArgs e)
        {
            // Remove any non-digit characters
            string text = new string(aadhar.Text.Where(char.IsDigit).ToArray());

            // Limit to 12 digits
            if (text.Length > 12)
            {
                text = text.Substring(0, 12);
            }

            // Update textbox if changed
            if (aadhar.Text != text)
            {
                int cursorPosition = aadhar.SelectionStart;
                aadhar.Text = text;
                aadhar.SelectionStart = Math.Min(cursorPosition, text.Length);
            }
        }

        private void aadhar_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (backspace, delete, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void state_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an Image for Labor";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                pictureBox3.Image = Image.FromFile(filePath);
                button2.Visible = false;
                button1.Visible = false;
                pictureBox5.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                removefaceimage.Visible = true;
                keepphoto.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Aadhar Card Image";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                pictureBox4.Image = Image.FromFile(filePath);
                button3.Visible = false;
                button4.Visible = false;
                pictureBox6.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                removeaadharimage.Visible = true;
                button5.Visible = true;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            // Validate Aadhar number
            if (string.IsNullOrWhiteSpace(aadhar.Text))
            {
                MessageBox.Show("Please enter Aadhar number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                aadhar.Focus();
                return;
            }

            if (aadhar.Text.Length < 12)
            {
                MessageBox.Show($"Aadhar number must be exactly 12 digits! You entered only {aadhar.Text.Length} digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                aadhar.Focus();
                return;
            }

            // Validate Contact number
            if (string.IsNullOrWhiteSpace(Contactno.Text))
            {
                MessageBox.Show("Please enter contact number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Contactno.Focus();
                return;
            }

            if (Contactno.Text.Length < 10)
            {
                MessageBox.Show($"Contact number must be exactly 10 digits! You entered only {Contactno.Text.Length} digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Contactno.Focus();
                return;
            }

            // Validate other required fields
            if (string.IsNullOrWhiteSpace(labourid.Text))
            {
                MessageBox.Show("Please enter Labor ID!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                labourid.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(firstname.Text))
            {
                MessageBox.Show("Please enter First Name!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                firstname.Focus();
                return;
            }

            if (statebox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a State!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                statebox.Focus();
                return;
            }

            // Get data from textboxes
            string laborID = labourid.Text;
            string aadharNumber = aadhar.Text;
            string contactNumber = Contactno.Text;
            string firstName = firstname.Text;
            string lastName = lastname.Text;
            string contractorName = Contractorname.Text;
            string fatherMotherName = fathermothername.Text;
            string state = statebox.SelectedItem.ToString();
            string address = addressbox.Text;

            // For Labor Image (stored as byte array)
            byte[] laborImageBytes = null;
            if (pictureBox3.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox3.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    laborImageBytes = ms.ToArray();
                }
            }

            // For ID Image (stored as byte array) - FIXED
            byte[] aadharImageBytes = null;
            if (pictureBox4.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox4.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    aadharImageBytes = ms.ToArray();
                }
            }

            // SQL Connection string
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\RISHI\Documents\Eyegate.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

            // SQL Insert query
            string query = @"INSERT INTO LaborRegistration (LaborID, AadharNumber, ContactNumber, FirstName, LastName, ContractorName, FatherMotherName, StateName, AddressName, LaborImage, AadharImage) 
                     VALUES (@LaborID, @AadharNumber, @ContactNumber, @FirstName, @LastName, @ContractorName, @FatherMotherName, @StateName, @AddressName, @LaborImage, @AadharImage)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LaborID", laborID);
                        cmd.Parameters.AddWithValue("@AadharNumber", aadharNumber);
                        cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@ContractorName", contractorName);
                        cmd.Parameters.AddWithValue("@FatherMotherName", fatherMotherName);
                        cmd.Parameters.AddWithValue("@StateName", state);
                        cmd.Parameters.AddWithValue("@AddressName", address);

                        if (laborImageBytes == null)
                        {
                            cmd.Parameters.AddWithValue("@LaborImage", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@LaborImage", laborImageBytes);
                        }

                        if (aadharImageBytes == null)
                        {
                            cmd.Parameters.AddWithValue("@AadharImage", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AadharImage", aadharImageBytes);
                        }

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Form5 form5 = new Form5();
                        form5.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            labourid.Clear();
            aadhar.Clear();
            Contactno.Clear();
            firstname.Clear();
            lastname.Clear();
            Contractorname.Clear();
            fathermothername.Clear();
            statebox.SelectedIndex = -1;
            addressbox.Clear();

            pictureBox3.Image = null;
            pictureBox4.Image = null;

            button2.Visible = true;
            button3.Visible = true;
            button1.Visible = true;
            button4.Visible = true;
            removefaceimage.Visible = false;
            removeaadharimage.Visible = false;
            keepphoto.Visible = false;
            button5.Visible = false;
            pictureBox5.Visible = true;
            pictureBox6.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
        }

        private void fathermothername_TextChanged(object sender, EventArgs e)
        {

        }

        private void address_TextChanged(object sender, EventArgs e)
        {

        }

        private void Contractorname_TextChanged(object sender, EventArgs e)
        {

        }

        private void firstname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Contactno_TextChanged(object sender, EventArgs e)
        {
            // Remove any non-digit characters
            string text = new string(Contactno.Text.Where(char.IsDigit).ToArray());

            // Limit to 10 digits
            if (text.Length > 10)
            {
                text = text.Substring(0, 10);
            }

            // Update textbox if changed
            if (Contactno.Text != text)
            {
                int cursorPosition = Contactno.SelectionStart;
                Contactno.Text = text;
                Contactno.SelectionStart = Math.Min(cursorPosition, text.Length);
            }
        }

        private void Contactno_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void removefaceimage_Click(object sender, EventArgs e)
        {
            pictureBox3.Image?.Dispose();
            pictureBox3.Image = null;

            button2.Visible = true;
            button1.Visible = true;
            pictureBox5.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            removefaceimage.Visible = false;
            keepphoto.Visible = false;
        }

        private void removeaadharimage_Click(object sender, EventArgs e)
        {
            // Stop camera if running
            if (isAadharCameraRunning)
            {
                capture?.Stop();
                capture?.Dispose();
                isAadharCameraRunning = false;
                button4.Text = "Start Camera";
            }

            pictureBox4.Image?.Dispose();
            pictureBox4.Image = null;

            button4.Visible = true;
            button3.Visible = true;
            pictureBox6.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
            removeaadharimage.Visible = false;
            button5.Visible = false;
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isCameraRunning)
            {
                // Stop Aadhar camera if it's running when starting the face camera
                if (isAadharCameraRunning)
                {
                    capture?.Stop();
                    capture?.Dispose();
                    isAadharCameraRunning = false;
                    button4.Text = "Start Camera";
                    // Reset Aadhar controls visually if needed
                }

                capture = new VideoCapture(0);
                capture.ImageGrabbed += ProcessFrame;
                capture.Start();
                isCameraRunning = true;
                button1.Text = "Stop Camera";

                button2.Visible = false;
                pictureBox5.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                removefaceimage.Visible = true;
                keepphoto.Visible = true;
            }
            else
            {
                capture?.Stop();
                capture?.Dispose();
                isCameraRunning = false;
                button1.Text = "Start Camera";

                // Show other controls back
                button2.Visible = true;
                pictureBox5.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
            }
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (capture != null && capture.Ptr != IntPtr.Zero)
            {
                Mat frame = new Mat();
                capture.Retrieve(frame);

                // Convert to gray for face detection
                Mat grayFrame = new Mat();
                CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);
                CvInvoke.EqualizeHist(grayFrame, grayFrame);

                // Detect faces
                Rectangle[] faces = faceCascade.DetectMultiScale(grayFrame, 1.1, 3, Size.Empty);

                if (faces.Length > 0)
                {
                    // Get the first face
                    Rectangle face = faces[0];

                    // Add padding (top and bottom regions)
                    int topPadding = face.Height / 2;
                    int bottomPadding = face.Height / 3;

                    // Calculate cropped region
                    int y = Math.Max(0, face.Y - topPadding);
                    int height = Math.Min(frame.Height - y, face.Height + topPadding + bottomPadding);

                    Rectangle cropRegion = new Rectangle(face.X, y, face.Width, height);

                    // Crop the face region
                    Mat croppedFace = new Mat(frame, cropRegion);

                    // Show in pictureBox3
                    pictureBox3.Invoke(new Action(() =>
                    {
                        pictureBox3.Image?.Dispose();
                        pictureBox3.Image = croppedFace.ToBitmap();
                    }));
                }
                else
                {
                    // No face detected, show full frame
                    pictureBox3.Invoke(new Action(() =>
                    {
                        pictureBox3.Image?.Dispose();
                        pictureBox3.Image = frame.ToBitmap();
                    }));
                }
            }
        }

        private void keepphoto_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image != null)
            {
                try
                {
                    // Stop camera if running
                    if (isCameraRunning)
                    {
                        capture?.Stop();
                        capture?.Dispose();
                        isCameraRunning = false;
                        button1.Text = "Start Camera";
                    }

                    // Create desktop path with person's name
                    string personName = firstname.Text.Trim();
                    if (string.IsNullOrEmpty(personName))
                    {
                        personName = "Unknown_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    }

                    // Remove invalid characters from filename
                    string invalidChars = new string(Path.GetInvalidFileNameChars());
                    foreach (char c in invalidChars)
                    {
                        personName = personName.Replace(c.ToString(), "");
                    }

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string fileName = $"{personName}.jpg";
                    string fullPath = Path.Combine(desktopPath, fileName);

                    // If file exists, add timestamp
                    if (File.Exists(fullPath))
                    {
                        fileName = $"{personName}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.jpg";
                        fullPath = Path.Combine(desktopPath, fileName);
                    }

                    // Save the image
                    pictureBox3.Image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    MessageBox.Show($"Photo saved successfully to Desktop as {fileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Don't show other controls back - keep the photo displayed
                    // User can click removefaceimage if they want to retake
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving photo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No photo to save!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!isAadharCameraRunning)
            {
                // Stop Face camera if it's running when starting the Aadhar camera
                if (isCameraRunning)
                {
                    capture?.Stop();
                    capture?.Dispose();
                    isCameraRunning = false;
                    button1.Text = "Start Camera";
                }

                capture = new VideoCapture(0);
                capture.ImageGrabbed += ProcessAadharFrame;
                capture.Start();
                isAadharCameraRunning = true;
                button4.Text = "Stop Camera";

                // Hide other buttons, show removeaadharimage AND button5
                button3.Visible = false;
                pictureBox6.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                removeaadharimage.Visible = true;
                button5.Visible = true;
            }
            else
            {
                // Stop the camera
                capture?.Stop();
                capture?.Dispose();
                isAadharCameraRunning = false;
                button4.Text = "Start Camera";

                // Keep the last captured frame in pictureBox4
                // Don't show other controls back - keep removeaadharimage and button5 visible
                // so user can save or retake the photo
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (pictureBox4.Image != null)
            {
                try
                {
                    // Stop camera if running
                    if (isAadharCameraRunning)
                    {
                        capture?.Stop();
                        capture?.Dispose();
                        isAadharCameraRunning = false;
                        button4.Text = "Start Camera";
                    }

                    // Get Aadhaar number from the textbox
                    string aadharNumber = aadhar.Text.Trim();

                    if (string.IsNullOrEmpty(aadharNumber))
                    {
                        MessageBox.Show("Please enter an Aadhaar number first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Remove invalid characters from filename
                    string invalidChars = new string(Path.GetInvalidFileNameChars());
                    foreach (char c in invalidChars)
                    {
                        aadharNumber = aadharNumber.Replace(c.ToString(), "");
                    }

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string fileName = $"{aadharNumber}.png";
                    string fullPath = Path.Combine(desktopPath, fileName);

                    // If file exists, add timestamp
                    if (File.Exists(fullPath))
                    {
                        fileName = $"{aadharNumber}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.png";
                        fullPath = Path.Combine(desktopPath, fileName);
                    }

                    // Save the image as PNG
                    pictureBox4.Image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);

                    MessageBox.Show($"Aadhaar image saved successfully to Desktop as {fileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Don't show other controls back - keep the photo displayed
                    // User can click removeaadharimage if they want to retake
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving Aadhaar image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No Aadhaar image to save!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ProcessAadharFrame(object sender, EventArgs e)
        {
            if (capture != null && capture.Ptr != IntPtr.Zero)
            {
                Mat frame = new Mat();
                capture.Retrieve(frame);

                // Convert to grayscale
                Mat gray = new Mat();
                CvInvoke.CvtColor(frame, gray, ColorConversion.Bgr2Gray);

                // Apply bilateral filter to reduce noise while keeping edges sharp
                Mat filtered = new Mat();
                CvInvoke.BilateralFilter(gray, filtered, 9, 75, 75);

                // Apply adaptive threshold
                Mat thresh = new Mat();
                CvInvoke.AdaptiveThreshold(filtered, thresh, 255, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 11, 2);

                // Edge detection with adjusted parameters
                Mat edges = new Mat();
                CvInvoke.Canny(filtered, edges, 50, 150, 3);

                // Dilate edges to make them more connected
                Mat kernel = new Mat(new Size(3, 3), DepthType.Cv8U, 1);
                kernel.SetTo(new MCvScalar(1));
                CvInvoke.Dilate(edges, edges, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

                // Find contours
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Mat hierarchy = new Mat();
                CvInvoke.FindContours(edges, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                // Find the largest rectangular contour
                double maxArea = 0;
                int maxAreaIndex = -1;
                VectorOfPoint bestApprox = null;

                for (int i = 0; i < contours.Size; i++)
                {
                    VectorOfPoint contour = contours[i];
                    double area = CvInvoke.ContourArea(contour);
                    double perimeter = CvInvoke.ArcLength(contour, true);

                    // Filter based on area (card should be reasonably large in frame)
                    if (area > 50000 && area < frame.Width * frame.Height * 0.9)
                    {
                        VectorOfPoint approx = new VectorOfPoint();
                        CvInvoke.ApproxPolyDP(contour, approx, 0.02 * perimeter, true);

                        // Check if it's a quadrilateral (4 corners) and has the largest area
                        if (approx.Size == 4 && area > maxArea)
                        {
                            // Check aspect ratio (Aadhar card is typically 3.37:2.125)
                            Rectangle rect = CvInvoke.BoundingRectangle(approx);
                            double aspectRatio = (double)rect.Width / rect.Height;

                            // Accept aspect ratios between 1.2 and 2.0 (to account for perspective)
                            if (aspectRatio > 1.2 && aspectRatio < 2.0)
                            {
                                maxArea = area;
                                maxAreaIndex = i;
                                bestApprox = approx;
                            }
                        }
                    }
                }

                Mat resultFrame = frame.Clone();

                if (maxAreaIndex != -1 && bestApprox != null)
                {
                    // Draw the detected rectangle in green
                    CvInvoke.DrawContours(resultFrame, new VectorOfVectorOfPoint(bestApprox), -1, new MCvScalar(0, 255, 0), 3);

                    // Get bounding rectangle
                    Rectangle boundingRect = CvInvoke.BoundingRectangle(bestApprox);

                    // Add some padding to the bounding rectangle
                    int padding = 10;
                    boundingRect.X = Math.Max(0, boundingRect.X - padding);
                    boundingRect.Y = Math.Max(0, boundingRect.Y - padding);
                    boundingRect.Width = Math.Min(frame.Width - boundingRect.X, boundingRect.Width + 2 * padding);
                    boundingRect.Height = Math.Min(frame.Height - boundingRect.Y, boundingRect.Height + 2 * padding);

                    // Crop the card region
                    if (boundingRect.Width > 0 && boundingRect.Height > 0)
                    {
                        Mat croppedCard = new Mat(frame, boundingRect);

                        // Apply perspective transform for better quality if possible
                        try
                        {
                            Point[] srcPoints = new Point[4];
                            for (int i = 0; i < 4; i++)
                            {
                                srcPoints[i] = bestApprox.ToArray()[i];
                            }

                            // Sort points: top-left, top-right, bottom-right, bottom-left
                            srcPoints = SortPoints(srcPoints);

                            // Calculate destination dimensions
                            int width = Math.Max(
                                Distance(srcPoints[0], srcPoints[1]),
                                Distance(srcPoints[2], srcPoints[3])
                            );
                            int height = Math.Max(
                                Distance(srcPoints[0], srcPoints[3]),
                                Distance(srcPoints[1], srcPoints[2])
                            );

                            PointF[] dstPoints = new PointF[]
                            {
                                new PointF(0, 0),
                                new PointF(width - 1, 0),
                                new PointF(width - 1, height - 1),
                                new PointF(0, height - 1)
                            };

                            PointF[] srcPointsF = Array.ConvertAll(srcPoints, p => new PointF(p.X, p.Y));

                            Mat perspectiveMatrix = CvInvoke.GetPerspectiveTransform(srcPointsF, dstPoints);
                            Mat warped = new Mat();
                            CvInvoke.WarpPerspective(frame, warped, perspectiveMatrix, new Size(width, height));

                            pictureBox4.Invoke(new Action(() =>
                            {
                                pictureBox4.Image?.Dispose();
                                pictureBox4.Image = warped.ToBitmap();
                            }));
                        }
                        catch
                        {
                            // If perspective transform fails, use simple crop
                            pictureBox4.Invoke(new Action(() =>
                            {
                                pictureBox4.Image?.Dispose();
                                pictureBox4.Image = croppedCard.ToBitmap();
                            }));
                        }
                    }
                }
                else
                {
                    // No card detected, show full frame with edge detection overlay
                    pictureBox4.Invoke(new Action(() =>
                    {
                        pictureBox4.Image?.Dispose();
                        pictureBox4.Image = resultFrame.ToBitmap();
                    }));
                }

                // Cleanup
                filtered.Dispose();
            }
        }

        /// <summary>
        /// Sorts the 4 corner points of a detected rectangle for perspective transform.
        /// Order: Top-Left, Top-Right, Bottom-Right, Bottom-Left
        /// </summary>
        private Point[] SortPoints(Point[] points)
        {
            // Sort by x-coordinate
            Point[] sortedX = points.OrderBy(p => p.X).ToArray();

            // Get left two points and right two points
            Point[] leftPoints = sortedX.Take(2).OrderBy(p => p.Y).ToArray();
            Point[] rightPoints = sortedX.Skip(2).OrderBy(p => p.Y).ToArray();

            // leftPoints[0] is Top-Left, leftPoints[1] is Bottom-Left
            // rightPoints[0] is Top-Right, rightPoints[1] is Bottom-Right

            Point topLeft = leftPoints[0];
            Point bottomLeft = leftPoints[1];
            Point topRight = rightPoints[0];
            Point bottomRight = rightPoints[1];

            // Rearrange to: Top-Left, Top-Right, Bottom-Right, Bottom-Left
            return new Point[] { topLeft, topRight, bottomRight, bottomLeft };
        }

        private void label17_Click_1(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

    }
}