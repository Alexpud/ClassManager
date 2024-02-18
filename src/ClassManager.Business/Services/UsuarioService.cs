using AutoMapper;
using ClassManager.Business.Dtos.Usuario;
using ClassManager.Business.Entities;
using ClassManager.Business.Errors;
using ClassManager.Business.Interfaces.Repositories;
using FluentResults;
using FluentValidation;

namespace ClassManager.Business.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CriarUsuarioDto> _usuarioValidator;

    public UsuarioService(
        IUsuarioRepository usuarioRepository,
        IValidator<CriarUsuarioDto> usuarioValidator,
        IMapper mapper)
    {
        _usuarioValidator = usuarioValidator;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<Result<Usuario>> Criar(CriarUsuarioDto dto)
    {
        var usuario = new Usuario()
        {
            UserName = dto.UserName,
            Tipo = dto.TipoUsuario,
            Nome = dto.Nome,
            SobreNome = dto.SobreNome
        };
        var result = new Result<Usuario>();
        var validationResult = _usuarioValidator.Validate(dto);
        if (!validationResult.IsValid)
            return result.WithErrors(validationResult.Errors.Select(p => new ValidationError(p.ErrorMessage)));

        var repositoryAddResult = await _usuarioRepository.Adicionar(usuario, dto.Password);
        if (!repositoryAddResult.Succeeded)
            return result.WithErrors(repositoryAddResult.Errors.Select(p => new Error(p.Description)));

        return usuario;
    }

    public async Task<UsuarioDto?> ObterDadosResumidosPorId(Guid id)
    {
        var usuario = await _usuarioRepository.ObterPorId(id);
        if (usuario == null)
            return null;
        return _mapper.Map<UsuarioDto>(usuario);
    }

    public async Task<IEnumerable<UsuarioDto>> ObterTodos()
    {
        var usuarios = await _usuarioRepository.ObterTodos();
        if (!usuarios.Any())
            return Enumerable.Empty<UsuarioDto>();

        return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
    }

    // public async Task Remover(Guid id)
    // {
    //    _usuarioRepository.Remover(id);
    //    await _usuarioRepository.SaveChanges();
    // }
}
