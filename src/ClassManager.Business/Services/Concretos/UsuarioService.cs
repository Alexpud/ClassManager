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

    public UsuarioService(
        IUsuarioRepository usuarioRepository, 
        IValidator<Usuario> usuarioValidator, 
        INotificationServce notificationServce) : base(notificationServce)
    {
        _usuarioValidator = usuarioValidator;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario?> Adicionar(UsuarioCriacaoDto dto)
    {
        var usuario = new Usuario()
        {
            Login = dto.Login,
            Senha = dto.Senha,
            Nome = dto.Nome,
            SobreNome = dto.SobreNome
        };
        
        if (!Validar(_usuarioValidator, usuario))
            return null;

        _usuarioRepository.Adicionar(usuario);
        await _usuarioRepository.SaveChanges();

        return usuario;
    }

    public async Task<Usuario?> ObterPorId(Guid id)
        => await _usuarioRepository.ObterPorId(id);

    public async Task<IEnumerable<Usuario>> ObterTodos()
        => await _usuarioRepository.ObterTodos();

    public async Task Remover(Guid id)
    {
        _usuarioRepository.Remover(id);
        await _usuarioRepository.SaveChanges();
    }
}
