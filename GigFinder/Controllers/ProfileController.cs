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
    public class ProfileController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public ProfileController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Profile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetProfiles()
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            //var query = _context.Users;

            return await _context.Users.ToListAsync();
        }
    }
}