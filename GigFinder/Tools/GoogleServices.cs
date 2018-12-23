using Google.Apis.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GigFinder.Tools
{
    public static class GoogleServices
    {
        public static async Task<GoogleJsonWebSignature.Payload> GetTokenPayloadAsync(string token)
        {
            try
            { 
                return await GoogleJsonWebSignature.ValidateAsync(token);
            }
            catch (InvalidJwtException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return false;
                return await GoogleJsonWebSignature.ValidateAsync(token) != null;
            }
            catch (InvalidJwtException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static async Task<bool> SendFCMAsync(string to, string title, string body)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "Your server key - use app config");
                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "Your sender id - use app config");

                var data = new
                {
                    to, // Recipient device token
                    notification = new { title, body }
                };

                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                            return true;
                        else
                        {
                            // Use result.StatusCode to handle failure
                            // Your custom error handler here
                            Console.WriteLine($"Error sending notification. Status Code: {result.StatusCode}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown in Notify Service: {ex}");
            }
            return false;
        }
    }
}
