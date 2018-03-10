using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VipCon.Data;
using VipCon.Models;

namespace VipCon.Pages.Noticias
{
    public class DeleteModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public DeleteModel(VipCon.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Noticia = await _context.Noticia.FindAsync(id);

            if (Noticia != null)
            {
                _context.Noticia.Remove(Noticia);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
