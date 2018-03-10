using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VipCon.Data;

namespace VipCon.Pages.Prospect
{
    public class IndexModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public IndexModel(VipCon.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<VipCon.Data.Prospect> Prospect { get;set; }

        public async Task OnGetAsync()
        {
            Prospect = await _context.Prospect.ToListAsync();
        }
    }
}
