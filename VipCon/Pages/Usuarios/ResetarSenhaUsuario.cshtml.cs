using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VipCon.Data;
using VipCon.Models;

namespace VipCon.Pages.Usuarios
{
    public class ResetarSenhaModel : PageModel
    {        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly VipCon.Data.ApplicationDbContext _context;

        public ResetarSenhaModel(VipCon.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {            
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser = await _userManager.FindByEmailAsync(id);            

            if (ApplicationUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser = await _userManager.FindByEmailAsync(id);

            if (ApplicationUser != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(ApplicationUser);
                await _userManager.ResetPasswordAsync(ApplicationUser, code, "vipcon123");                
            }

            return RedirectToPage("./ListaUsuario");
        }
    }
}
