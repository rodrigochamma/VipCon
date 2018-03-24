using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VipCon.Data;
using VipCon.DTO;
using VipCon.Models;

namespace VipCon.Pages.Account.Manage
{
    public class ListaUsuario : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ListaUsuario(VipCon.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IList<UsuarioParaListagemDTO> UsuarioParaListagemDTO { get;set; }

        public async Task OnGetAsync()
        {
            IList<ApplicationUser> usuarios = await _userManager.Users.ToListAsync();
            
            UsuarioParaListagemDTO = _mapper.Map<IList<UsuarioParaListagemDTO>>(usuarios);
        }
    }
}
