using AutoMapper;
using CRUDDapper.DTO;
using CRUDDapper.Models;

namespace CRUDDapper.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Usuario, UsuarioListarDTO>();
        }
    }
}
