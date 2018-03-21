using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VipCon.Data;
using VipCon.Models;

namespace VipCon.Pages.Parceiros
{
    public class CreateModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public CreateModel(VipCon.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Parceiro Parceiro { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Parceiro.Add(Parceiro);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}