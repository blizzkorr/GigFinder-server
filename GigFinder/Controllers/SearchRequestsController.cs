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
    public class SearchRequestsController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public SearchRequestsController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/SearchRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchRequest>>> GetSearchRequests()
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            
            return await _context.SearchRequests.Where(sr => sr.ArtistId == authorizedUser.Value.Id).ToListAsync();
        }

        // GET: api/SearchRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SearchRequest>> GetSearchRequest(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            var searchRequest = await _context.SearchRequests.FindAsync(id);

            if (searchRequest.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();
            if (searchRequest == null)
                return NotFound();

            return searchRequest;
        }

        // PUT: api/SearchRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSearchRequest(int id, SearchRequest searchRequest)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (searchRequest.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();
            if (id != searchRequest.Id)
                return BadRequest();

            _context.Entry(searchRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchRequestExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/SearchRequests
        [HttpPost]
        public async Task<ActionResult<SearchRequest>> PostSearchRequest(SearchRequest searchRequest)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (searchRequest.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();

            _context.SearchRequests.Add(searchRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSearchRequest", new { id = searchRequest.Id }, searchRequest);
        }

        // DELETE: api/SearchRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SearchRequest>> DeleteSearchRequest(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            var searchRequest = await _context.SearchRequests.FindAsync(id);

            if (searchRequest.ArtistId != authorizedUser.Value.Id)
                return Unauthorized();
            if (searchRequest == null)
                return NotFound();

            _context.SearchRequests.Remove(searchRequest);
            await _context.SaveChangesAsync();

            return searchRequest;
        }

        private bool SearchRequestExists(int id)
        {
            return _context.SearchRequests.Any(e => e.Id == id);
        }
    }
}
