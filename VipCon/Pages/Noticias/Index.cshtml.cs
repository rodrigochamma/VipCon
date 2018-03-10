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
    public class IndexModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public IndexModel(VipCon.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Noticia> Noticia { get;set; }

        public async Task OnGetAsync()
        {
            Noticia = await _context.Noticia.ToListAsync();
        }
    }
}
