﻿using System;
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
    public class EventsController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public EventsController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents(GeoPoint location, double? radius, int? genre, int? host, int? artist)
        {
            DBInitializer.Run();

            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            if (location == null && !genre.HasValue && !host.HasValue && !artist.HasValue)
                return await _context.Events.Where(e => e.HostId == authorizedUser.Value.Id || e.Participations.Any(p => p.ArtistId == authorizedUser.Value.Id)).ToListAsync();

            var query = _context.Events;
            if (location != null && radius.HasValue)
                query.Where(e => GeoPoint.CalculateDistance(location, new GeoPoint() { Longitude = e.Longitude, Latitude = e.Latitude }) <= radius.Value);
            if (genre.HasValue)
                query.Where(e => e.EventGenres.Any(eg => eg.GenreId == genre));
            if (host.HasValue)
                query.Where(e => e.HostId == host);
            if (artist.HasValue)
                query.Where(e => e.Participations.Any(p => p.ArtistId == artist));

            return await query.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
                return NotFound();

            return @event;
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (id != @event.Id)
                return BadRequest();
            if (authorizedUser.Value == null)
                return Unauthorized();
            if (@event.HostId != authorizedUser.Value.Id)
                return Unauthorized();

            //SetEventGenres(@event);
            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                    return NotFound();
                else
                    throw;
            }
            return Ok();
        }

        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (@event.Host == null)
                @event.HostId = authorizedUser.Value.Id;
            if (@event.HostId != authorizedUser.Value.Id)
                return Unauthorized();

            //SetEventGenres(@event);
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            var @event = await _context.Events.FindAsync(id);

            if (@event.HostId != authorizedUser.Value.Id)
                return Unauthorized();
            if (@event == null)
                return NotFound();

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        private void SetEventGenres(Event @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            foreach (var hostGenre in @event.EventGenres)
                @event.EventGenres.Remove(hostGenre);

            if (@event.GenreIds == null || @event.GenreIds.Count == 0)
                return;

            foreach (int genreId in @event.GenreIds)
                @event.EventGenres.Add(new EventGenre() { EventId = @event.Id, GenreId = genreId });
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
