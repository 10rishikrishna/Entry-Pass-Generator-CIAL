using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.Data.SqlClient;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Entry_Pass_Generator_CIAL
{
    public partial class Form4 : Form
    {

        public Form4()
        {

            InitializeComponent();
            faceCascade = new CascadeClassifier("C:\\Users\\RISHI\\Downloads\\haarcascade_frontalface_default (1).xml");


        }
        private VideoCapture capture;
        private bool isCameraRunning = false;
        private CascadeClassifier faceCascade;
        private Image capturedImage = null;
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

        private void label17_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {

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
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an Image for Labor";
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
            }
        }







        private void save_Click(object sender, EventArgs e)
        {
            // Get data from textboxes
            string laborID = labourid.Text;  // Labor ID field
            string aadharNumber = aadhar.Text;  // Aadhar number field
            string contactNumber = Contactno.Text;  // Contact number field (textBox6)
            string firstName = firstname.Text;  // First name field
            string lastName = lastname.Text;  // Last name field (textBox7)
            string contractorName = Contractorname.Text;  // Contractor name field
            string fatherMotherName = fathermothername.Text;  // Father/Mother name field
            string state = statebox.Text;  // State field
            string address = addressbox.Text;  // Address field

            // For Labor Image (stored as byte array)
            byte[] laborImageBytes = null;
            if (pictureBox3.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    laborImageBytes = ms.ToArray();  // Convert image to byte array
                }
            }

            // For ID Image (stored as byte array)
            byte[] aadharImageBytes = null;
            if (pictureBox4.Image != null) // Assuming you have another PictureBox for ID Image
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox4.Image.Save(ms, pictureBox4.Image.RawFormat);
                    aadharImageBytes = ms.ToArray();  // Convert ID image to byte array
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
                        }// Handle ID Image

                        cmd.ExecuteNonQuery();

                        Form5 form5 = new Form5();
                        form5.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that might occur
                    MessageBox.Show("Error: " + ex.Message);
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
            statebox.Clear();
            addressbox.Clear();

            pictureBox3.Image = null;
            pictureBox4.Image = null;

            button2.Visible = true;
            button3.Visible = true;

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

        private void removefaceimage_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;

            button2.Visible = true;
            button1.Visible = true;
        }

        private void removeaadharimage_Click(object sender, EventArgs e)
        {
            pictureBox4.Image = null;
            button4.Visible = true;
            button3.Visible = true;

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
                capture = new VideoCapture(0);
                capture.ImageGrabbed += ProcessFrame;
                capture.Start();
                isCameraRunning = true;
                button1.Text = "Stop Camera";
            }
            else
            {
                capture?.Stop();
                capture?.Dispose();
                isCameraRunning = false;
                button1.Text = "Start Camera";
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

                    // Show in pictureBox1
                    pictureBox3.Invoke(new Action(() => {
                        pictureBox3.Image?.Dispose();
                        pictureBox3.Image = croppedFace.ToBitmap();
                    }));
                }
                else
                {
                    // No face detected, show full frame
                    pictureBox3.Invoke(new Action(() => {
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
                // Dispose previous captured image
                capturedImage?.Dispose();

                // Store the captured image
                capturedImage = (Image)pictureBox3.Image.Clone();
                pictureBox3.Image = capturedImage;

                // Optional: Stop camera after capture
                if (isCameraRunning)
                {
                    capture?.Stop();
                    capture?.Dispose();
                    isCameraRunning = false;
                    button1.Text = "Start Camera";
                }
            }
        }
    }
}
