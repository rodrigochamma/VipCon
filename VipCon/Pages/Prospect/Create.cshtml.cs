using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VipCon.Data;

namespace VipCon.Pages.Prospect
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
        public VipCon.Data.Prospect Prospect { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Prospect.Add(Prospect);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}