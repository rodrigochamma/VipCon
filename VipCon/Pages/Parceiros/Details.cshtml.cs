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
    public class DetailsModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public DetailsModel(VipCon.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
