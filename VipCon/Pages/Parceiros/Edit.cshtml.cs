using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VipCon.Data;
using VipCon.Models;

namespace VipCon.Pages.Parceiros
{
    public class EditModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public EditModel(VipCon.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Parceiro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParceiroExists(Parceiro.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ParceiroExists(int id)
        {
            return _context.Parceiro.Any(e => e.Id == id);
        }
    }
}
