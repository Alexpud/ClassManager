using ClassManager.Business.Dtos;
using ClassManager.Business.Entities;

namespace ClassManager.Business.Services.Interfaces;

public interface IUsuarioService
{
    Task<Usuario?> Criar(UsuarioCriacaoDto usuario);
    Task<string> Login(UsuarioLoginDto dto);
    //Task<Usuario?> ObterPorId(Guid id);
    //Task<IEnumerable<Usuario>> ObterTodos();
    //Task Remover(Guid id);
}