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

namespace VipCon.Pages.Noticias
{
    public class EditModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public EditModel(VipCon.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Noticia Noticia { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Noticia = await _context.Noticia.SingleOrDefaultAsync(m => m.ID == id);

            if (Noticia == null)
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

            _context.Attach(Noticia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(Noticia.ID))
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

        private bool NoticiaExists(int id)
        {
            return _context.Noticia.Any(e => e.ID == id);
        }
    }
}
