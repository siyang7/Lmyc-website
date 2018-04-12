using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Lmyc.Models;
using Lmyc.Models.AccountViewModels;
using Lmyc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lmyc.Controllers.API
{
    [Produces("application/json")]
    [Route("api/AccountAPI")]
    public class AccountAPIController : Controller
    { 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public AccountAPIController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        // POST api/AccountAPI/Register
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                HomePhone = model.HomePhone,
                UserName = model.Email,
                Email = model.Email,
                Street = model.Street,
                City = model.City,
                Province = model.Province,
                Country = model.Country,
                PostalCode = model.PostalCode,
                SailingExperience = model.SailingExperience,
                Skills = model.Skills,
                SailingQualifications = model.SailingQualifications,
                EmergencyContactOne = model.EmergencyContactOne,
                StartingCredit = 320
            };

            var result = await _userManager.CreateAsync(user, model.Password);


            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}