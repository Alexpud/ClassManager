using AutoMapper;
using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Entities;

namespace ClassManager.Business.Profiles;

public class CursoProfile : Profile
{
    public CursoProfile()
    {
        CreateMap<Curso, CursoDto>();
    }
}
