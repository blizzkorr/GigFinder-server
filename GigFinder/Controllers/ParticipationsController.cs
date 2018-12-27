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
                return await _context.Participations.Where(p => p.ArtistId == authorizedUser.Value.Id || p.Event.HostId == authorizedUser.Value.Id).ToListAsync();

            var query = _context.Participations;
            if (@event.HasValue)
                query.Where(p => p.EventId == @event);
            if (host.HasValue)
                query.Where(p => p.Event.HostId == host);
            if (artist.HasValue)
                query.Where(p => p.ArtistId == artist);

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
            if (!(participation.ArtistId == authorizedUser.Value.Id || @event.HostId == authorizedUser.Value.Id))
                return Unauthorized();
            
            _context.Participations.Add(participation);
            try
            {
                await _context.SaveChangesAsync();
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

            return participation;
        }

        private bool ParticipationExists(int id)
        {
            return _context.Participations.Any(e => e.Id == id);
        }
    }
}