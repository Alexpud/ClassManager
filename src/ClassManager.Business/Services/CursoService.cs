using AutoMapper;
using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Entities;
using ClassManager.Business.Enums;
using ClassManager.Business.Interfaces.Repositories;
using ClassManager.Business.Notifications;
using FluentValidation;

namespace ClassManager.Business.Services;

public class CursoService : BaseService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IValidator<CriarCursoDto> _criarCursoDtoValidator;
    private readonly IMapper _mapper;

    public CursoService(
        INotificationServce notificationServce,
        IUsuarioRepository usuarioRepository,
        ICursoRepository cursoRepository,
        IValidator<CriarCursoDto> validator,
        IMapper mapper) : base(notificationServce)
    {
        _usuarioRepository = usuarioRepository;
        _cursoRepository = cursoRepository;
        _criarCursoDtoValidator = validator;
        _mapper = mapper;
    }

    public async Task<CursoDto> CriarCurso(CriarCursoDto dto)
    {
        if (!Validar(_criarCursoDtoValidator, dto))
            return null;

        if (dto.ProfessorId == Guid.Empty)
            _notificationService.Handle("Não pode criar curso sem professor");

        var usuario = await _usuarioRepository.ObterPorId(dto.ProfessorId);
        if (usuario == null || usuario.Tipo != TipoUsuario.Professor)
        {
            _notificationService.Handle("Professor não foi encontrado");
            return null;
        }
        var curso = new Curso
        {
            ProfessorId = dto.ProfessorId,
            Nome = dto.Nome,
            Tags = string.Join(",", dto.Tags)
        };
        
        _cursoRepository.Adicionar(curso);
        await _cursoRepository.SaveChanges();

        return _mapper.Map<CursoDto>(curso);
    }
}
