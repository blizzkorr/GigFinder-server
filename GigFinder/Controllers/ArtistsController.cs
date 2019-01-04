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
    public class ArtistsController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public ArtistsController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            return _context.Artists.Where(u => u.Id == authorizedUser.Value.Id).ForEach(u => u.Anonymize()).ToList();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();
            if (authorizedUser.Value == null)
                return Unauthorized();

            var artist = await _context.Artists.FindAsync(id);

            if (artist.Id != authorizedUser.Value.Id)
                return Unauthorized();
            if (artist == null)
                return NotFound();

            return artist;
        }

        // PUT: api/Artists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (authorizedUser.Value.Id != artist.Id)
                return Unauthorized();
            if (id != artist.Id)
                return BadRequest();

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
                    return NotFound();
                else
                    throw;
            }
            return Ok();
        }

        // POST: api/Artists
        [HttpPost]
        public async Task<IActionResult> PostEvent(Artist artist)
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            var payload = await GoogleServices.GetTokenPayloadAsync(Request.Headers["Authorization"].First());
            if (string.IsNullOrEmpty(artist.GoogleIdToken))
                artist.GoogleIdToken = payload.JwtId;

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> DeleteArtist(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            var artist = await _context.Artists.FindAsync(id);

            if (authorizedUser.Value.Id != artist.Id)
                return Unauthorized();
            if (artist == null)
                return NotFound();

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return artist;
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
