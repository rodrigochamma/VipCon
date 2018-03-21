using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VipCon.Data;
using VipCon.Models;

namespace VipCon.Pages.Parceiros
{
    public class DeleteModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public DeleteModel(VipCon.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Parceiro Parceiro { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Parceiro = await _context.Parceiro.SingleOrDefaultAsync(m => m.Id == id);

            if (Parceiro == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Parceiro = await _context.Parceiro.FindAsync(id);

            if (Parceiro != null)
            {
                _context.Parceiro.Remove(Parceiro);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
