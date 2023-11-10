using AutoMapper;
using ClassManager.Business.Dtos;
using ClassManager.Business.Entities;
using ClassManager.Business.Entities.Validators;
using ClassManager.Business.Repositories;
using ClassManager.Business.Services.Interfaces;

namespace ClassManager.Business.Services.Concretos;

public class UsuarioService : BaseService, IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario?> Adicionar(UsuarioCriacaoDto dto)
    {
        // Validar usuário
        var usuario = new Usuario();
        if (!Validar(new UsuarioValidator(), usuario))
            return null;
            
        _usuarioRepository.Adicionar(usuario);
        await _usuarioRepository.SaveChanges();

        return usuario;
    }

    public async Task<Usuario> ObterPorId(Guid id) 
        => await _usuarioRepository.ObterPorId(id);

    public async Task<IEnumerable<Usuario>> ObterTodos() 
        => await _usuarioRepository.ObterTodos();
}
