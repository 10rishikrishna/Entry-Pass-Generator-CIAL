using CIAL.Shared.DigitalSignature;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Entry_Pass_Generator_CIAL
{
    public class PassGeneratorService
    {
        private const int PassWidth = 468;
        private const int PassHeight = 564;

        public static string GenerateApprovedPass(PassModel passData, string approvalSignatureImagePath = null)
        {
            if (passData == null)
                throw new ArgumentNullException(nameof(passData), "PassModel cannot be null");

            Bitmap passBitmap = null;
            Graphics g = null;

            try
            {
                passBitmap = new Bitmap(PassWidth, PassHeight, PixelFormat.Format32bppArgb);
                g = Graphics.FromImage(passBitmap);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                g.Clear(Color.White);

                DrawDottedBorder(g);
                DrawHeader(g);
                DrawUnderEscortBanner(g);
                DrawEmployeePhoto(g, passData.LabourImageBase64);
                DrawPersonDetails(g, passData);
                DrawValidityAndAccessSection(g, passData);
                DrawDigitalSignatureBadge(g, passData.DigitalSignature);
                DrawFooter(g);

                if (g != null)
                {
                    g.Dispose();
                    g = null;
                }

                string savedPath = SavePass(passBitmap, passData.LaborID);
                return savedPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating pass: {ex.Message}\nStack: {ex.StackTrace}", ex);
            }
            finally
            {
                if (g != null)
                {
                    g.Dispose();
                }
                if (passBitmap != null)
                {
                    passBitmap.Dispose();
                }
            }
        }

        private static void DrawDottedBorder(Graphics g)
        {
            using (Pen dottedPen = new Pen(Color.Black, 2))
            {
                dottedPen.DashStyle = DashStyle.Dot;
                g.DrawRectangle(dottedPen, 2, 2, PassWidth - 4, PassHeight - 4);
            }
        }

        private static void DrawHeader(Graphics g)
        {
            using (Font headerFont = new Font("Arial", 15, FontStyle.Bold))
            using (SolidBrush lightBannerBrush = new SolidBrush(Color.FromArgb(248, 248, 250)))
            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
            {
                int headerY = 5;
                int bannerHeight = 40;

                g.FillRectangle(lightBannerBrush, 0, headerY, PassWidth, bannerHeight);

                StringFormat centerFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString("AERODOME ENTRY PERMIT", headerFont, blackBrush,
                    new RectangleF(0, headerY, PassWidth, bannerHeight), centerFormat);
            }
        }

        private static void DrawUnderEscortBanner(Graphics g)
        {
            using (SolidBrush redBrush = new SolidBrush(Color.FromArgb(211, 47, 47)))
            using (Font bannerFont = new Font("Arial", 11, FontStyle.Bold))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            {
                // Increased margin from top and bottom
                int startY = 220;
                int bannerHeight = 170;

                g.FillRectangle(redBrush, 0, startY, 30, bannerHeight);

                GraphicsState state = g.Save();
                // Center the text in the banner
                g.TranslateTransform(15, startY + (bannerHeight / 2));
                g.RotateTransform(-90);

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString("UNDER ESCORT", bannerFont, whiteBrush, 0, 0, sf);
                g.Restore(state);
            }
        }

        private static void DrawEmployeePhoto(Graphics g, string base64Image)
        {
            int photoX = 178;
            int photoY = 50;
            int photoWidth = 112;
            int photoHeight = 140;

            using (Pen photoBorder = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(photoBorder, photoX, photoY, photoWidth, photoHeight);
            }

            if (!string.IsNullOrEmpty(base64Image))
            {
                Bitmap photoBitmap = null;
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Image);

                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        using (Image tempImage = Image.FromStream(ms, true, false))
                        {
                            photoBitmap = new Bitmap(photoWidth - 4, photoHeight - 4);
                            using (Graphics gPhoto = Graphics.FromImage(photoBitmap))
                            {
                                gPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                gPhoto.DrawImage(tempImage, 0, 0, photoWidth - 4, photoHeight - 4);
                            }

                            g.DrawImage(photoBitmap, photoX + 2, photoY + 2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading photo: {ex.Message}");
                    DrawPhotoPlaceholder(g, photoX, photoY, photoWidth, photoHeight);
                }
                finally
                {
                    if (photoBitmap != null)
                    {
                        photoBitmap.Dispose();
                    }
                }
            }
            else
            {
                DrawPhotoPlaceholder(g, photoX, photoY, photoWidth, photoHeight);
            }
        }

        private static void DrawPhotoPlaceholder(Graphics g, int x, int y, int width, int height)
        {
            using (SolidBrush placeholderBrush = new SolidBrush(Color.FromArgb(245, 245, 245)))
            using (Font placeholderFont = new Font("Arial", 10, FontStyle.Regular))
            using (SolidBrush textBrush = new SolidBrush(Color.Gray))
            {
                g.FillRectangle(placeholderBrush, x + 2, y + 2, width - 4, height - 4);

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString("No Photo", placeholderFont, textBrush,
                    new RectangleF(x + 2, y + 2, width - 4, height - 4), sf);
            }
        }

        private static void DrawPersonDetails(Graphics g, PassModel passData)
        {
            using (Font nameFont = new Font("Arial", 18, FontStyle.Bold))
            using (Font labelFont = new Font("Arial", 10, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 10, FontStyle.Regular))
            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
            {
                StringFormat centerFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                int nameY = 210;
                string fullName = passData.FullName ?? "N/A";

                g.DrawString(fullName, nameFont, blackBrush,
                    new RectangleF(40, nameY, PassWidth - 80, 25), centerFormat);

                int detailY1 = 238;
                string dobValue = passData.DOB ?? "N/A";
                string labourValue = passData.LaborID ?? "N/A";

                float centerX = PassWidth / 2f;
                SizeF fullSize = g.MeasureString($"DOB: {dobValue}  |  Labour ID: {labourValue}", valueFont);
                float startX = centerX - (fullSize.Width / 2);
                float currentX = startX;

                g.DrawString("DOB:", labelFont, blackBrush, currentX, detailY1);
                currentX += g.MeasureString("DOB:", labelFont).Width;

                g.DrawString(" " + dobValue + "  |  ", valueFont, blackBrush, currentX, detailY1);
                currentX += g.MeasureString(" " + dobValue + "  |  ", valueFont).Width;

                g.DrawString("Labour ID:", labelFont, blackBrush, currentX, detailY1);
                currentX += g.MeasureString("Labour ID:", labelFont).Width;

                g.DrawString(" " + labourValue, valueFont, blackBrush, currentX, detailY1);

                int detailY2 = 258;
                string contractorValue = passData.ContractorName ?? "N/A";

                SizeF contractorLabelSize = g.MeasureString("Contractor: ", labelFont);
                SizeF contractorValueSize = g.MeasureString(contractorValue, valueFont);
                float contractorStartX = centerX - ((contractorLabelSize.Width + contractorValueSize.Width) / 2);

                g.DrawString("Contractor:", labelFont, blackBrush, contractorStartX, detailY2);
                g.DrawString(" " + contractorValue, valueFont, blackBrush,
                    contractorStartX + contractorLabelSize.Width, detailY2);
            }
        }

        private static void DrawValidityAndAccessSection(Graphics g, PassModel passData)
        {
            using (Font sectionFont = new Font("Arial", 11, FontStyle.Bold))
            using (Font labelFont = new Font("Arial", 9, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 9, FontStyle.Regular))
            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
            {
                int sectionY = 288;
                int leftX = 50;
                int rightX = 270;

                g.DrawString("Validity:", sectionFont, blackBrush, leftX, sectionY);
                g.DrawString("From:", labelFont, blackBrush, leftX + 10, sectionY + 25);
                string fromDate = $"{passData.EntryDate ?? "N/A"} {passData.EntryTime ?? ""}".Trim();
                g.DrawString(fromDate, valueFont, blackBrush, leftX + 10, sectionY + 42);

                g.DrawString("To:", labelFont, blackBrush, leftX + 10, sectionY + 60);
                string toDate = $"{passData.ExitDate ?? "N/A"} {passData.CheckoutTime ?? ""}".Trim();
                g.DrawString(toDate, valueFont, blackBrush, leftX + 10, sectionY + 77);

                g.DrawString("Access Gates:", sectionFont, blackBrush, rightX, sectionY);
                g.DrawString("Areas:", labelFont, blackBrush, rightX + 10, sectionY + 25);
                string area = passData.Area ?? "N/A";
                g.DrawString(area, valueFont, blackBrush, rightX + 10, sectionY + 42);

                g.DrawString("Gates:", labelFont, blackBrush, rightX + 10, sectionY + 60);
                string gates = passData.GateAccess ?? "N/A";
                g.DrawString(gates, valueFont, blackBrush, rightX + 10, sectionY + 77);
            }
        }

        private static void DrawDigitalSignatureBadge(Graphics g, DigitalSignatureData signature)
        {
            int badgeX = 50;
            int badgeY = 390;
            int circleSize = 60;
            int circleOffsetY = 5; // Offset to move circle down separately

            using (SolidBrush greenBrush = new SolidBrush(Color.FromArgb(76, 175, 80)))
            {
                g.FillEllipse(greenBrush, badgeX, badgeY + circleOffsetY, circleSize, circleSize);
            }

            using (Pen checkPen = new Pen(Color.White, 5.5f))
            {
                checkPen.StartCap = LineCap.Round;
                checkPen.EndCap = LineCap.Round;
                checkPen.LineJoin = LineJoin.Round;

                using (GraphicsPath checkPath = new GraphicsPath())
                {
                    PointF p1 = new PointF(badgeX + 18, badgeY + circleOffsetY + 33);
                    PointF p2 = new PointF(badgeX + 26, badgeY + circleOffsetY + 42);
                    PointF p3 = new PointF(badgeX + 43, badgeY + circleOffsetY + 23);

                    checkPath.AddLine(p1, p2);
                    checkPath.AddLine(p2, p3);

                    g.DrawPath(checkPen, checkPath);
                }
            }

            using (Font approvedFont = new Font("Arial Narrow", 18, FontStyle.Bold))
            using (Font nameFont = new Font("Arial", 9, FontStyle.Bold))
            using (Font detailFont = new Font("Arial", 7.5f, FontStyle.Regular))
            using (Font signatureIdFont = new Font("Arial", 7f, FontStyle.Bold))
            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
            using (SolidBrush grayBrush = new SolidBrush(Color.FromArgb(102, 102, 102)))
            using (SolidBrush blueBrush = new SolidBrush(Color.FromArgb(63, 81, 181)))
            {
                int textX = badgeX + circleSize + 12;
                int textStartY = badgeY + 5;

                g.DrawString("DIGITALLY SIGNED", approvedFont, blackBrush, textX, textStartY);

                if (signature != null)
                {
                    // Increased spacing between "DIGITALLY SIGNED" and details
                    g.DrawString(signature.SignerName ?? "N/A", nameFont, grayBrush, textX - 2, textStartY + 38);
                    g.DrawString(signature.SignerTitle ?? "", detailFont, grayBrush, textX - 2, textStartY + 51);
                    g.DrawString(signature.SignerOrganization ?? "", detailFont, grayBrush, textX - 2, textStartY + 63);

                    string dateTimeStr = signature.SignedDate.ToString("dd-MMM-yyyy HH:mm:ss");
                    g.DrawString($"Date: {dateTimeStr}", detailFont, grayBrush, textX - 2, textStartY + 75);
                    g.DrawString($"Signature ID: {signature.SignatureId ?? "N/A"}", signatureIdFont, blueBrush, textX - 2, textStartY + 87);
                }
                else
                {
                    g.DrawString("Awaiting Approval", detailFont, grayBrush, textX - 2, textStartY + 38);
                }
            }
        }

        private static void DrawFooter(Graphics g)
        {
            using (SolidBrush blueBrush = new SolidBrush(Color.FromArgb(63, 81, 181)))
            using (Font footerFont = new Font("Arial", 16, FontStyle.Bold))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(blueBrush, 0, 520, PassWidth, 44);

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString("TEMPORARY PASS", footerFont, whiteBrush,
                    new RectangleF(0, 520, PassWidth, 44), sf);
            }
        }

        private static string SavePass(Bitmap passBitmap, string laborId)
        {
            string passesFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "CIAL_Entry_Passes"
            );

            Directory.CreateDirectory(passesFolder);

            string fileName = $"Pass_{laborId ?? "Unknown"}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string fullPath = Path.Combine(passesFolder, fileName);

            try
            {
                passBitmap.Save(fullPath, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        passBitmap.Save(ms, ImageFormat.Png);
                        File.WriteAllBytes(fullPath, ms.ToArray());
                    }
                }
                catch
                {
                    throw new Exception($"Error saving pass image to {fullPath}: {ex.Message}", ex);
                }
            }

            return fullPath;
        }
    }
}