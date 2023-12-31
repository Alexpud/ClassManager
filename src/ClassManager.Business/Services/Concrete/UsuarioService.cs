using AutoMapper;
using ClassManager.Business.Dtos.Usuario;
using ClassManager.Business.Entities;
using ClassManager.Business.Notifications;
using ClassManager.Business.Repositories;
using ClassManager.Business.Services.Abstract;
using FluentValidation;

namespace ClassManager.Business.Services.Concrete;

public class UsuarioService : BaseService, IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<Usuario> _usuarioValidator;

    public UsuarioService(
        IUsuarioRepository usuarioRepository, 
        IValidator<Usuario> usuarioValidator, 
        INotificationServce notificationServce,
        IMapper mapper) : base(notificationServce)
    {
        _usuarioValidator = usuarioValidator;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<Usuario?> Criar(UsuarioCriacaoDto dto)
    {
        var usuario = new Usuario()
        {
            UserName = dto.UserName,
            Tipo = dto.TipoUsuario,
            Nome = dto.Nome,
            SobreNome = dto.SobreNome
        };

        if (!Validar(_usuarioValidator, usuario))
            return null;

        var result = await _usuarioRepository.Adicionar(usuario, dto.Password);
        if (!result.Succeeded)
        {
            Notificar(result.Errors.Select(p => p.Description));
            return null;
        }

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
