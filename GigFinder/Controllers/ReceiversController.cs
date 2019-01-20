using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigFinder.Models;
using GigFinder.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GigFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiversController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public ReceiversController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetReceivers()
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();

            using (var db = new GigFinderContext())
            {
                IEnumerable<int> receiverIds = db.Messages.Where(m => m.AuthorId == authorizedUser.Value.Id).Select(m => m.ReceiverId).Concat(db.Messages.Where(m => m.ReceiverId == authorizedUser.Value.Id).Select(m => m.AuthorId)).Distinct();
                List<object> output = new List<object>();

                foreach (int receiverId in receiverIds)
                {
                    Artist receivingArtist = await db.Artists.FindAsync(receiverId);
                    Host receivingHost = await db.Hosts.FindAsync(receiverId);
                    Message lastMessage = db.Messages.Where(m => (m.AuthorId == authorizedUser.Value.Id && m.ReceiverId == receiverId) || (m.ReceiverId == authorizedUser.Value.Id && m.AuthorId == receiverId)).OrderBy(m => m.Created).Last();

                    if (receivingArtist != null)
                        output.Add(new { Artist = receivingArtist, LastMessage = lastMessage });
                    else if (receivingHost != null)
                        output.Add(new { Host = receivingHost, LastMessage = lastMessage });
                    else
                        output.Add(new { Id = receiverId, LastMessage = lastMessage });
                }
                return output;
            }
        }
    }
}