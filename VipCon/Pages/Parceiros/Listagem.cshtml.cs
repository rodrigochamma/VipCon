using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VipCon.Models;

namespace VipCon.Pages.Parceiros
{
    public class ListagemModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public ListagemModel(VipCon.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Parceiro> Parceiro { get; set; }

        public async Task OnGetAsync()
        {
            Parceiro = await _context.Parceiro.ToListAsync();
        }
    }
}