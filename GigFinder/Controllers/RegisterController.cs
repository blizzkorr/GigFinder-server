using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigFinder.Models;
using GigFinder.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public RegisterController(GigFinderContext context)
        {
            _context = context;
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> PostEvent(User user)
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            var payload = await GoogleServices.GetTokenPayloadAsync(Request.Headers["Authorization"].First());
            if (string.IsNullOrEmpty(user.GoogleIdToken))
                user.GoogleIdToken = payload.JwtId;

            if (user is Artist artist)
                _context.Artists.Add(artist);
            else if (user is Host host)
                _context.Hosts.Add(host);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}