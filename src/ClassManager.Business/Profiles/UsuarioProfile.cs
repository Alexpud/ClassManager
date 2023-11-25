using AutoMapper;
using ClassManager.Business.Dtos.Usuario;
using ClassManager.Business.Entities;

namespace ClassManager.Business.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDto>();
    }
}