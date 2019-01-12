using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GigFinder.Models;
using GigFinder.Tools;

namespace GigFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public SocialMediasController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/SocialMedias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMedias()
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            return await _context.SocialMedias.ToListAsync();
        }

        // GET: api/SocialMedias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocialMedia>> GetSocialMedia(int id)
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            var socialMedia = await _context.SocialMedias.FindAsync(id);

            if (socialMedia == null)
                return NotFound();

            return socialMedia;
        }
    }
}
