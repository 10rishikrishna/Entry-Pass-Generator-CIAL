using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entry_Pass_Generator_CIAL
{
    public partial class Form9 : Form
    {
        private const string API_BASE_URL = "http://localhost:5135";
        private List<PassModel> approvedPasses = new List<PassModel>();
        private System.Windows.Forms.Timer refreshTimer;
        private FlowLayoutPanel mainPanel;
        private Label statusLabel;

        public Form9()
        {
            InitializeComponent();
            SetupEnhancedUI();
            _ = LoadApprovedPassesAsync();

            // Auto-refresh every 30 seconds
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 30000;
            refreshTimer.Tick += async (s, e) => await LoadApprovedPassesAsync();
            refreshTimer.Start();
        }

        private void SetupEnhancedUI()
        {
            this.Text = "Download Approved Passes - CIAL";
            this.Size = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.MinimumSize = new Size(1100, 600);
            this.AutoScaleMode = AutoScaleMode.Dpi;

            // Main container panel
            Panel containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250)
            };
            this.Controls.Add(containerPanel);

            // Header Panel with gradient effect
            Panel headerPanel = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(0, 82, 155), // CIAL Blue
                Padding = new Padding(25, 15, 25, 15)
            };

            // Add subtle shadow to header
            headerPanel.Paint += (s, e) =>
            {
                using (var shadow = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, headerPanel.Height - 3, headerPanel.Width, 3),
                    Color.FromArgb(50, 0, 0, 0),
                    Color.Transparent,
                    90f))
                {
                    e.Graphics.FillRectangle(shadow, 0, headerPanel.Height - 3, headerPanel.Width, 3);
                }
            };

            // Back Button with better styling
            Button backButton = new Button
            {
                Text = "← Back",
                Location = new Point(25, 28),
                Size = new Size(100, 44),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(0, 82, 155),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 245, 250);
            backButton.Click += (s, e) =>
            {
                Form5 form5 = new Form5();
                form5.Show();
                this.Close();
            };
            headerPanel.Controls.Add(backButton);

            // Logo placeholder (add your logo image here)
            PictureBox logoBox = new PictureBox
            {
                Location = new Point(145, 20),
                Size = new Size(60, 60),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            // If you have the logo: logoBox.Image = Image.FromFile("path_to_cial_logo.png");
            headerPanel.Controls.Add(logoBox);

            // Title with better typography
            Label titleLabel = new Label
            {
                Text = "AERODOME ENTRY PERMIT",
                Location = new Point(220, 25),
                AutoSize = true,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White
            };
            headerPanel.Controls.Add(titleLabel);

            // Subtitle
            Label subtitleLabel = new Label
            {
                Text = "Download Approved Passes",
                Location = new Point(220, 64),
                AutoSize = true,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(220, 230, 240)
            };
            headerPanel.Controls.Add(subtitleLabel);

            // Status Label with icon
            statusLabel = new Label
            {
                Text = "🔄 Loading passes...",
                Location = new Point(650, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(230, 240, 250)
            };
            headerPanel.Controls.Add(statusLabel);

            // Refresh Button with modern styling - positioned dynamically
            Button refreshButton = new Button
            {
                Text = "🔄 Refresh",
                Size = new Size(120, 44),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            refreshButton.Location = new Point(this.ClientSize.Width - 145, 28);
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(33, 136, 56);
            refreshButton.Click += async (s, e) =>
            {
                refreshButton.Enabled = false;
                refreshButton.Text = "⏳ Loading...";
                await LoadApprovedPassesAsync();
                refreshButton.Enabled = true;
                refreshButton.Text = "🔄 Refresh";
                MessageBox.Show("List refreshed successfully!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            headerPanel.Controls.Add(refreshButton);

            containerPanel.Controls.Add(headerPanel);

            // Content Panel with proper spacing - MUST be added AFTER header to prevent overlap
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(40, 20, 40, 30)
            };

            // Main Panel for passes with better scrolling
            mainPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(0),
                BackColor = Color.Transparent
            };

            contentPanel.Controls.Add(mainPanel);

            // IMPORTANT: Add content panel AFTER header panel
            containerPanel.Controls.Add(contentPanel);
            containerPanel.Controls.Add(headerPanel);
        }

        private async Task LoadApprovedPassesAsync()
        {
            try
            {
                statusLabel.Text = "🔄 Loading passes...";

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    string apiUrl = $"{API_BASE_URL}/api/passes/approved";
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        approvedPasses = JsonSerializer.Deserialize<List<PassModel>>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }) ?? new List<PassModel>();

                        DisplayApprovedPasses();
                        statusLabel.Text = $"✓ {approvedPasses.Count} approved pass(es) ready";
                        System.Diagnostics.Debug.WriteLine($"[{DateTime.Now:HH:mm:ss}] Loaded {approvedPasses.Count} approved passes");
                    }
                    else
                    {
                        statusLabel.Text = "⚠ Error loading passes";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                statusLabel.Text = "❌ Connection error";
                MessageBox.Show("Unable to connect to the server. Please ensure the API is running.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DisplayApprovedPasses()
        {
            mainPanel.Controls.Clear();

            // Calculate card width based on available space
            int availableWidth = mainPanel.ClientSize.Width - 20; // Account for scrollbar

            if (approvedPasses.Count == 0)
            {
                Panel emptyPanel = new Panel
                {
                    Width = availableWidth,
                    Height = 300,
                    BackColor = Color.White,
                    Margin = new Padding(0, 30, 0, 0)
                };

                // Add rounded corners effect
                emptyPanel.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(220, 223, 230), 1))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, emptyPanel.Width - 1, emptyPanel.Height - 1);
                    }
                };

                Label emptyIcon = new Label
                {
                    Text = "📋",
                    Font = new Font("Segoe UI", 56),
                    AutoSize = true
                };
                int iconX = (availableWidth - 80) / 2;
                emptyIcon.Location = new Point(iconX, 80);
                emptyPanel.Controls.Add(emptyIcon);

                Label emptyLabel = new Label
                {
                    Text = "No approved passes available",
                    Font = new Font("Segoe UI", 16),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    AutoSize = true
                };
                int labelX = (availableWidth - 320) / 2;
                emptyLabel.Location = new Point(labelX, 170);
                emptyPanel.Controls.Add(emptyLabel);

                Label emptySubLabel = new Label
                {
                    Text = "Approved passes will appear here automatically",
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.FromArgb(173, 181, 189),
                    AutoSize = true
                };
                int subLabelX = (availableWidth - 360) / 2;
                emptySubLabel.Location = new Point(subLabelX, 205);
                emptyPanel.Controls.Add(emptySubLabel);

                mainPanel.Controls.Add(emptyPanel);
                return;
            }

            foreach (var pass in approvedPasses)
            {
                Panel card = CreateEnhancedPassCard(pass);
                mainPanel.Controls.Add(card);
            }
        }

        private Panel CreateEnhancedPassCard(PassModel pass)
        {
            // Calculate card width dynamically - account for scrollbar
            int cardWidth = mainPanel.ClientSize.Width - 30;
            if (cardWidth < 900) cardWidth = 900; // Minimum width

            Panel card = new Panel
            {
                Width = cardWidth,
                Height = 150,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15)
            };

            // Enhanced shadow and border
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Subtle shadow
                using (var shadowBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(2, 2, card.Width - 2, card.Height - 2),
                    Color.FromArgb(20, 0, 0, 0),
                    Color.FromArgb(5, 0, 0, 0),
                    45f))
                {
                    e.Graphics.FillRectangle(shadowBrush, 2, 2, card.Width - 2, card.Height - 2);
                }

                // Border
                using (var pen = new Pen(Color.FromArgb(222, 226, 230), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            // Left accent bar (matches CIAL blue)
            Panel accentBar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(5, 150),
                BackColor = Color.FromArgb(0, 82, 155)
            };
            card.Controls.Add(accentBar);

            // Labor ID (Bold and prominent)
            Label lblLaborId = new Label
            {
                Text = pass.LaborID,
                Location = new Point(25, 20),
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(33, 37, 41)
            };
            card.Controls.Add(lblLaborId);

            // Name with icon
            Label lblName = new Label
            {
                Text = $"👤 {pass.FullName ?? "N/A"}",
                Location = new Point(25, 55),
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                AutoSize = true,
                ForeColor = Color.FromArgb(73, 80, 87)
            };
            card.Controls.Add(lblName);

            // Contractor info with icon
            if (!string.IsNullOrEmpty(pass.ContractorName))
            {
                Label lblContractor = new Label
                {
                    Text = $"🏢 {pass.ContractorName}",
                    Location = new Point(25, 90),
                    Font = new Font("Segoe UI", 9),
                    AutoSize = true,
                    MaximumSize = new Size(400, 0),
                    ForeColor = Color.FromArgb(108, 117, 125)
                };
                card.Controls.Add(lblContractor);
            }

            // Calculate positions based on card width - ensure button is visible
            int validityX = Math.Max(450, cardWidth / 2 - 100);
            int approvedX = Math.Max(validityX + 280, cardWidth - 420);
            int buttonX = Math.Max(approvedX + 150, cardWidth - 250);

            // Validity Dates in a better format
            if (!string.IsNullOrEmpty(pass.EntryDate))
            {
                Panel datesPanel = new Panel
                {
                    Location = new Point(validityX, 25),
                    Size = new Size(250, 80),
                    BackColor = Color.FromArgb(248, 249, 250)
                };

                Label lblValidityTitle = new Label
                {
                    Text = "Validity Period",
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    Location = new Point(10, 8),
                    AutoSize = true
                };
                datesPanel.Controls.Add(lblValidityTitle);

                Label lblFromDate = new Label
                {
                    Text = $"From: {pass.EntryDate}",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(73, 80, 87),
                    Location = new Point(10, 28),
                    AutoSize = true
                };
                datesPanel.Controls.Add(lblFromDate);

                Label lblToDate = new Label
                {
                    Text = $"To: {pass.ExitDate ?? "N/A"}",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(73, 80, 87),
                    Location = new Point(10, 48),
                    AutoSize = true
                };
                datesPanel.Controls.Add(lblToDate);

                card.Controls.Add(datesPanel);
            }

            // Approved Badge (styled like the image)
            Panel badge = new Panel
            {
                Location = new Point(approvedX, 35),
                Size = new Size(130, 35),
                BackColor = Color.FromArgb(212, 237, 218)
            };

            badge.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(195, 230, 203), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, badge.Width - 1, badge.Height - 1);
                }
            };

            Label badgeLabel = new Label
            {
                Text = "✓ APPROVED",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 135, 84),
                AutoSize = false,
                Size = new Size(130, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };
            badge.Controls.Add(badgeLabel);
            card.Controls.Add(badge);

            // Download Button with modern styling - ensure it's visible
            Button btnDownload = new Button
            {
                Text = "📥 Download",
                Location = new Point(buttonX, 35),
                Size = new Size(170, 50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 82, 155),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = pass
            };
            btnDownload.FlatAppearance.BorderSize = 0;
            btnDownload.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 65, 125);
            btnDownload.Click += DownloadPassButton_Click;
            card.Controls.Add(btnDownload);

            // View Details link (optional)
            LinkLabel lnkDetails = new LinkLabel
            {
                Text = "View Details →",
                Location = new Point(buttonX + 40, 95),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                LinkColor = Color.FromArgb(0, 82, 155),
                VisitedLinkColor = Color.FromArgb(0, 82, 155),
                LinkBehavior = LinkBehavior.HoverUnderline
            };
            lnkDetails.Click += (s, e) =>
            {
                MessageBox.Show(
                    $"Labor ID: {pass.LaborID}\n" +
                    $"Full Name: {pass.FullName ?? "N/A"}\n" +
                    $"Contractor: {pass.ContractorName ?? "N/A"}\n" +
                    $"Entry Date: {pass.EntryDate ?? "N/A"}\n" +
                    $"Exit Date: {pass.ExitDate ?? "N/A"}\n" +
                    $"DOB: {pass.DOB ?? "N/A"}",
                    "Pass Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            };
            card.Controls.Add(lnkDetails);

            return card;
        }

        private async void DownloadPassButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is not PassModel pass) return;

            try
            {
                string signatureImagePath = @"C:\Users\RISHI\source\repos\Entry Pass Generator-CIAL\Entry Pass Generator-CIAL\Images\approval_signature.png";

                if (!File.Exists(signatureImagePath))
                {
                    MessageBox.Show(
                        $"Signature image not found!\n\nExpected location:\n{signatureImagePath}\n\nPlease ensure the signature file exists.",
                        "Missing Signature File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                btn.Enabled = false;
                btn.Text = "⏳ Generating...";
                btn.BackColor = Color.FromArgb(108, 117, 125);
                Cursor = Cursors.WaitCursor;

                string savedPath = PassGeneratorService.GenerateApprovedPass(pass, signatureImagePath);

                await MarkAsDownloaded(pass.LaborID);

                Cursor = Cursors.Default;
                btn.Enabled = true;
                btn.Text = "📥 Download Pass";
                btn.BackColor = Color.FromArgb(0, 82, 155);

                DialogResult result = MessageBox.Show(
                    $"✓ Pass generated successfully!\n\n" +
                    $"Labor ID: {pass.LaborID}\n" +
                    $"Name: {pass.FullName}\n\n" +
                    $"Saved to:\n{savedPath}\n\n" +
                    $"Would you like to open the folder?",
                    "Success",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{savedPath}\"");
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                btn.Enabled = true;
                btn.Text = "📥 Download Pass";
                btn.BackColor = Color.FromArgb(0, 82, 155);

                MessageBox.Show(
                    $"Error generating pass:\n\n{ex.Message}\n\nPlease try again or contact support.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task MarkAsDownloaded(string laborId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"{API_BASE_URL}/api/passes/mark-downloaded/{laborId}";
                    var response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine($"Pass {laborId} marked as downloaded");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error marking as downloaded: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            refreshTimer?.Stop();
            refreshTimer?.Dispose();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            // Form load event - initialization already done in constructor
        }
    }
}