using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entry_Pass_Generator_CIAL
{
    public static class PassSender
    {
        private static readonly HttpClient client = new HttpClient();

        // Update this to match your ASP.NET middle layer API
        private const string API_BASE_URL = "http://localhost:5135";
        private const string SEND_PASS_ENDPOINT = "/api/passes";  // Adjust to your actual endpoint

        public static async Task<bool> SendPassForApproval(PassModel passModel)
        {
            try
            {
                // Set Status to Pending before sending
                passModel.Status = "Pending";

                // Serialize PassModel to JSON
                string json = JsonSerializer.Serialize(passModel, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Use camelCase for consistency
                    WriteIndented = true
                });

                // Debug output
                System.Diagnostics.Debug.WriteLine($"[{DateTime.Now:HH:mm:ss}] Sending pass to API");
                System.Diagnostics.Debug.WriteLine($"URL: {API_BASE_URL}{SEND_PASS_ENDPOINT}");
                System.Diagnostics.Debug.WriteLine($"JSON Data: {json}");

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                string fullUrl = $"{API_BASE_URL}{SEND_PASS_ENDPOINT}";
                HttpResponseMessage response = await client.PostAsync(fullUrl, content);

                string responseBody = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine($"Response Status: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"Response Body: {responseBody}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        $"Pass sent successfully!\n\nLabor ID: {passModel.LaborID}\nName: {passModel.FullName}\n\nStatus: Pending Approval",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show(
                        $"Failed to send pass.\n\nStatus Code: {response.StatusCode}\nReason: {response.ReasonPhrase}\n\nServer Response:\n{responseBody}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show(
                    $"Cannot connect to the API server.\n\nPlease ensure:\n" +
                    $"1. ASP.NET API is running on port 5135\n" +
                    $"2. URL is correct: {API_BASE_URL}\n" +
                    $"3. Firewall is not blocking the connection\n\n" +
                    $"Error Details:\n{httpEx.Message}",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                System.Diagnostics.Debug.WriteLine($"HttpRequestException: {httpEx.Message}");
                return false;
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "Request timed out.\n\nThe API server is not responding.\nPlease check if the API is running.",
                    "Timeout Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show(
                    $"Error serializing pass data.\n\nDetails: {jsonEx.Message}",
                    "JSON Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                System.Diagnostics.Debug.WriteLine($"JsonException: {jsonEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An unexpected error occurred.\n\nError Type: {ex.GetType().Name}\nDetails: {ex.Message}",
                    "Unexpected Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                return false;
            }
        }
    }
}