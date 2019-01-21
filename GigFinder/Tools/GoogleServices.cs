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
                if (string.IsNullOrEmpty(token))
                    return null;
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
                if (string.IsNullOrWhiteSpace(to))
                    throw new ArgumentNullException(nameof(to));

                var serverKey = $"key={""}";
                var senderId = $"id={""}";

                var data = new
                {
                    to,
                    notification = new { title, body }
                };

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                            return true;
                        else
                            Console.WriteLine($"Error sending notification. Status Code: {result.StatusCode}");
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
