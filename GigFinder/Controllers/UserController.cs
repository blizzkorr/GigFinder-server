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
    public class UserController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public UserController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<object>> GetUser()
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            if (authorizedUser.Value == null)
                return Unauthorized();
            else if (authorizedUser.Value.Artist == null && authorizedUser.Value.Host != null)
                return authorizedUser.Value.Host;
            else if (authorizedUser.Value.Artist != null && authorizedUser.Value.Host == null)
                return authorizedUser.Value.Artist;
            else
                return new { authorizedUser.Value.Artist, authorizedUser.Value.Host };
        }
    }
}