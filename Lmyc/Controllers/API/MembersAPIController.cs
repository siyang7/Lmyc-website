using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lmyc.Data;
using Lmyc.Models;
using Lmyc.Models.UserViewModels;
using Microsoft.AspNetCore.Identity;

namespace Lmyc.Controllers.API
{
    [Produces("application/json")]
    [Route("api/MembersAPI")]
    public class MembersAPIController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public MembersAPIController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/MembersAPI
        [HttpGet]
        public IEnumerable<UserViewModel> GetMembers()
        {
            var users = _userManager.Users;

            var models = new List<UserViewModel>();
            
            foreach (var u in users)
            {
                var model = new UserViewModel
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = string.Join(", ", _userManager.GetRolesAsync(u).Result)
                };

                models.Add(model);
            }

            return models;
        }

        // GET: api/MembersAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }
    }
}