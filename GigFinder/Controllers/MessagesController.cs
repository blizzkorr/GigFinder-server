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
    public class MessagesController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public MessagesController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(int? receiver)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            using (var db = new GigFinderContext())
            {
                if (!receiver.HasValue)
                    return await db.Messages.Where(m => m.AuthorId == authorizedUser.Value.Id || m.ReceiverId == authorizedUser.Value.Id).ToListAsync();
                else
                    return await db.Messages.Where(m => (m.AuthorId == authorizedUser.Value.Id && m.ReceiverId == receiver.Value)
                        || (m.AuthorId == receiver.Value && m.ReceiverId == authorizedUser.Value.Id)).ToListAsync();
            }
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            var message = await _context.Messages.FindAsync(id);

            if (!(message.AuthorId == authorizedUser.Value.Id || message.ReceiverId == authorizedUser.Value.Id))
                return Unauthorized();
            if (message == null)
                return NotFound();

            return message;
        }

        // PUT: api/Messages/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMessage(int id, Message message)
        //{
        //    if (!Authentication.AuthenticateAsync(Request).Result)
        //        return Unauthorized();
        //    
        //    if (id != message.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(message).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MessageExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        
        //    return Ok();
        //}

        // POST: api/Messages
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (message.AuthorId != authorizedUser.Value.Id)
                return Unauthorized();

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            if (message != null)
                Task.Run(() => NotifyReceiverAsync(message.Id));
                //await NotifyReceiverAsync(message.Id);

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Message>> DeleteMessage(int id)
        //{
        //    if (!Authentication.AuthenticateAsync(Request).Result)
        //        return Unauthorized();
        //    
        //    var message = await _context.Messages.FindAsync(id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Messages.Remove(message);
        //    await _context.SaveChangesAsync();

        //    return message;
        //}

        private async Task NotifyReceiverAsync(int id)
        {
            using (var db = new GigFinderContext())
            {
                Message message = db.Messages.Include(m => m.Author.Host).Include(m => m.Author.Artist).SingleOrDefault(m => m.Id == id);
                if (message != null) 
                    await GoogleServices.SendFCMAsync(message.Receiver.DeviceToken, $"New message from {message.Author.Host?.Name ?? message.Author.Artist?.Name}", message.Content);
            }
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
