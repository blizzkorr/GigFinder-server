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
    public class PicturesController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public PicturesController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Pictures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPicture(int? @event, int? host, int? artist)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            if (@event.HasValue)
                return await _context.Picture.Where(p => p.EventId == @event.Value).ToListAsync();
            else if (host.HasValue)
                return await _context.Picture.Where(p => p.HostId == host.Value).ToListAsync();
            else if (artist.HasValue)
                return await _context.Picture.Where(p => p.ArtistId == artist.Value).ToListAsync();
            else
                return BadRequest();
        }

        // GET: api/Pictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            var picture = await _context.Picture.FindAsync(id);

            if (picture == null)
                return NotFound();

            return picture;
        }

        // PUT: api/Pictures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(int id, Picture picture)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (picture.ArtistId.HasValue && picture.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();
            else if (picture.HostId.HasValue && picture.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();
            else if (picture.EventId.HasValue)
            {
                Event @event = await _context.Events.FindAsync(picture.EventId.Value);
                if (@event == null)
                    return BadRequest();
                else if (@event.HostId != authorizedUser.Value.Id)
                    return Unauthorized();
            }
            if (id != picture.Id)
                return BadRequest();

            _context.Entry(picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
                    return NotFound();
                else
                    throw;
            }
            return Ok();
        }

        // POST: api/Pictures
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(Picture picture)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (picture.ArtistId.HasValue && picture.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();
            else if (picture.HostId.HasValue && picture.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();
            else if (picture.EventId.HasValue)
            {
                Event @event = await _context.Events.FindAsync(picture.EventId.Value);
                if (@event == null)
                    return BadRequest();
                else if (@event.HostId != authorizedUser.Value.Id)
                    return Unauthorized();
            }

            _context.Picture.Add(picture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPicture", new { id = picture.Id }, picture);
        }

        // DELETE: api/Pictures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Picture>> DeletePicture(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            var picture = await _context.Picture.FindAsync(id);

            if (picture == null)
                return NotFound();
            if (picture.ArtistId.HasValue && picture.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();
            else if (picture.HostId.HasValue && picture.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();
            else if (picture.EventId.HasValue)
            {
                Event @event = await _context.Events.FindAsync(picture.EventId.Value);
                if (@event == null)
                    return BadRequest();
                else if (@event.HostId != authorizedUser.Value.Id)
                    return Unauthorized();
            }

            _context.Picture.Remove(picture);
            await _context.SaveChangesAsync();

            return picture;
        }

        private bool PictureExists(int id)
        {
            return _context.Picture.Any(e => e.Id == id);
        }
    }
}
