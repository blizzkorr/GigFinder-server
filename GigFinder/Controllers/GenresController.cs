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
    public class GenresController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public GenresController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenre()
        {
            DBInitializer.Run();

            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            return await _context.Genres.ToListAsync();
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
                return NotFound();

            return genre;
        }
    }
}
