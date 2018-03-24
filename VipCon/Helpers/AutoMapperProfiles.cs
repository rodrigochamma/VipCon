using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VipCon.Data;
using VipCon.DTO;

namespace VipCon.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UsuarioParaListagemDTO>()
                .ForMember(dest => dest.Perfil, opt =>
                {                    
                    opt.ResolveUsing(src => src.Admin == true ? "Administrador" : "Parceiro");
                });
        }
    }
}
