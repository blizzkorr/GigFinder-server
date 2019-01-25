using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigFinder.Models;
using GigFinder.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTokenController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public DeviceTokenController(GigFinderContext context)
        {
            _context = context;
        }

        // PUT: api/DeviceToken/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceToken(int id, [FromBody] string deviceToken)
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            if (authorizedUser.Value.Id != id)
                return Unauthorized();

            _context.UserIDs.Find(id).DeviceToken = deviceToken;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserIDExists(id))
                    return NotFound();
                else
                    throw;
            }
            return Ok();
        }

        private bool UserIDExists(int id)
        {
            return _context.UserIDs.Any(ui => ui.Id == id);
        }
    }
}