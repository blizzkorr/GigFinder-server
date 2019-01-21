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
    public class LoginController : ControllerBase
    {
        private readonly GigFinderContext _context;

        public LoginController(GigFinderContext context)
        {
            _context = context;
        }

        // GET: api/Login
        [HttpGet]
        public async Task<ActionResult<bool>> GetLogin()
        {
            var authorizedUser = await Authentication.GetAuthenticatedUserAsync(_context, Request);
            if (authorizedUser.Result is UnauthorizedResult)
                return Unauthorized();

            return authorizedUser.Value != default(UserID);
        }
    }
}