// File: HttpClientHelper.cs

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entry_Pass_Generator_CIAL
{
    public class HttpClientHelper
    {
        public static async Task SendPassDataAsync(PassModel passData)
        {
            using (HttpClient client = new HttpClient())
            {
                // Define the API URL (replace this with your actual URL)
                string apiUrl = "http://localhost:5135/api/approve-pass";  // Update this with your actual endpoint

                // Serialize the PassModel object to JSON
                string json = JsonSerializer.Serialize(passData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the HTTP POST request
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Pass Data Sent Successfully!");
                }
                else
                {
                    MessageBox.Show("Error sending data.");
                }
            }
        }
    }
}
