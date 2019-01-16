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
    public class ReviewsController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public ReviewsController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int? host, int? artist)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            if (!host.HasValue && !artist.HasValue)
                return await _context.Reviews.Where(r => r.AuthorId == authorizedUser.Value.Id).ToListAsync();
            
            var query = (IQueryable<Review>)_context.Reviews;
            if (host.HasValue)
                query = query.Where(r => r.HostId == host);
            if (artist.HasValue)
                query = query.Where(r => r.ArtistId == artist);

            return await query.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            if (!Authentication.AuthenticateAsync(Request).Result)
                return Unauthorized();

            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
                return NotFound();

            return review;
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (review.AuthorId != authorizedUser.Value.Id)
                return Unauthorized();
            if (id != review.Id)
                return BadRequest();

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                    return NotFound();
                else
                    throw;
            }
            return Ok();
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (review.AuthorId != authorizedUser.Value.Id)
                return Unauthorized();

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            var review = await _context.Reviews.FindAsync(id);

            if (review.AuthorId != authorizedUser.Value.Id)
                return Unauthorized();
            if (review == null)
                return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return review;
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
