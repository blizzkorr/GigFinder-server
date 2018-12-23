using GigFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Tools
{
    public static class Authentication
    {
        public static async Task<bool> AuthenticateAsync(HttpRequest httpRequest)
        {
            string tokenString = "";
            if (httpRequest.Headers.Any(h => h.Key == "Authorization"))
                tokenString = httpRequest.Headers["Authorization"].First();

            return await GoogleServices.ValidateTokenAsync(tokenString);
        }

        public static async Task<ActionResult<User>> GetAuthenticatedUserAsync(GigFinderContext context, HttpRequest httpRequest)
        {
            string tokenString = "";
            if (httpRequest.Headers.Any(h => h.Key == "Authorization"))
                tokenString = httpRequest.Headers["Authorization"].First();

            var payload = await GoogleServices.GetTokenPayloadAsync(tokenString);
            if (payload == null)
                return new UnauthorizedResult();

            return context.Users.SingleOrDefault(u => u.GoogleIdToken == payload.JwtId);
        }
    }
}
