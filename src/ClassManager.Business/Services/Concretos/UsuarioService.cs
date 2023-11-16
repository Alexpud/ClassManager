using AutoMapper;
using ClassManager.Business.Dtos;
using ClassManager.Business.Entities;
using ClassManager.Business.Entities.Validators;
using ClassManager.Business.Notifications;
using ClassManager.Business.Repositories;
using ClassManager.Business.Services.Interfaces;
using FluentValidation;

namespace ClassManager.Business.Services.Concretos;

public class UsuarioService : BaseService, IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IValidator<Usuario> _usuarioValidator;
    private readonly ILoginService _authenticationService;

    public UsuarioService(
        IUsuarioRepository usuarioRepository, 
        IValidator<Usuario> usuarioValidator, 
        INotificationServce notificationServce,
        ILoginService authenticationService) : base(notificationServce)
    {
        _usuarioValidator = usuarioValidator;
        this._authenticationService = authenticationService;
        _usuarioRepository = usuarioRepository;
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

    public async Task<string> Login(UsuarioLoginDto dto)
    {
        // Autenticar o usuario com o usuario e senha
        if (!await _authenticationService.CredenciaisSaoValidas(dto.UserName, dto.Password))
            return null;

        return await _authenticationService.GerarTokenAcesso(dto.UserName);
    }

    //public async Task<Usuario?> ObterPorId(Guid id)
    //    => await _usuarioRepository.ObterPorId(id);

    //public async Task<IEnumerable<Usuario>> ObterTodos()
    //    => await _usuarioRepository.ObterTodos();

    //public async Task Remover(Guid id)
    //{
    //    _usuarioRepository.Remover(id);
    //    await _usuarioRepository.SaveChanges();
    //}
}
