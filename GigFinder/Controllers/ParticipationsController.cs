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
    public class ParticipationsController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public ParticipationsController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Participations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participation>>> GetParticipations(int? @event, int? host, int? artist)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            if (!@event.HasValue && !host.HasValue && !artist.HasValue)
                return await _context.Participations.Include(p => p.Event).Include(p => p.Event.EventGenres).Where(p => p.ArtistId == authorizedUser.Value.Id || p.Event.HostId == authorizedUser.Value.Id).ToListAsync();

            var query = (IQueryable<Participation>)_context.Participations;
            if (@event.HasValue)
                query = query.Include(p => p.Artist).Where(p => p.EventId == @event);
            if (host.HasValue)
                query = query.Include(p => p.Artist).Include(p => p.Event).Include(p => p.Event.EventGenres).Where(p => p.Event.HostId == host);
            if (artist.HasValue)
                query = query.Include(p => p.Event).Where(p => p.ArtistId == artist);

            return await query.ToListAsync();
        }

        // GET: api/Participations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participation>> GetParticipation(int id)
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            var participation = await _context.Participations.FindAsync(id);

            if (participation == null)
                return NotFound();

            return participation;
        }

        // PUT: api/Participations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipation(int id, Participation participation)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            var @event = await _context.Events.FindAsync(participation.EventId);

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (!(participation.ArtistId == authorizedUser.Value.Id || @event.HostId == authorizedUser.Value.Id))
                return Unauthorized();
            if (id != participation.Id)
                return BadRequest();

            _context.Entry(participation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                
                if (participation.ArtistId == authorizedUser.Value.Id)
                    Task.Run(() => NotifyHostUpdateAsync(participation.Id));
                else if (participation.Event.HostId == authorizedUser.Value.Id)
                    Task.Run(() => NotifyArtistUpdateAsync(participation.Id));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipationExists(id))
                    return NotFound();
                else
                    throw;
            }
            return Ok();
        }

        // POST: api/Participations
        [HttpPost]
        public async Task<ActionResult<Participation>> PostParticipation(Participation participation)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            var @event = await _context.Events.FindAsync(participation.EventId);

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (participation.ArtistId == 0)
                participation.ArtistId = authorizedUser.Value.Id;
            if (!(participation.ArtistId == authorizedUser.Value.Id || @event.HostId == authorizedUser.Value.Id))
                return Unauthorized();
            
            _context.Participations.Add(participation);
            try
            {
                await _context.SaveChangesAsync();

                if (participation.ArtistId == authorizedUser.Value.Id)
                    Task.Run(() => NotifyHostAsync(participation.Id));
            }
            catch (DbUpdateException)
            {
                if (ParticipationExists(participation.Id))
                    return Conflict();
                else
                    throw;
            }

            return CreatedAtAction("GetParticipation", new { id = participation.Id }, participation);
        }

        // DELETE: api/Participations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Participation>> DeleteParticipation(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();
            
            var participation = await _context.Participations.FindAsync(id);
            if (authorizedUser.Value == null)
                return Unauthorized();
            if (!(participation.ArtistId == authorizedUser.Value.Id || participation.Event.HostId == authorizedUser.Value.Id))
                return Unauthorized();
            if (participation == null)
                return NotFound();

            _context.Participations.Remove(participation);
            await _context.SaveChangesAsync();

            // Notify Host or Artist

            return participation;
        }

        private async Task NotifyHostAsync(int id)
        {
            using (var db = new GigFinderContext())
            {
                Participation participation = db.Participations.Include(p => p.Event.Host.UserId).Include(p => p.Artist).SingleOrDefault(m => m.Id == id);
                if (participation != null)
                    await GoogleServices.SendFCMAsync(participation.Event.Host.UserId.DeviceToken, "New participation request", $"{participation.Artist.Name} wants to participate in your event {participation.Event.Title}");
            }            
        }

        private async Task NotifyHostUpdateAsync(int id)
        {
            using (var db = new GigFinderContext())
            {
                Participation participation = db.Participations.Include(p => p.Event.Host.UserId).Include(p => p.Artist).SingleOrDefault(m => m.Id == id);
                if (participation != null)
                    await GoogleServices.SendFCMAsync(participation.Event.Host.UserId.DeviceToken, "New participation update", $"{participation.Artist.Name} has updated the participation at your event {participation.Event.Title}");
            }
        }

        private async Task NotifyArtistUpdateAsync(int id)
        {
            using (var db = new GigFinderContext())
            {
                Participation participation = db.Participations.Include(p => p.Artist.UserId).Include(p => p.Event).SingleOrDefault(m => m.Id == id);
                if (participation != null)
                {
                    string partString = participation.Accepted ? "accepted" : "declined";
                    await GoogleServices.SendFCMAsync(participation.Artist.UserId.DeviceToken, "New participation update", $"Your participation at {participation.Event.Title} has been {partString}");
                }
            }
        }

        private bool ParticipationExists(int id)
        {
            return _context.Participations.Any(e => e.Id == id);
        }
    }
}
