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
    public class HostsController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public HostsController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Hosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Host>>> GetHosts()
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            return await _context.Hosts.Include(h => h.HostSocialMedias)
                .Where(h => h.Id == authorizedUser.Value.Id).ToListAsync();
        }

        // GET: api/Hosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Host>> GetHost(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();
            if (authorizedUser.Value == null)
                return Unauthorized();

            var host = await _context.Hosts.Include(h => h.HostSocialMedias).SingleOrDefaultAsync(h => h.Id == id);

            if (host == null)
                return NotFound();
            if (host.Id != authorizedUser.Value.Id)
                return Unauthorized();

            return host;
        }

        // PUT: api/Hosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHost(int id, Host host)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (authorizedUser.Value.Id != host.Id)
                return Unauthorized();
            if (id != host.Id)
                return BadRequest();

            //SetHostGenres(host);
            _context.Entry(host).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HostExists(id))
                    return NotFound();
                else
                    throw;
            }
            return Ok();
        }

        // POST: api/Artists
        [HttpPost]
        public async Task<ActionResult<Host>> PostHost(Host host)
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            var payload = await GoogleServices.GetTokenPayloadAsync(Request.Headers["Authorization"].First());
            UserID existingUser = _context.UserIDs.SingleOrDefault(u => u.GoogleIdToken == payload.Subject);
            if (existingUser != null)
                host.UserId = existingUser;
            else
                host.UserId = new UserID() { GoogleIdToken = payload.Subject };

            //SetHostGenres(host);
            _context.Hosts.Add(host);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHost", new { id = host.Id }, host);
        }

        // DELETE: api/Hosts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Host>> DeleteHost(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            var host = await _context.Hosts.FindAsync(id);

            if (authorizedUser.Value.Id != host.Id)
                return Unauthorized();
            if (host == null)
                return NotFound();

            _context.Hosts.Remove(host);
            if (authorizedUser.Value.Artist == null)
                _context.UserIDs.Remove(authorizedUser.Value);
            await _context.SaveChangesAsync();

            return host;
        }

        private void SetHostGenres(Host host)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));

            foreach(var hostGenre in host.HostGenres)
                host.HostGenres.Remove(hostGenre);

            if (host.GenreIds == null || host.GenreIds.Count == 0)
                return;

            foreach (int genreId in host.GenreIds)
                host.HostGenres.Add(new HostGenre() { HostId = host.Id, GenreId = genreId });
        }

        private bool HostExists(int id)
        {
            return _context.Hosts.Any(e => e.Id == id);
        }
    }
}
