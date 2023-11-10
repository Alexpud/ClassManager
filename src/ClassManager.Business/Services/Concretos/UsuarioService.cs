using AutoMapper;
using ClassManager.Business.Dtos;
using ClassManager.Business.Entities;
using ClassManager.Business.Entities.Validators;
using ClassManager.Business.Repositories;
using ClassManager.Business.Services.Interfaces;

namespace ClassManager.Business.Services.Concretos;

public class UsuarioService : BaseService<Usuario>, IUsuarioService
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;
    public async Task<Usuario?> Adicionar(UsuarioCriacaoDto dto)
    {
        // Validar usuário
        var usuario = _mapper.Map<Usuario>(dto);
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
