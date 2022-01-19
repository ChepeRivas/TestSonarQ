using AutoMapper;
using PerfectWorld.Data.Dtos;
using PerfectWorld.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VMUsers, Users>()
                .ForMember(d => d.name, o => o.MapFrom(a => a.Usuario))
                .ForMember(d => d.passwd, o => o.MapFrom(a => a.Contrasena))
                .ForMember(d => d.passwd2, o => o.MapFrom(a => a.Contrasena))
                .ForMember(d => d.email, o => o.MapFrom(a => a.Correo));
        }
    }
    public class MappingSecret : Profile
    {
        public MappingSecret()
        {
            CreateMap<Users, VMUsers>()
           .ForMember(d => d.Usuario, o => o.MapFrom(a => a.name))
           .ForMember(d => d.Correo, o => o.MapFrom(a => a.email))
           .ForMember(d => d.Id, o => o.MapFrom(a => a.ID))
           .ForMember(d => d.Secret, o => o.MapFrom(a => a.answer));
        }
    }
}
