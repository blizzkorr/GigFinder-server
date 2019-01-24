using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GigFinder.Tools;
using GigFinder.Models;

namespace GigFinder.Controllers
{
    [Route("api/[controller]")]
    public class UpdatesController : Controller
    {
        private readonly GigFinderContext _context;

        public UpdatesController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/updates
        [HttpGet]
        public async Task<IActionResult> GetUpdates()
        {
            DBInitializer.Run();

            if (_context.SocialMedias.SingleOrDefault(s => s.Name == "Instagram") == null)
                _context.SocialMedias.Add(new SocialMedia() { Name = "Instagram", Website = "https://www.instagram.com/" });

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
