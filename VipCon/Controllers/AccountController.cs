using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VipCon.Data;

namespace VipCon.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly RoleManager<MyIdentityRole> _roleManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, RoleManager<MyIdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }
    }
}
