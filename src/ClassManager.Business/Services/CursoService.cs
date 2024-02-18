using AutoMapper;
using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Entities;
using ClassManager.Business.Enums;
using ClassManager.Business.Errors;
using ClassManager.Business.Interfaces.Repositories;
using FluentResults;
using FluentValidation;

namespace ClassManager.Business.Services;

public class CursoService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IValidator<CriarCursoDto> _criarCursoDtoValidator;
    private readonly IMapper _mapper;

    public CursoService(
        IUsuarioRepository usuarioRepository,
        ICursoRepository cursoRepository,
        IValidator<CriarCursoDto> validator,
        IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _cursoRepository = cursoRepository;
        _criarCursoDtoValidator = validator;
        _mapper = mapper;
    }

    public async Task<List<CursoDto>> ObterTodos()
    {
        var cursos = await _cursoRepository.ObterTodos();
        return _mapper.Map<List<CursoDto>>(cursos);
    }

    public async Task<Result<CursoDto>> CriarCurso(CriarCursoDto dto)
    {
        var result = new Result<CursoDto>();
        var validationResult = _criarCursoDtoValidator.Validate(dto);
        if (!validationResult.IsValid)
            return result.WithErrors(validationResult.Errors.Select(p => new ValidationError(p.ErrorMessage)));

        var usuario = await _usuarioRepository.ObterPorId(dto.ProfessorId);
        if (usuario == null || usuario.Tipo != TipoUsuario.Professor)
            return result.WithError("Professor não foi encontrado");
        
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
